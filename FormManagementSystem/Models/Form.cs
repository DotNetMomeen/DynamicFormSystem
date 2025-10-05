using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace FormManagementSystem.Models
{
    public class Form
    {
        [Key]
        public int FormId { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = false;
        public bool IsPublished { get; set; } = false;

        [Required]
        public string CreatedById { get; set; } = null!;
        public ApplicationUser? CreatedBy { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? Deadline { get; set; }

        // Fix property names to match DbContext
        public ICollection<FormField> Fields { get; set; } = new List<FormField>();
        public ICollection<FormSubmission> Submissions { get; set; } = new List<FormSubmission>();
    }
}
