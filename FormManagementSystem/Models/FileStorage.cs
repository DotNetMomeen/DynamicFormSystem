using System.ComponentModel.DataAnnotations;

namespace FormManagementSystem.Models
{
    //public class FileStorage
    //{
    //    [Key]
    //    public int FileStorageId { get; set; }

    //    [Required]
    //    public int SubmissionId { get; set; }
    //    public FormSubmission Submission { get; set; }

    //    [Required]
    //    public int FieldId { get; set; }

    //    [Required, MaxLength(255)]
    //    public string FileName { get; set; }

    //    [Required]
    //    public string StoredPath { get; set; } // relative path or blob URL

    //    public long Size { get; set; }
    //    public string ContentType { get; set; }
    //    public DateTimeOffset UploadedAt { get; set; } = DateTimeOffset.UtcNow;
    //}
    //public class FileStorage
    //{
    //    [Key]
    //    public int FileStorageId { get; set; }

    //    [Required]
    //    public int SubmissionId { get; set; }
    //    public FormSubmission? Submission { get; set; }

    //    [Required]
    //    public int FieldId { get; set; }

    //    [Required, MaxLength(255)]
    //    public string FileName { get; set; } = null!;

    //    [Required]
    //    public string StoredPath { get; set; } = null!;

    //    public long Size { get; set; }
    //    public string? ContentType { get; set; }
    //    public DateTimeOffset UploadedAt { get; set; } = DateTimeOffset.UtcNow;
    //}

    public class FileStorage
    {
        [Key]
        public int FileStorageId { get; set; }

        [Required]
        public int FormSubmissionId { get; set; }  // Change from SubmissionId to FormSubmissionId
        public FormSubmission? FormSubmission { get; set; }

        [Required]
        public int FormFieldId { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; } = null!;

        [Required]
        public string StoredPath { get; set; } = null!;

        public long Size { get; set; }
        public string? ContentType { get; set; }
        public DateTimeOffset UploadedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
