using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Forms.Holds;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.StudentGroupLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Holds
{
    public partial class AddHold
    {
        [Inject] private IHoldService _holdService { get; set; } = default!;
        [Inject] private ITermService _termService { get; set; } = default!;
        [Inject] private ISubjectService _subjectService { get; set; } = default!;
        [Inject] private IStudentGroupService _studentGroupService { get; set; } = default!;
        [Inject] private IStudentGroupMemberService _studentGroupMemberService { get; set; } = default!;
        [Inject] private IUserService _userService { get; set; } = default!;

        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private CreateHoldFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;
        private bool _isLoading;
        private string _studentSearchText = "";
        private string _selectedStudentSearchText = "";

        private string _teacherSearchText = "";
        private string _selectedTeacherSearchText = "";

        private Guid _selectedStudentGroupId = Guid.Empty;
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

        private async Task AddSelectedStudentGroup()
        {
            if (_selectedStudentGroupId == Guid.Empty)
            {
                return;
            }

            var studentGroup = _studentGroups
                .FirstOrDefault(x => x.Id == _selectedStudentGroupId);

            if (studentGroup is null)
            {
                return;
            }

            try
            {
                var students = await _studentGroupMemberService
                    .GetStudentsAsync(studentGroup.Id);

                // Fjern eventuelle elever, der allerede er valgt
                var studentsToAdd = students
                    .Where(student => !_selectedStudents.Contains(student))
                    .ToList();

                // Fjern eleverne fra tilgængelige elever
                _allStudents = _allStudents
                    .Except(studentsToAdd)
                    .ToList();

                // Tilføj eleverne til valgte elever
                _selectedStudents.AddRange(studentsToAdd);

                // Nulstil dropdown
                _selectedStudentGroupId = Guid.Empty;

                // Ryd eventuelle søgninger
                _studentSearchText = "";
                _selectedStudentSearchText = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved tilføjelse af elevgruppe: {ex}");
                _errorMessage = "Elevgruppen kunne ikke tilføjes. Prøv igen.";
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

        private void ClearStudentSearch()
        {
            _studentSearchText = "";
        }

        private void ClearSelectedStudentSearch()
        {
            _selectedStudentSearchText = "";
        }

        private void ClearTeacherSearch()
        {
            _teacherSearchText = "";
        }

        private void ClearSelectedTeacherSearch()
        {
            _selectedTeacherSearchText = "";
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

        private IEnumerable<MinimalUserInformationDTO> FilteredStudents
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_studentSearchText))
                {
                    return _allStudents;
                }

                var search = _studentSearchText.Trim();

                return _allStudents.Where(student =>
                    student.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
        }

        private IEnumerable<MinimalUserInformationDTO> FilteredSelectedStudents
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_selectedStudentSearchText))
                {
                    return _selectedStudents;
                }

                var search = _selectedStudentSearchText.Trim();

                return _selectedStudents.Where(student =>
                    student.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
        }

        private IEnumerable<MinimalUserInformationDTO> FilteredTeachers
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_teacherSearchText))
                {
                    return _allTeachers;
                }

                var search = _teacherSearchText.Trim();

                return _allTeachers.Where(teacher =>
                    teacher.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
        }

        private IEnumerable<MinimalUserInformationDTO> FilteredSelectedTeachers
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_selectedTeacherSearchText))
                {
                    return _selectedTeachers;
                }

                var search = _selectedTeacherSearchText.Trim();

                return _selectedTeachers.Where(teacher =>
                    teacher.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
