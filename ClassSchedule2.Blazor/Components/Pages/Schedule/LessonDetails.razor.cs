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
        [Inject] private NavigationManager Navigation { get; set; } = default!;


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

        private void ShowTeacherProfile(MinimalUserInformationDTO user)
        {
            Navigation.NavigateTo($"/profile/{user.UserId}");
        }

        private async Task OpenStudentListModal()
        {
            _modalMode = LessonInfoModalMode.Students;
        }
        private async Task OpenNoteModal()
        {
            _modalMode = LessonInfoModalMode.Note;
        }

        private async Task OpenAbsenceModal()
        {
            _modalMode = LessonInfoModalMode.Absence;
        }

        private async Task CloseModals()
        {
            await RefreshDataAsync();
            _modalMode = LessonInfoModalMode.None;
        }

        private async Task HandleSaved()
        {
            await CloseModals();
            await Load();
            StateHasChanged();
        }

        private async Task RefreshDataAsync()
        {
            LessonDTO? dto = await LessonService.GetLesson(Lesson.Id);
            if (dto != null) Lesson = dto;
        }
    }
}
