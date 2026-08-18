using ClassSchedule2.Blazor.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Forms.Periods
{
    public class CreatePeriodFormModel
    {
        [Required(ErrorMessage = "Navn på periode er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navnet må højst være 100 tegn.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Starttid er påkrævet.")]
        public TimeOnly StartTime { get; set; } = new(8,00);

        [Required(ErrorMessage = "Sluttid er påkrævet.")]
        [TimeGreaterThan(nameof(StartTime), ErrorMessage = "Sluttid skal være efter starttid.")]
        public TimeOnly EndTime { get; set; } = new(9,00);

        [Required(ErrorMessage = "Rækkefølge er påkrævet.")]
        [Range(1, int.MaxValue, ErrorMessage = "Rækkefølge skal være et positivt tal.")]
        public int SortOrder { get; set; } = 1;
    }
}
