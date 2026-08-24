using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Forms.StudentGroup
{
    public class CreateStudentGroupFormModel
    {
        [Required(ErrorMessage = "Navn på Klassen er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navnet må højst være 100 tegn.")]
        public string Name { get; set; }

        public List<Guid> Students { get; set; } = [];
    }
}
