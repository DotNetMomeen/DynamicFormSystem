using FormManagementSystem.Database;

namespace FormManagementSystem.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IFormRepository Forms { get; }
        public ISubmissionRepository Submissions { get; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Forms = new FormRepository(db);
            Submissions = new SubmissionRepository(db);
        }

        public async Task<int> SaveChangesAsync() => await _db.SaveChangesAsync();

        public void Dispose() => _db?.Dispose();
    }
}
