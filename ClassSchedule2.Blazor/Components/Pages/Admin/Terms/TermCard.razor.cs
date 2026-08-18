using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Terms
{
    public partial class TermCard
    {
        [Inject] private ITermService _termService { get; set; } = default!;
        private CrudModalMode _modalMode;
        private List<TermDTO> _terms = [];
        private bool _isLoading;

        private TermDTO? _selectedTerm;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await LoadTermsAsync();

            StateHasChanged();
        }

        private async Task LoadTermsAsync()
        {
            _isLoading = true;

            try
            {
                _terms = await _termService.GetAllTermsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"TermCard fejl: {ex}");
                _terms = [];
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OpenCreateModal()
        {
            _selectedTerm = null;
            _modalMode = CrudModalMode.Create;
        }

        private void OpenEditModal(TermDTO term)
        {
            _selectedTerm = term;
            _modalMode = CrudModalMode.Edit;
        }

        private void OpenDeleteModal(TermDTO term)
        {
            _selectedTerm = term;
            _modalMode = CrudModalMode.Delete;
        }

        private void CloseModals()
        {
            _modalMode = CrudModalMode.None;
            _selectedTerm = null;
        }

        private async Task HandleSaved()
        {
            CloseModals();
            await LoadTermsAsync();
            StateHasChanged();
        }
    }
}
