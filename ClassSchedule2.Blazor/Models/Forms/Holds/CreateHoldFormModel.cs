using ClassSchedule2.Blazor.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Models.Forms.Holds
{
    public class CreateHoldFormModel
    {
        [Required(ErrorMessage = "Navn på Hold er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navnet må højst være 100 tegn.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fag Id på Hold er påkrævet")]
        public Guid SubjectId { get; set; }

        [Required(ErrorMessage = "Term Id på Hold er påkrævet")]
        public Guid TermId { get; set; }

        public List<MinimalUserInformationDTO> Teachers = [];
        public List<MinimalUserInformationDTO> Students = [];
    }
}
