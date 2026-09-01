using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.LessonLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Schedule
{
    public partial class RegisterLessonAbsence
    {
        [Inject] private IAbsenceService AbsenceService { get; set; } = default!;

        [Parameter, EditorRequired] public LessonDTO Lesson { get; set; } = default!;
        [Parameter] public List<MinimalUserInformationDTO> Students { get; set; } = [];

        [Parameter] public EventCallback OnCancel { get; set; }
        [Parameter] public EventCallback OnSaved { get; set; }

        private bool _isLoading = true;
        private bool _isSubmitting;

        private readonly Dictionary<Guid, AbsenceStatus> _studentStatuses = [];

        private int _absenceCount => _studentStatuses.Count(x => x.Value == AbsenceStatus.Absent || x.Value == AbsenceStatus.Sick || x.Value == AbsenceStatus.Excused);

        protected override async Task OnParametersSetAsync()
        {
            await LoadAsync();

            _isLoading = false;
        }

        private async Task LoadAsync()
        {
            var absences = await AbsenceService.GetAllAbsencesFromLesson(Lesson.Id);

            _studentStatuses.Clear();

            foreach (var absence in absences)
            {
                _studentStatuses[absence.StudentIds] = absence.Status;
            }
        }

        private AbsenceStatus? GetStatus(Guid studentId)
        {
            return _studentStatuses.TryGetValue(studentId, out var status) ? status : null;
        }

        private string GetStatusValue(AbsenceStatus? status)
        {
            return status.HasValue ? ((int)status.Value).ToString() : "0";
        }

        private void SetStatus(Guid studentId, string? value)
        {
            if (!int.TryParse(value, out var statusValue))
            {
                return;
            }

            // Fremmødt
            if (statusValue == 0)
            {
                _studentStatuses.Remove(studentId);
                return;
            }

            if (Enum.IsDefined(typeof(AbsenceStatus), statusValue))
            {
                _studentStatuses[studentId] = (AbsenceStatus)statusValue;
            }
        }

        private string GetStatusClass(AbsenceStatus? status)
        {
            return status switch
            {
                AbsenceStatus.Absent => "w-32 cursor-pointer rounded-lg border border-red-200 bg-red-50 px-3 py-1.5 text-xs font-semibold text-red-700 outline-none focus:border-red-400 focus:ring-2 focus:ring-red-400/20 dark:border-red-500/20 dark:bg-red-500/10 dark:text-red-400",

                AbsenceStatus.Sick => "w-32 cursor-pointer rounded-lg border border-orange-200 bg-orange-50 px-3 py-1.5 text-xs font-semibold text-orange-700 outline-none focus:border-orange-400 focus:ring-2 focus:ring-orange-400/20 dark:border-orange-500/20 dark:bg-orange-500/10 dark:text-orange-400",

                AbsenceStatus.Excused => "w-32 cursor-pointer rounded-lg border border-amber-200 bg-amber-50 px-3 py-1.5 text-xs font-semibold text-amber-700 outline-none focus:border-amber-400 focus:ring-2 focus:ring-amber-400/20 dark:border-amber-500/20 dark:bg-amber-500/10 dark:text-amber-400",

                _ => "w-32 cursor-pointer rounded-lg border border-slate-200 bg-white px-3 py-1.5 text-xs font-semibold text-slate-600 outline-none focus:border-amber-400 focus:ring-2 focus:ring-amber-400/20 dark:border-slate-700 dark:bg-slate-950 dark:text-slate-300"
            };
        }

        private static string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "?";
            }

            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                return parts[0][..1].ToUpper();
            }

            return $"{parts[0][0]}{parts[^1][0]}".ToUpper();
        }

        private async Task HandleSubmitAsync()
        {
            if (_isSubmitting)
            {
                return;
            }

            _isSubmitting = true;

            try
            {
                var dtos = _studentStatuses.Select(x => new AbsenceLibrary.SetAbsenceDTO(x.Key, x.Value)).ToList();

                var success = await AbsenceService.RegisterAbsence(Lesson.Id, dtos);

                if (success)
                {
                    await OnSaved.InvokeAsync();
                }
            }
            finally
            {
                _isSubmitting = false;
            }
        }

        private async Task Cancel()
        {
            if (_isSubmitting)
            {
                return;
            }

            await OnCancel.InvokeAsync();
        }
    }
}