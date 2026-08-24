using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.StudentGroupLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.StudentGroup
{
    public partial class StudentGroupCard
    {
        [Inject] private IStudentGroupService _studentGroupService { get; set; } = default!;
        private CrudModalMode _modalMode;
        private List<StudentGroupDTO> _studentGroupDTOs = [];
        private bool _isLoading;

        private StudentGroupDTO? _selectedStudentGroup;

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
                _studentGroupDTOs = await _studentGroupService.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"StudentGroupCard fejl: {ex}");
                _studentGroupDTOs = [];
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OpenCreateModal()
        {
            _selectedStudentGroup = null;
            _modalMode = CrudModalMode.Create;
        }

        private void OpenEditModal(StudentGroupDTO dto)
        {
            _selectedStudentGroup = dto;
            _modalMode = CrudModalMode.Edit;
        }

        private void OpenDeleteModal(StudentGroupDTO dto)
        {
            _selectedStudentGroup = dto;
            _modalMode = CrudModalMode.Delete;
        }

        private void CloseModals()
        {
            _modalMode = CrudModalMode.None;
            _selectedStudentGroup = null;
        }

        private async Task HandleSaved()
        {
            CloseModals();
            await Load();
            StateHasChanged();
        }
    }
}
