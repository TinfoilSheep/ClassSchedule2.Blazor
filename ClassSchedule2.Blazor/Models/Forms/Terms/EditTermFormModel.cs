using ClassSchedule2.Blazor.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Forms.Terms
{
    public class EditTermFormModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Navn på termin er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navnet må højst være 100 tegn.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Startdato er påkrævet.")]
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required(ErrorMessage = "Slutdato er påkrævet.")]
        [DateGreaterThan(nameof(StartDate), ErrorMessage = "Slutdato skal være efter startdato.")]
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));
    }
}
