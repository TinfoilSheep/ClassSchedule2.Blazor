using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Forms.Holds;
using ClassSchedule2.Blazor.Models.Forms.NonTeachingDays;
using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.StudentGroupLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Holds
{
    public partial class AddHold
    {
        [Inject] private IHoldService _holdService { get; set; } = default!;
        [Inject] private IHoldMemberService _holdMemberService { get; set; }
        [Inject] private ITermService _termService { get; set; } = default!;
        [Inject] private ISubjectService _subjectService { get; set; } = default!;
        [Inject] private IStudentGroupService _studentGroupService { get; set; }
        [Inject] private IStudentGroupMemberService _studentGroupMemberService { get; set; }
        [Inject] private IUserService _userService { get; set; }

        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private CreateHoldFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;
        private bool _isLoading;

        private List<TermDTO> _terms = [];
        private List<SubjectDTO> _subjects = [];

        private List<MinimalUserInformationDTO> _selectedTeachers = [];
        private List<MinimalUserInformationDTO> _selectedStudents = [];
        private List<MinimalUserInformationDTO> _allTeachers = [];
        private List<MinimalUserInformationDTO> _allStudents = [];

        private List<StudentGroupDTO> _studentGroups = [];

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await LoadPeriodsAsync();

            StateHasChanged();
        }

        private async Task LoadPeriodsAsync()
        {
            _isLoading = true;

            try
            {
                _terms = await _termService.GetAllTermsAsync();
                _subjects = await _subjectService.GetAllSubjectsAsync();

                _allTeachers = (await _userService.GetAllUsersListAsync(UserRoles.Teacher))
                    .Select(t => new MinimalUserInformationDTO($"{t.FirstName} {t.LastName}", t.Id)).ToList();

                _allStudents = (await _userService.GetAllUsersListAsync(UserRoles.Student))
                    .Select(s => new MinimalUserInformationDTO($"{s.FirstName} {s.LastName}", s.Id)).ToList();

                _studentGroups = await _studentGroupService.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HoldCard fejl: {ex}");
                _terms = [];
                _subjects = [];
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void AddTeacher(MinimalUserInformationDTO teacher)
        {
            _allTeachers.Remove(teacher);
            _selectedTeachers.Add(teacher);
        }

        private void RemoveTeacher(MinimalUserInformationDTO teacher)
        {
            _selectedTeachers.Remove(teacher);
            _allTeachers.Add(teacher);
        }

        private void AddStudent(MinimalUserInformationDTO teacher)
        {
            _allStudents.Remove(teacher);
            _selectedStudents.Add(teacher);
        }

        private void RemoveStudent(MinimalUserInformationDTO teacher)
        {
            _selectedStudents.Remove(teacher);
            _allStudents.Add(teacher);
        }

        private async Task AddStudentGroup(StudentGroupDTO studentGroup)
        {
            // Merge all students back into _allStudents
            _allStudents = _allStudents.Union(_selectedStudents).ToList();

            List<MinimalUserInformationDTO> studentGroupMembers = await _studentGroupMemberService.GetStudentsAsync(studentGroup.Id);

            _allStudents = _allStudents.Except(studentGroupMembers).ToList();
            _selectedStudents = _selectedStudents.Union(studentGroupMembers).ToList();
        }

        private async Task HandleSubmitAsync()
        {
            if (_isSubmitting || _isLoading)
            {
                return;
            }

            _isSubmitting = true;
            _errorMessage = null;

            try
            {
                List<Guid> teacherIds = _selectedTeachers.Select(t => t.UserId).ToList();
                List<Guid> studentIds = _selectedStudents.Select(s => s.UserId).ToList();

                var dto = new CreateHoldDTO(_form.Name, _form.TermId, _form.SubjectId, teacherIds, studentIds);
                var result = await _holdService.Create(dto);

                if (!result)
                {
                    _errorMessage = "Hold kunne ikke oprettes. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under oprettelsen af Hold.";
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
