using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Forms.Subjects
{
    public class CreateSubjectFormModel
    {
        [Required(ErrorMessage = "Navn på fag er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navnet må højst være 100 tegn.")]
        public string Name { get; set; } = string.Empty;
    }
}
