using ClassSchedule2.Blazor.Models.Models;
using Microsoft.AspNetCore.Components;

namespace ClassSchedule2.Blazor.Components.Pages.Schedule
{
    public partial class LessonDetails
    {
        [Parameter, EditorRequired]
        public ScheduleLesson Lesson { get; set; } = default!;

        [Parameter]
        public EventCallback OnCancel { get; set; }
    }
}
