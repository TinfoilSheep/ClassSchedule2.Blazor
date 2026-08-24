using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Forms.Holds;
using ClassSchedule2.Blazor.Models.Forms.NonTeachingDays;
using ClassSchedule2.Blazor.Models.Forms.StudentGroup;
using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.StudentGroupLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.StudentGroup
{
    public partial class AddStudentGroup
    {
        [Inject]
        private IStudentGroupService _studentGroupService { get; set; } = default!;
        [Inject]
        private IUserService _userService { get; set; } = default!;

        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private CreateStudentGroupFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;
        private bool _isLoading;

        private List<MinimalUserInformationDTO> _allStudents = [];
        private List<MinimalUserInformationDTO> _selectedStudents = [];

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

            try
            {
                _allStudents = (await _userService.GetAllUsersListAsync(UserRoles.Student))
                    .Select(s => new MinimalUserInformationDTO($"{s.FirstName} {s.LastName}", s.Id)).ToList();
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

        private void AddStudent(MinimalUserInformationDTO student)
        {
            _allStudents.Remove(student);
            _selectedStudents.Add(student);
        }

        private void RemoveStudent(MinimalUserInformationDTO student)
        {
            _selectedStudents.Remove(student);
            _allStudents.Add(student);
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
                List<Guid> studentIds = _selectedStudents.Select(s => s.UserId).ToList();
                var dto = new CreateStudentGroupDTO(_form.Name, studentIds);
                var result = await _studentGroupService.Create(dto);

                if (!result)
                {
                    _errorMessage = "Klassen kunne ikke oprettes. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
                
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under oprettelsen af Klassen.";
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
