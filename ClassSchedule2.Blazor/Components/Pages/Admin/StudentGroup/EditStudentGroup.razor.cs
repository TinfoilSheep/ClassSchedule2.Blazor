using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Forms.Holds;
using ClassSchedule2.Blazor.Models.Forms.NonTeachingDays;
using ClassSchedule2.Blazor.Models.Forms.StudentGroup;
using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.StudentGroupLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.StudentGroup
{
    public partial class EditStudentGroup
    {
        [Inject]
        private IStudentGroupService _studentGroupService { get; set; } = default!;
        [Inject]
        private IStudentGroupMemberService _studentGroupMemberService { get; set; } = default!;
        [Inject]
        private IUserService _userService { get; set; } = default!;

        [Parameter]
        public StudentGroupDTO? StudentGroup { get; set; }

        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private EditStudentGroupFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;
        private bool _isLoading;
        private string _studentSearchText = "";
        private string _selectedStudentSearchText = "";

        private List<MinimalUserInformationDTO> _allStudents = [];

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await Load();

            StateHasChanged();
        }

        private async Task Load()
        {
            _isLoading = true;

            if (StudentGroup is null)
            {
                return;
            }

            try
            {
                _form.Students = await _studentGroupMemberService.GetStudentsAsync(StudentGroup.Id);

                _allStudents = (await _userService.GetAllUsersListAsync(UserRoles.Student))
                    .Select(s => new MinimalUserInformationDTO($"{s.FirstName} {s.LastName}", s.Id))
                    .Where(s => _form.Students.Contains(s) == false).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"StudentGroupCard fejl: {ex}");
                _allStudents = [];
            }
            finally
            {
                _isLoading = false;
            }
        }

        protected override void OnParametersSet()
        {
            if (StudentGroup is null)
            {
                return;
            }

            _form.Id = StudentGroup!.Id;
            _form.Name = StudentGroup!.Name;

            _errorMessage = null;
        }

        private void AddStudent(MinimalUserInformationDTO student)
        {
            _allStudents.Remove(student);
            _form.Students.Add(student);
        }

        private void RemoveStudent(MinimalUserInformationDTO student)
        {
            _form.Students.Remove(student);
            _allStudents.Add(student);
        }

        private void ClearStudentSearch()
        {
            _studentSearchText = "";
        }

        private void ClearSelectedStudentSearch()
        {
            _selectedStudentSearchText = "";
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
                List<Guid> students = _form.Students.Select(s => s.UserId).ToList();
                var dto = new EditStudentGroupDTO(_form.Id, _form.Name, students);
                var result = await _studentGroupService.Update(dto);

                if (!result)
                {
                    _errorMessage = "Elevgruppen kunne ikke opdateres. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under opdateringen af Elevgruppen.";
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
                    return _form.Students;
                }

                var search = _selectedStudentSearchText.Trim();

                return _form.Students.Where(student =>
                    student.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
        }
    }
}
