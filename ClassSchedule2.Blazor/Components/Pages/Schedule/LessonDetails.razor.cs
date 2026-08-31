using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using static ClassSchedule2.Blazor.Models.DTOs.LessonLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Schedule
{
    public partial class LessonDetails
    {
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
        [Inject] private ILessonService LessonService { get; set; } = default!;

        [Parameter, EditorRequired]
        public LessonDTO Lesson { get; set; } = default!;

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private LessonInfoModalMode _modalMode;

        private bool _isLoading = true;

        public List<MinimalUserInformationDTO> Students = [];
        public int StudentsCount { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await Load();
            _isLoading = false;
        }

        private async Task Load()
        {
            Students = await LessonService.GetAllStudents(Lesson.Id);
            StudentsCount = Students.Count;
        }

        private void OpenStudentListModal()
        {
            _modalMode = LessonInfoModalMode.Students;
        }
        private void OpenNoteModal()
        {
            _modalMode = LessonInfoModalMode.Note;
        }

        private void OpenAbsenceModal()
        {
            _modalMode = LessonInfoModalMode.Absence;
        }

        private void CloseModals()
        {
            _modalMode = LessonInfoModalMode.None;
        }

        private async Task HandleSaved()
        {
            CloseModals();
            await Load();
            StateHasChanged();
        }
    }
}
