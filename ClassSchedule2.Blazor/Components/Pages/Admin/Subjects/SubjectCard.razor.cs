using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Subjects
{
    public partial class SubjectCard
    {
        [Inject] private ISubjectService SubjectService { get; set; } = default!;
        private SubjectModalMode _modalMode;
        private List<SubjectDTO> _subjects = [];
        private bool _isLoading;

        private SubjectDTO? _selectedSubject;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await LoadSubjectsAsync();

            StateHasChanged();
        }

        private async Task LoadSubjectsAsync()
        {
            _isLoading = true;

            try
            {
                _subjects = await SubjectService.GetAllSubjectsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SubjectCard fejl: {ex}");
                _subjects = [];
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OpenCreateModal()
        {
            _selectedSubject = null;
            _modalMode = SubjectModalMode.Create;
        }

        private void OpenEditModal(SubjectDTO subject)
        {
            _selectedSubject = subject;
            _modalMode = SubjectModalMode.Edit;
        }

        private void OpenDeleteModal(SubjectDTO subject)
        {
            _selectedSubject = subject;
            _modalMode = SubjectModalMode.Delete;
        }

        private void CloseModals()
        {
            _modalMode = SubjectModalMode.None;
            _selectedSubject = null;
        }

        private async Task HandleSaved()
        {
            CloseModals();
            await LoadSubjectsAsync();
            StateHasChanged();
        }
    }
}
