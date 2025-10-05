using FormManagementSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FormManagementSystem.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Form> Forms { get; set; } = null!;
        public DbSet<FormField> FormFields { get; set; } = null!;
        public DbSet<FormFieldOption> FormFieldOptions { get; set; } = null!;
        public DbSet<FormSubmission> FormSubmissions { get; set; } = null!;
        public DbSet<SubmissionValue> SubmissionValues { get; set; } = null!;
        public DbSet<FileStorage> FileStorages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasIndex(u => u.UserName).IsUnique();
            builder.Entity<ApplicationUser>().HasIndex(u => u.Email).IsUnique();

            // Form -> FormFields (one-to-many)
            builder.Entity<Form>()
                .HasMany(f => f.Fields)
                .WithOne(ff => ff.Form)
                .HasForeignKey(ff => ff.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            // Form -> FormSubmissions (one-to-many)
            builder.Entity<Form>()
                .HasMany(f => f.Submissions)
                .WithOne(fs => fs.Form)
                .HasForeignKey(fs => fs.FormId)
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationUser -> Forms (one-to-many)
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.FormsCreated)
                .WithOne(f => f.CreatedBy)
                .HasForeignKey(f => f.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // ApplicationUser -> FormSubmissions (one-to-many)
            builder.Entity<ApplicationUser>()
                .HasMany(u => u.FormSubmissions)
                .WithOne(fs => fs.SubmittedBy)
                .HasForeignKey(fs => fs.SubmittedById)
                .OnDelete(DeleteBehavior.Restrict);

            // FormField -> FormFieldOptions (one-to-many)
            builder.Entity<FormField>()
                .HasMany(ff => ff.Options)
                .WithOne(o => o.FormField)
                .HasForeignKey(o => o.FormFieldId)
                .OnDelete(DeleteBehavior.Cascade);

            // FormSubmission -> SubmissionValues (one-to-many)
            builder.Entity<FormSubmission>()
                .HasMany(fs => fs.FieldSubmissions)
                .WithOne(sv => sv.FormSubmission)
                .HasForeignKey(sv => sv.FormSubmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            // FormSubmission -> FileStorage (one-to-many)
            builder.Entity<FormSubmission>()
                .HasMany(fs => fs.Files)
                .WithOne(f => f.FormSubmission)
                .HasForeignKey(f => f.FormSubmissionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<FormFieldOption>()
                .HasIndex(o => new { o.FormFieldId, o.SortOrder });
        }
    }
}
