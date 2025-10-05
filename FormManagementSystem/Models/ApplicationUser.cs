using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FormManagementSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(150)]
        public string FullName { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        // Fix property names to match DbContext
        public ICollection<Form> FormsCreated { get; set; } = new List<Form>();
        public ICollection<FormSubmission> FormSubmissions { get; set; } = new List<FormSubmission>();
    }
}
