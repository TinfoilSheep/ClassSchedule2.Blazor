using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.ScheduleLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Schedule
{
    public partial class LessonDetails
    {
        [Parameter, EditorRequired]
        public ScheduleLessonDTO Lesson { get; set; } = default!;

        [Parameter]
        public EventCallback OnCancel { get; set; }
    }
}
