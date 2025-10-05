using FormManagementSystem.Extensions;
using FormManagementSystem.Models;
using FormManagementSystem.Repositories;
using FormManagementSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FormManagementSystem.Controllers.Areas.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class FormsController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IFormService _formService;
        private readonly IExcelExportService _excel;

        public FormsController(IUnitOfWork uow, IFormService formService, IExcelExportService excel)
        {
            _uow = uow;
            _formService = formService;
            _excel = excel;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Form dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var userId = User.GetUserId();
            var form = await _formService.CreateFormAsync(dto, userId);
            return Ok(form);
        }

        [HttpPost]
        public async Task<IActionResult> Publish(int id, [FromBody] DateTimeOffset deadline)
        {
            try
            {
                var actor = User.GetUserId();
                await _formService.PublishFormAsync(id, deadline, actor);
                return Ok();
            }
            catch (Exception ex) { return BadRequest(new { error = ex.Message }); }
        }

        [HttpGet]
        public async Task<IActionResult> ExportExcel(int id)
        {
            var ms = await _excel.ExportFormAsync(id);
            return File(ms, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"form-{id}-export.xlsx");
        }
    }
}
