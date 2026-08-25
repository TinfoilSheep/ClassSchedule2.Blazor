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
    public partial class EditHold
    {
        [Inject]
        private IHoldService _holdService { get; set; } = default!;

        [Inject]
        private IHoldMemberService _holdMemberService { get; set; } = default!;

        [Inject]
        private ISubjectService _subjectService { get; set; } = default!;

        [Inject]
        private ITermService _termService { get; set; } = default!;

        [Inject]
        private IStudentGroupService _studentGroupService { get; set; } = default!;

        [Inject]
        private IStudentGroupMemberService _studentGroupMemberService { get; set; } = default!;

        [Inject]
        private IUserService _userService { get; set; } = default!;


        [Parameter]
        public HoldDTO? UpdateHold { get; set; }

        [Parameter]
        public EventCallback OnSaved { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }


        private EditHoldFormModel _form = new();

        private bool _isSubmitting;
        private bool _isLoading;

        private string? _errorMessage;


        private List<TermDTO> _terms = [];
        private List<SubjectDTO> _subjects = [];
        private List<StudentGroupDTO> _studentGroups = [];


        private List<MinimalUserInformationDTO> _selectedTeachers = [];
        private List<MinimalUserInformationDTO> _selectedStudents = [];

        private List<MinimalUserInformationDTO> _allTeachers = [];
        private List<MinimalUserInformationDTO> _allStudents = [];


        private Guid _selectedStudentGroupId = Guid.Empty;


        private string _studentSearchText = "";
        private string _selectedStudentSearchText = "";

        private string _teacherSearchText = "";
        private string _selectedTeacherSearchText = "";


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await LoadAsync();

            StateHasChanged();
        }


        private async Task LoadAsync()
        {
            if (UpdateHold is null)
            {
                return;
            }

            _isLoading = true;
            _errorMessage = null;

            try
            {
                _terms = await _termService.GetAllTermsAsync();
                _subjects = await _subjectService.GetAllSubjectsAsync();
                _studentGroups = await _studentGroupService.GetAll();

                _allTeachers = (await _userService.GetAllUsersListAsync(UserRoles.Teacher))
                    .Select(t => new MinimalUserInformationDTO($"{t.FirstName} {t.LastName}", t.Id)).ToList();

                _allStudents = (await _userService.GetAllUsersListAsync(UserRoles.Student))
                    .Select(s => new MinimalUserInformationDTO($"{s.FirstName} {s.LastName}", s.Id)).ToList();

                _selectedTeachers = await _holdMemberService.GetTeachersAsync(UpdateHold.Id);

                _selectedStudents = await _holdMemberService.GetStudentsAsync(UpdateHold.Id);

                var selectedTeacherIds = _selectedTeachers.Select(t => t.UserId).ToHashSet();

                var selectedStudentIds = _selectedStudents.Select(s => s.UserId).ToHashSet();


                _allTeachers = _allTeachers.Where(t => !selectedTeacherIds.Contains(t.UserId)).ToList();

                _allStudents = _allStudents.Where(s => !selectedStudentIds.Contains(s.UserId)).ToList();

                _form.Id = UpdateHold.Id;
                _form.Name = UpdateHold.Name;

                _form.SubjectId = UpdateHold.SubjectId;
                _form.SubjectName = UpdateHold.SubjectName ?? _subjects.FirstOrDefault(s => s.Id == UpdateHold.SubjectId)?.Name ?? "";

                _form.TermId = UpdateHold.TermId;
                _form.TermName = UpdateHold.TermName ?? _terms.FirstOrDefault(t => t.Id == UpdateHold.TermId)?.Name ?? "";

                _form.Teachers = _selectedTeachers;
                _form.Students = _selectedStudents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EditHold fejl: {ex}");

                _terms = [];
                _subjects = [];
                _studentGroups = [];
                _allTeachers = [];
                _allStudents = [];

                _selectedTeachers = [];
                _selectedStudents = [];

                _errorMessage = "Der opstod en fejl under indlæsningen af holdet.";
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void AddStudent(MinimalUserInformationDTO student)
        {
            _allStudents.Remove(student);
            _selectedStudents.Add(student);

            _form.Students = _selectedStudents;
        }


        private void RemoveStudent(MinimalUserInformationDTO student)
        {
            _selectedStudents.Remove(student);
            _allStudents.Add(student);

            _form.Students = _selectedStudents;
        }


        private void ClearStudentSearch()
        {
            _studentSearchText = "";
        }


        private void ClearSelectedStudentSearch()
        {
            _selectedStudentSearchText = "";
        }

        private void AddTeacher(MinimalUserInformationDTO teacher)
        {
            _allTeachers.Remove(teacher);
            _selectedTeachers.Add(teacher);

            _form.Teachers = _selectedTeachers;
        }


        private void RemoveTeacher(MinimalUserInformationDTO teacher)
        {
            _selectedTeachers.Remove(teacher);
            _allTeachers.Add(teacher);

            _form.Teachers = _selectedTeachers;
        }


        private void ClearTeacherSearch()
        {
            _teacherSearchText = "";
        }


        private void ClearSelectedTeacherSearch()
        {
            _selectedTeacherSearchText = "";
        }

        private async Task AddSelectedStudentGroup()
        {
            if (_selectedStudentGroupId == Guid.Empty || _isLoading || _isSubmitting)
            {
                return;
            }

            try
            {
                var studentGroup = await _studentGroupMemberService.GetStudentsAsync(_selectedStudentGroupId);

                if (studentGroup.Count == 0)
                {
                    return;
                }

                var selectedIds = _selectedStudents.Select(s => s.UserId).ToHashSet();

                var newStudents = studentGroup.Where(s => !selectedIds.Contains(s.UserId)).ToList();


                foreach (var student in newStudents)
                {
                    _allStudents.Remove(student);
                    _selectedStudents.Add(student);
                }


                _form.Students = _selectedStudents;

                _selectedStudentGroupId = Guid.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved tilføjelse af elevgruppe: {ex}");

                _errorMessage = "Elevgruppens elever kunne ikke tilføjes.";
            }
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

                return _allStudents.Where(student => student.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
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

                return _selectedStudents.Where(student => student.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
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

                return _allTeachers.Where(teacher => teacher.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
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

                return _selectedTeachers.Where(teacher => teacher.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
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
                _form.Teachers = _selectedTeachers;
                _form.Students = _selectedStudents;

                List<Guid> teacherIds = _form.Teachers.Select(t => t.UserId).ToList();
                List<Guid> studentIds = _form.Students.Select(s => s.UserId).ToList();

                var dto = new HoldDTO(
                    _form.Id,
                    _form.Name,
                    _form.SubjectId,
                    _form.TermId,
                    _form.SubjectName,
                    _form.TermName,
                    teacherIds,
                    studentIds);

                var result = await _holdService.Update(dto);

                if (!result)
                {
                    _errorMessage = "Hold kunne ikke opdateres. Prøv igen.";

                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under opdateringen af holdet.";
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