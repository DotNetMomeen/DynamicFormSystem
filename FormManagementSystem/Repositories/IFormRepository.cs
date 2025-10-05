using FormManagementSystem.Models;

namespace FormManagementSystem.Repositories
{
    public interface IFormRepository : IRepository<Form>
    {
        Task<Form?> GetByIdWithFieldsAsync(int id);
        Task<IEnumerable<Form>> GetActivePublishedFormsAsync();
        Task<int> CountActiveFormsAsync();
    }
}
