using FormManagementSystem.Database;
using FormManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FormManagementSystem.Repositories
{
    public class SubmissionRepository : ISubmissionRepository
    {
        private readonly ApplicationDbContext _db;
        public SubmissionRepository(ApplicationDbContext db) => _db = db;

        public async Task AddAsync(FormSubmission submission) => await _db.FormSubmissions.AddAsync(submission);

        public async Task<FormSubmission?> GetByIdAsync(int id)
        {
            return await _db.FormSubmissions
                .Include(s => s.FieldSubmissions)
                    .ThenInclude(v => v.FormField)
                .Include(s => s.Files)
                .FirstOrDefaultAsync(s => s.FormSubmissionId == id && !s.IsDeleted);
        }

        public async Task<IEnumerable<FormSubmission>> GetByFormIdAsync(int formId)
        {
            return await _db.FormSubmissions
                .Where(s => s.FormId == formId && !s.IsDeleted)
                .Include(s => s.FieldSubmissions)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> CountAllSubmissionsAsync()
        {
            return await _db.FormSubmissions.CountAsync(s => !s.IsDeleted);
        }
    }
}
