using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.AdminDashboard
{
    public partial class AdminDashboard
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private IHoldService HoldService { get; set; } = default!;
        [Inject] private ISubjectService SubjectService { get; set; } = default!;
        [Inject] private IRoomService RoomService { get; set; } = default!;
        [Inject] private IStudentGroupService StudentGroupService { get; set; } = default!;
        [Inject] private ITermService TermService { get; set; } = default!;
        [Inject] private IPeriodService PeriodService { get; set; } = default!;
        [Inject] private INonTeachingDayService NonTeachingDayService { get; set; } = default!;

        private bool _isLoading = true;

        private int _studentCount;
        private int _teacherCount;
        private int _holdCount;
        private int _subjectCount;
        private int _roomCount;
        private int _studentGroupCount;
        private int _termCount;
        private int _periodCount;
        private int _nonTeachingDayCount;

        protected override async Task OnParametersSetAsync()
        {
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            _isLoading = true;

            try
            {
                // Tilpas metodenavnene til dine eksisterende services.
                var users = await UserService.GetAllUsersListAsync();
                var holds = await HoldService.GetAll();
                var subjects = await SubjectService.GetAllSubjectsAsync();
                var rooms = await RoomService.GetAllRoomsAsync();
                var studentGroups = await StudentGroupService.GetAll();
                var terms = await TermService.GetAllTermsAsync();
                var periods = await PeriodService.GetAllPeriodsAsync();
                var nonTeachingDays = await NonTeachingDayService.GetAllNonTeachingDaysAsync();


                _studentCount = users.Count(u => u.Role == UserRoles.Student);
                _teacherCount = users.Count(u => u.Role == UserRoles.Teacher);
                _holdCount = holds.Count();
                _subjectCount = subjects.Count();
                _roomCount = rooms.Count();
                _studentGroupCount = studentGroups.Count();
                _termCount = terms.Count();
                _periodCount = periods.Count();
                _nonTeachingDayCount = nonTeachingDays.Count();
            }
            finally
            {
                _isLoading = false;
            }
        }
    }
}