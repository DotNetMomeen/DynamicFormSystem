using System.ComponentModel.DataAnnotations;

namespace FormManagementSystem.Models
{
    //public class FormFieldOption
    //{
    //    [Key]
    //    public int FormFieldOptionId { get; set; }

    //    [Required]
    //    public int FormFieldId { get; set; }
    //    public FormField FormField { get; set; }

    //    [Required]
    //    public string Value { get; set; }

    //    // Text shown to the user (dropdown label)
    //    public string DisplayText { get; set; }

    //    // Defines option order in dropdown/multiselect
    //    public int SortOrder { get; set; }
    //}

    //public class FormFieldOption
    //{
    //    [Key]
    //    public int FormFieldOptionId { get; set; }

    //    [Required]
    //    public int FormFieldId { get; set; }
    //    public FormField? FormField { get; set; }

    //    [Required]
    //    public string Value { get; set; } = null!;

    //    public string? DisplayText { get; set; }

    //    public int SortOrder { get; set; } = 0;
    //}

    public class FormFieldOption
    {
        [Key]
        public int FormFieldOptionId { get; set; }

        [Required]
        public int FormFieldId { get; set; }
        public FormField? FormField { get; set; }

        [Required]
        public string Value { get; set; } = null!;

        public string? DisplayText { get; set; }

        public int SortOrder { get; set; } = 0;
    }
}
