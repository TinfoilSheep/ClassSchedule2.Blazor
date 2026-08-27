using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Models;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.ScheduleLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Schedule
{
    public partial class Schedule : ComponentBase
    {
        [Inject]
        private IScheduleService ScheduleService { get; set; } = default!;
        [Inject]
        private ICurrentUserProvider CurrentUserProvider { get; set; } = default!;

        [Parameter] public Guid? TargetUserId { get; set; } = null;

        private CurrentUserData? CurrentUser { get; set; }

        private Guid UserId { get; set; }

        private List<ScheduleLessonDTO> _lessons = [];
        private ScheduleLessonDTO? _selectedLesson;

        private readonly HashSet<DateOnly> _loadedWeeks = [];
        private DateOnly _selectedWeek = new();
        private const int MinuteHeight = 1;
        private static readonly TimeOnly ScheduleStart = new(8, 0);
        private static readonly TimeOnly ScheduleEnd = new(16, 30);
        private const int ScheduleDays = 56;
        private bool _showLessonModal;
        private bool _isLoading = true;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
                return;

            CurrentUser = await CurrentUserProvider.GetAsync();

            if (CurrentUser is null)
            {
                _isLoading = false;
                StateHasChanged();
                return;
            }

            _selectedWeek = GetMonday(DateOnly.FromDateTime(DateTime.Today));

            await EnsureWeekLoadedAsync(_selectedWeek);

            _isLoading = false;

            StateHasChanged();
        }

        private async Task EnsureWeekLoadedAsync(DateOnly week)
        {
            week = GetMonday(week);

            if (_loadedWeeks.Contains(week))
                return;

            var from = week;
            var to = from.AddDays(ScheduleDays - 1);

            var dto = new GetScheduleLessonDTO(
                TargetId: Guid.Parse("53ecadbd-9a9b-4d03-96c9-050527f6dc8c"),
                From: from,
                To: to);

            var newLessons = await ScheduleService.GetScheduleAsync(dto);

            var existingIds = _lessons.Select(x => x.Id).ToHashSet();

            _lessons.AddRange(newLessons.Where(x => !existingIds.Contains(x.Id)));

            for (var currentWeek = from; currentWeek <= to; currentWeek = currentWeek.AddDays(7))
            {
                _loadedWeeks.Add(currentWeek);
            }
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

        private double GetLessonTop(ScheduleLessonDTO lesson)
        {
            var lessonStartMinutes = lesson.StartTime.Hour * 60 + lesson.StartTime.Minute;

            var scheduleStartMinutes = ScheduleStart.Hour * 60 + ScheduleStart.Minute;

            return (lessonStartMinutes - scheduleStartMinutes) * MinuteHeight;
        }

        private double GetLessonHeight(ScheduleLessonDTO lesson)
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

        private bool IsCurrentLesson(ScheduleLessonDTO lesson)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var now = TimeOnly.FromDateTime(DateTime.Now);

            return lesson.Date == today && now >= lesson.StartTime && now < lesson.EndTime;
        }

        private async Task PreviousWeek()
        {
            var previousWeek = GetMonday(_selectedWeek).AddDays(-7);

            _selectedWeek = previousWeek;

            await EnsureWeekLoadedAsync(previousWeek);

            StateHasChanged();
        }

        private async Task NextWeek()
        {
            var nextWeek = GetMonday(_selectedWeek).AddDays(7);

            _selectedWeek = nextWeek;

            await EnsureWeekLoadedAsync(nextWeek);

            StateHasChanged();
        }

        private async Task GoToCurrentWeek()
        {
            var currentWeek = GetMonday(DateOnly.FromDateTime(DateTime.Today));

            _selectedWeek = currentWeek;

            await EnsureWeekLoadedAsync(currentWeek);

            StateHasChanged();
        }

        private string GetLessonCardClass(ScheduleLessonDTO lesson)
        {
            var baseClass = "absolute right-2 left-2 z-10 cursor-pointer rounded-xl p-2 shadow-sm transition-all overflow-hidden";

            if (IsCurrentLesson(lesson))
            {
                return $"{baseClass} border-2 border-amber-400 bg-amber-400/20 shadow-md shadow-amber-400/10 ring-2 ring-amber-400/20 dark:border-sky-400 dark:bg-sky-500/20 dark:shadow-sky-500/10 dark:ring-sky-400/20";
            }

            return $"{baseClass} border border-amber-400/20 bg-amber-400/10 hover:bg-amber-400/15 hover:shadow-md dark:border-sky-500/20 dark:bg-sky-500/10 dark:hover:bg-sky-500/15";
        }

        private void OpenLessonModal(ScheduleLessonDTO lesson)
        {
            _selectedLesson = lesson;
            _showLessonModal = true;
        }

        private void CloseLessonModal()
        {
            _showLessonModal = false;
            _selectedLesson = null;
        }

        private IEnumerable<ScheduleLessonDTO> GetLessonsForDay(DateOnly date)
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
