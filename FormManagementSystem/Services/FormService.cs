using FormManagementSystem.Models;
using FormManagementSystem.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FormManagementSystem.Services
{
    public class FormService : IFormService
    {
        private readonly IUnitOfWork _uow;

        public FormService(IUnitOfWork uow) => _uow = uow;

        public async Task<Form> CreateFormAsync(Form form, string createdById)
        {
            form.CreatedById = createdById;
            form.CreatedAt = DateTimeOffset.UtcNow;
            await _uow.Forms.AddAsync(form);
            await _uow.SaveChangesAsync();
            return form;
        }

        public async Task PublishFormAsync(int formId, DateTimeOffset deadline, string actorId)
        {
            var form = await _uow.Forms.GetByIdWithFieldsAsync(formId);
            if (form == null) throw new InvalidOperationException("Form not found.");
            if (deadline <= DateTimeOffset.UtcNow) throw new InvalidOperationException("Deadline must be in the future.");

            form.Deadline = deadline;
            form.IsPublished = true;
            form.IsActive = true;

            _uow.Forms.Update(form);
            await _uow.SaveChangesAsync();
            // TODO: audit log with actorId if needed
        }

        public async Task<bool> CanSubmitAsync(int formId)
        {
            var form = await _uow.Forms.GetAsync(formId);
            if (form == null || !form.IsPublished || !form.IsActive) return false;
            if (form.Deadline == null) return true;
            return DateTimeOffset.UtcNow < form.Deadline;
        }

        public async Task<FormSubmission> SaveSubmissionAsync(FormSubmission submission, bool singleSubmissionPerUser = false)
        {
            var can = await CanSubmitAsync(submission.FormId);
            if (!can) throw new InvalidOperationException("Form not accepting submissions (deadline passed or not published).");

            if (singleSubmissionPerUser)
            {
                var existing = (await _uow.Submissions.GetByFormIdAsync(submission.FormId))
                    .FirstOrDefault(s => s.SubmittedById == submission.SubmittedById && !s.IsDeleted);

                if (existing != null)
                {
                    existing.IsDeleted = true;
                    await _uow.SaveChangesAsync();
                    await _uow.Submissions.AddAsync(submission);
                    await _uow.SaveChangesAsync();
                    return submission;
                }
            }

            await _uow.Submissions.AddAsync(submission);
            await _uow.SaveChangesAsync();
            return submission;
        }
    }
}
