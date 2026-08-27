using System.ComponentModel.DataAnnotations;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Models.Forms.StudentGroup
{
    public class EditStudentGroupFormModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Navn på Hold er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navnet må højst være 100 tegn.")]
        public string Name { get; set; } = string.Empty;

        public List<MinimalUserInformationDTO> Students { get; set; } = [];
    }
}
