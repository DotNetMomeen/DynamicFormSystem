using FormManagementSystem.Database;
using System.Threading.Tasks;

namespace FormManagementSystem.Repositories
{
    public interface IUnitOfWork
    {
        IFormRepository Forms { get; }
        ISubmissionRepository Submissions { get; }
        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
