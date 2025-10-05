using FormManagementSystem.Models;

namespace FormManagementSystem.Repositories
{
    public interface ISubmissionRepository
    {
        Task AddAsync(FormSubmission submission);
        Task<IEnumerable<FormSubmission>> GetByFormIdAsync(int formId);
        Task<FormSubmission?> GetByIdAsync(int id);
        Task<int> CountAllSubmissionsAsync();
    }
}
