using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Models;
using Microsoft.AspNetCore.Components;

namespace ClassSchedule2.Blazor.Components.Pages.Schedule
{
    public partial class Schedule
    {
        [Inject]
        private IScheduleService ScheduleService { get; set; } = default!;

        private List<ScheduleLesson> _lessons = [];
        private DateOnly _selectedWeek = new(2026, 8, 10);
        private const int HourHeight = 80;
        private const int ScheduleStartHour = 8;
        private const int ScheduleEndHour = 12;

        private bool _isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            _lessons = await ScheduleService.GetScheduleAsync();

            _isLoading = false;
        }

        private DateOnly GetMonday(DateOnly date)
        {
            var difference = ((int)date.DayOfWeek - (int)DayOfWeek.Monday + 7) % 7;

            return date.AddDays(-difference);
        }

        private DateOnly GetSunday(DateOnly date)
        {
            return GetMonday(date).AddDays(6);
        }

        private int GetWeekNumber(DateOnly date)
        {
            return System.Globalization.ISOWeek.GetWeekOfYear(date.ToDateTime(TimeOnly.MinValue));
        }

        private string GetWeekDateRange()
        {
            var monday = GetMonday(_selectedWeek);
            var sunday = GetSunday(_selectedWeek);

            return $"{monday:dd. MMMM} – {sunday:dd. MMMM yyyy}";
        }

        private double GetLessonTop(ScheduleLesson lesson)
        {
            var lessonMinutes = lesson.StartTime.Hour * 60 + lesson.StartTime.Minute;

            var scheduleStartMinutes = ScheduleStartHour * 60;

            var minutesFromStart = lessonMinutes - scheduleStartMinutes;

            return minutesFromStart / 60.0 * HourHeight;
        }

        private double GetLessonHeight(ScheduleLesson lesson)
        {
            var startMinutes = lesson.StartTime.Hour * 60 + lesson.StartTime.Minute;

            var endMinutes = lesson.EndTime.Hour * 60 + lesson.EndTime.Minute;

            var durationMinutes = endMinutes - startMinutes;

            return durationMinutes / 60.0 * HourHeight;
        }

        private IEnumerable<ScheduleLesson> GetLessonsForDay(DateOnly date)
        {
            return _lessons.Where(x => x.Date == date).OrderBy(x => x.StartTime);
        }

        private IEnumerable<DateOnly> GetWeekDays()
        {
            var monday = GetMonday(_selectedWeek);

            return Enumerable.Range(0, 5).Select(monday.AddDays);
        }
    }
}
