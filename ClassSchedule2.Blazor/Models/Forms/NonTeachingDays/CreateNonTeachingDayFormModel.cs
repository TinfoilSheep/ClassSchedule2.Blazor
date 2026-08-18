using ClassSchedule2.Blazor.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Forms.NonTeachingDays
{
    public class CreateNonTeachingDayFormModel
    {
        [Required(ErrorMessage = "Startdato er påkrævet.")]
        public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required(ErrorMessage = "Slutdato er påkrævet.")]
        [DateGreaterThanOrEqual(nameof(StartDate), ErrorMessage = "Slutdato skal være samme dag eller efter startdato.")]
        public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

        [Required(ErrorMessage = "Grund til fridag er påkrævet.")]
        [StringLength(200, ErrorMessage = "Grunden må højst være 200 tegn.")]
        public string Reason { get; set; } = string.Empty;
    }
}
