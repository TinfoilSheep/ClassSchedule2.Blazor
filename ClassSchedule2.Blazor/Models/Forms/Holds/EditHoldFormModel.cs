using ClassSchedule2.Blazor.Models.DTOs;
using System.ComponentModel.DataAnnotations;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;

namespace ClassSchedule2.Blazor.Models.Forms.Holds
{
    public class EditHoldFormModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Navn på Hold er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navnet må højst være 100 tegn.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fag Id på Hold er påkrævet")]
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Term Id på Hold er påkrævet")]
        public Guid TermId { get; set; }
        public string TermName { get; set; } = string.Empty;

        public List<Guid> Teachers = [];
        public List<Guid> Students = [];
    }
}
