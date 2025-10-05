using System.ComponentModel.DataAnnotations;

namespace FormManagementSystem.Models
{
    //public class SubmissionValue
    //{
    //    [Key]
    //    public int SubmissionValueId { get; set; }

    //    [Required]
    //    public int SubmissionId { get; set; }
    //    public FormSubmission Submission { get; set; }

    //    [Required]
    //    public int FormFieldId { get; set; }
    //    public FormField FormField { get; set; }

    //    // stored as string for flexibility (JSON if complex)
    //    public string Value { get; set; }
    //}
    //public class SubmissionValue
    //{
    //    [Key]
    //    public int SubmissionValueId { get; set; }

    //    [Required]
    //    public int FormSubmissionId { get; set; }  // This should match FormSubmissionId, not SubmissionId
    //    public FormSubmission? FormSubmission { get; set; }

    //    [Required]
    //    public int FormFieldId { get; set; }
    //    public FormField? FormField { get; set; }

    //    public string? Value { get; set; }
    //}

    public class SubmissionValue
    {
        [Key]
        public int SubmissionValueId { get; set; }

        [Required]
        public int FormSubmissionId { get; set; }
        public FormSubmission? FormSubmission { get; set; }

        [Required]
        public int FormFieldId { get; set; }
        public FormField? FormField { get; set; }

        public string? Value { get; set; }
    }
}
