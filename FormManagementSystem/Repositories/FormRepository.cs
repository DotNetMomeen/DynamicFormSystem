using FormManagementSystem.Database;
using FormManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FormManagementSystem.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _db;
        public FormRepository(ApplicationDbContext db) => _db = db;

        public async Task AddAsync(Form entity) => await _db.Forms.AddAsync(entity);

        public async Task<Form?> GetAsync(params object[] keys) => await _db.Forms.FindAsync(keys);

        public async Task<IEnumerable<Form>> GetAllAsync() => await _db.Forms.AsNoTracking().ToListAsync();

        public void Remove(Form entity) => _db.Forms.Remove(entity);

        public void Update(Form entity) => _db.Forms.Update(entity);

        public async Task<Form?> GetByIdWithFieldsAsync(int id)
        {
            return await _db.Forms
                .Include(f => f.Fields)
                    .ThenInclude(ff => ff.Options)
                .Include(f => f.Submissions)
                .FirstOrDefaultAsync(f => f.FormId == id);
        }

        public async Task<IEnumerable<Form>> GetActivePublishedFormsAsync()
        {
            var utcNow = System.DateTimeOffset.UtcNow;
            return await _db.Forms
                .Where(f => f.IsPublished && f.IsActive && (f.Deadline == null || f.Deadline > utcNow))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> CountActiveFormsAsync()
        {
            return await _db.Forms.CountAsync(f => f.IsPublished && f.IsActive);
        }
    }
}
