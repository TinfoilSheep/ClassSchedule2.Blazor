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
        private ScheduleLesson? _selectedLesson;
        private DateOnly _selectedWeek = new(2026, 8, 10);
        private const int MinuteHeight = 1;
        //private const int HourHeight = 80;
        private static readonly TimeOnly ScheduleStart = new(8, 0);
        private static readonly TimeOnly ScheduleEnd = new(16, 30);
        private bool _showLessonModal;
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
            var lessonStartMinutes = lesson.StartTime.Hour * 60 + lesson.StartTime.Minute;

            var scheduleStartMinutes = ScheduleStart.Hour * 60 + ScheduleStart.Minute;

            return (lessonStartMinutes - scheduleStartMinutes) * MinuteHeight;
        }

        private double GetLessonHeight(ScheduleLesson lesson)
        {
            var lessonStartMinutes = lesson.StartTime.Hour * 60 + lesson.StartTime.Minute;

            var lessonEndMinutes = lesson.EndTime.Hour * 60 + lesson.EndTime.Minute;

            return (lessonEndMinutes - lessonStartMinutes) * MinuteHeight;
        }

        private double GetScheduleHeight()
        {
            var scheduleStartMinutes = ScheduleStart.Hour * 60 + ScheduleStart.Minute;

            var scheduleEndMinutes = ScheduleEnd.Hour * 60 + ScheduleEnd.Minute;

            return (scheduleEndMinutes - scheduleStartMinutes) * MinuteHeight;
        }

        private double GetTimeTop(TimeOnly time)
        {
            var timeMinutes = time.Hour * 60 + time.Minute;

            var startMinutes = ScheduleStart.Hour * 60 + ScheduleStart.Minute;

            return (timeMinutes - startMinutes) * MinuteHeight;
        }

        private bool IsCurrentLesson(ScheduleLesson lesson)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var now = TimeOnly.FromDateTime(DateTime.Now);

            return lesson.Date == today && now >= lesson.StartTime && now < lesson.EndTime;
        }

        private void PreviousWeek()
        {
            _selectedWeek = GetMonday(_selectedWeek).AddDays(-7);
        }

        private void NextWeek()
        {
            _selectedWeek = GetMonday(_selectedWeek).AddDays(7);
        }

        private void GoToCurrentWeek()
        {
            _selectedWeek = GetMonday(DateOnly.FromDateTime(DateTime.Today));
        }

        private string GetLessonCardClass(ScheduleLesson lesson)
        {
            var baseClass = "absolute right-2 left-2 z-10 cursor-pointer rounded-xl p-3 shadow-sm transition-all";

            if (IsCurrentLesson(lesson))
            {
                return $"{baseClass} border-2 border-amber-400 bg-amber-400/20 shadow-md shadow-amber-400/10 ring-2 ring-amber-400/20 dark:border-sky-400 dark:bg-sky-500/20 dark:shadow-sky-500/10 dark:ring-sky-400/20";
            }

            return $"{baseClass} border border-amber-400/20 bg-amber-400/10 hover:bg-amber-400/15 hover:shadow-md dark:border-sky-500/20 dark:bg-sky-500/10 dark:hover:bg-sky-500/15";
        }

        private void OpenLessonModal(ScheduleLesson lesson)
        {
            _selectedLesson = lesson;
            _showLessonModal = true;
        }

        private void CloseLessonModal()
        {
            _showLessonModal = false;
            _selectedLesson = null;
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

        private IEnumerable<TimeOnly> GetScheduleTimes()
        {
            var current = ScheduleStart;

            while (current <= ScheduleEnd)
            {
                yield return current;
                current = current.AddMinutes(60);
            }
        }
    }
}
