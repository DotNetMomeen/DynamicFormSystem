using FormManagementSystem.Extensions;
using FormManagementSystem.Models;
using FormManagementSystem.Repositories;
using FormManagementSystem.Services;
using FormManagementSystem.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FormManagementSystem.Controllers
{
    [Authorize(Roles = "User,Admin,SuperAdmin")]
    public class SubmissionsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IFormService _formService;
        private readonly IWebHostEnvironment _env;

        public SubmissionsController(IUnitOfWork uow, IFormService formService, IWebHostEnvironment env)
        {
            _uow = uow;
            _formService = formService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var forms = await _uow.Forms.GetActivePublishedFormsAsync();
            return View("User/Dashboard", forms);
        }

        [HttpGet]
        public async Task<IActionResult> Form(int id)
        {
            var form = await _uow.Forms.GetByIdWithFieldsAsync(id);
            if (form == null || !form.IsPublished || !form.IsActive) return NotFound();
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(IFormCollection formCollection)
        {
            // Build FormSubmission from posted fields and files
            var submission = new FormSubmission();
            if (!int.TryParse(formCollection["FormId"], out var formId)) return BadRequest("Invalid form id");
            submission.FormId = formId;
            submission.SubmittedById = User.GetUserId();
            submission.SubmittedAt = System.DateTimeOffset.UtcNow;

            var form = await _uow.Forms.GetByIdWithFieldsAsync(formId);
            if (form == null) return BadRequest("Form not found");

            var values = new List<SubmissionValue>();
            var files = new List<FileStorage>();

            // handle fields
            foreach (var f in form.Fields)
            {
                var key = $"field_{f.FormFieldId}";
                if (f.FieldType == FieldType.File || f.FieldType == FieldType.Image || f.FieldType == FieldType.Audio || f.FieldType == FieldType.Video)
                {
                    // file handling via Request.Form.Files
                    var file = Request.Form.Files.GetFile($"file_{f.FormFieldId}");
                    if (file != null && file.Length > 0)
                    {
                        var stored = await FileHelper.SaveFileAsync(file, _env.WebRootPath);
                        var fs = new FileStorage
                        {
                            FileStorageId = f.FormFieldId,
                            FileName = file.FileName,
                            StoredPath = stored ?? string.Empty,
                            Size = file.Length,
                            ContentType = file.ContentType
                        };
                        files.Add(fs);
                    }
                }
                else if (f.FieldType == FieldType.MultiSelect)
                {
                    var arr = formCollection[key];
                    if (arr.Count > 0)
                    {
                        var joined = string.Join(',', arr.ToArray());
                        values.Add(new SubmissionValue { FormFieldId = f.FormFieldId, Value = joined });
                    }
                }
                else if (f.FieldType == FieldType.Checkbox)
                {
                    var val = formCollection[key].FirstOrDefault();
                    var boolVal = !string.IsNullOrEmpty(val) && (val == "true" || val == "on");
                    values.Add(new SubmissionValue { FormFieldId = f.FormFieldId, Value = boolVal ? "true" : "false" });
                }
                else
                {
                    var val = formCollection[key].FirstOrDefault();
                    if (!string.IsNullOrEmpty(val))
                    {
                        values.Add(new SubmissionValue { FormFieldId = f.FormFieldId, Value = val });
                    }
                }
            }

            submission.FieldSubmissions = values;
            submission.Files = files;

            await _formService.SaveSubmissionAsync(submission, singleSubmissionPerUser: false);
            TempData["Success"] = "Submitted successfully";
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public async Task<IActionResult> MySubmissions(int formId)
        {
            var userId = User.GetUserId();
            var all = await _uow.Submissions.GetByFormIdAsync(formId);
            var mine = all.Where(s => s.SubmittedById == userId);
            return View(mine);
        }
    }
}
