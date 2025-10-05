using System.ComponentModel.DataAnnotations;

namespace FormManagementSystem.Models
{
    //public class FormSubmission
    //{
    //    [Key]
    //    public int FormSubmissionId { get; set; }

    //    [Required]
    //    public int FormId { get; set; }
    //    public Form Form { get; set; }

    //    [Required]
    //    public string SubmittedById { get; set; }
    //    public ApplicationUser SubmittedBy { get; set; }

    //    public DateTimeOffset SubmittedAt { get; set; } = DateTimeOffset.UtcNow;

    //    public bool IsDeleted { get; set; } = false; // soft delete for audit tracking

    //    public ICollection<SubmissionValue> Values { get; set; } = new List<SubmissionValue>();
    //    public ICollection<FileStorage> Files { get; set; } = new List<FileStorage>();
    //}

    //public class FormSubmission
    //{
    //    [Key]
    //    public int FormSubmissionId { get; set; }

    //    [Required]
    //    public int FormId { get; set; }
    //    public Form? Form { get; set; }

    //    [Required]
    //    public string SubmittedById { get; set; } = null!;
    //    public ApplicationUser? SubmittedBy { get; set; }

    //    public DateTimeOffset SubmittedAt { get; set; } = DateTimeOffset.UtcNow;

    //    public bool IsDeleted { get; set; } = false;

    //    public ICollection<SubmissionValue> SubmissionValues { get; set; } = new List<SubmissionValue>();
    //    public ICollection<FileStorage> FileStorageFiles { get; set; } = new List<FileStorage>();
    //}

    public class FormSubmission
    {
        [Key]
        public int FormSubmissionId { get; set; }

        [Required]
        public int FormId { get; set; }
        public Form? Form { get; set; }

        [Required]
        public string SubmittedById { get; set; } = null!;
        public ApplicationUser? SubmittedBy { get; set; }

        public DateTimeOffset SubmittedAt { get; set; } = DateTimeOffset.UtcNow;

        public bool IsDeleted { get; set; } = false;

        // Fix property names to match DbContext

        public ICollection<SubmissionValue> FieldSubmissions { get; set; } = new List<SubmissionValue>();
        public ICollection<FileStorage> Files { get; set; } = new List<FileStorage>();
    }
}
