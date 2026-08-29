using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.ScheduleLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Schedule
{
    public partial class LessonDetails
    {
        [Inject]
        private ILessonService LessonService { get; set; } = default!;

        [Parameter, EditorRequired]
        public ScheduleLessonDTO Lesson { get; set; } = default!;

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private bool _isLoading = true;
        private bool _showStudentModal = false;

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
            _showStudentModal = true;
        }

        private void CloseStudentListModal()
        {
            _showStudentModal = false;
        }
    }
}
