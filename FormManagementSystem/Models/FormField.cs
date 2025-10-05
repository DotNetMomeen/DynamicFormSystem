using System.ComponentModel.DataAnnotations;

namespace FormManagementSystem.Models
{
    //public class FormField
    //{
    //    [Key]
    //    public int FormFieldId { get; set; }

    //    [Required]
    //    public int FormId { get; set; }
    //    public Form Form { get; set; }

    //    [Required, MaxLength(150)]
    //    public string Label { get; set; }

    //    [Required]
    //    public FieldType FieldType { get; set; }

    //    public bool IsRequired { get; set; } = false;

    //    // For layout (grid positioning)
    //    public int RowNumber { get; set; } = 0;
    //    public int ColumnNumber { get; set; } = 0;

    //    // Determines display order within a row/column
    //    public int SortOrder { get; set; } = 0;

    //    // Stores JSON for extra configuration (e.g. placeholder, validation, etc.)
    //    public string MetaJson { get; set; }

    //    public ICollection<FormFieldOption> Options { get; set; } = new List<FormFieldOption>();
    //}

    //public class FormField
    //{
    //    [Key]
    //    public int FormFieldId { get; set; }

    //    [Required]
    //    public int FormId { get; set; }
    //    public Form? Form { get; set; }

    //    [Required, MaxLength(150)]
    //    public string Label { get; set; } = null!;

    //    [Required]
    //    public FieldType FieldType { get; set; }

    //    public bool IsRequired { get; set; } = false;

    //    public int RowNumber { get; set; } = 0;
    //    public int ColumnNumber { get; set; } = 0;

    //    public int SortOrder { get; set; } = 0;

    //    public string? MetaJson { get; set; }

    //    public ICollection<FormFieldOption> FormFieldOptions { get; set; } = new List<FormFieldOption>();
    //}

    public class FormField
    {
        [Key]
        public int FormFieldId { get; set; }

        [Required]
        public int FormId { get; set; }
        public Form? Form { get; set; }

        [Required, MaxLength(150)]
        public string Label { get; set; } = null!;

        [Required]
        public FieldType FieldType { get; set; }

        public bool IsRequired { get; set; } = false;

        public int RowNumber { get; set; } = 0;
        public int ColumnNumber { get; set; } = 0;

        public int SortOrder { get; set; } = 0;

        public string? MetaJson { get; set; }

        // Fix property name to match DbContext
        public ICollection<FormFieldOption> Options { get; set; } = new List<FormFieldOption>();
    }
}
