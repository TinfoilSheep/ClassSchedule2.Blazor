using ClassSchedule2.Blazor.Models.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Threading.Channels;

namespace ClassSchedule2.Blazor.Models.Forms.LessonTemplates
{
    public class CreateLessonTemplateFormModel
    {
        [Required(ErrorMessage = "Ugedag er påkrævet.")]
        [Range(1, 5, ErrorMessage = "Ugedag er påkrævet.")]
        public int WeekDay { get; set; }

        [Required(ErrorMessage = "Startdato er påkrævet.")]
        public DateOnly ValidFrom { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Required(ErrorMessage = "Slutdato er påkrævet.")]
        [DateGreaterThan(nameof(ValidFrom), ErrorMessage = "Slutdato skal være efter startdato.")]
        public DateOnly ValidTo { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Required(ErrorMessage = "Hold er påkrævet.")]
        public Guid? HoldId { get; set; }

        [Required(ErrorMessage = "Periode er påkrævet.")]
        public Guid? PeriodId { get; set; }

        public Guid? RoomId { get; set; }
    }
}
