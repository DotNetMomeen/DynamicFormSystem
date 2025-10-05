using FormManagementSystem.Models;
using System;
using System.Threading.Tasks;

namespace FormManagementSystem.Services
{
    public interface IFormService
    {
        Task<Form> CreateFormAsync(Form form, string createdById);
        Task PublishFormAsync(int formId, DateTimeOffset deadline, string actorId);
        Task<bool> CanSubmitAsync(int formId);
        Task<FormSubmission> SaveSubmissionAsync(FormSubmission submission, bool singleSubmissionPerUser = false);
    }
}
