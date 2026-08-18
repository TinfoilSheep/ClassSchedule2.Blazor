using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Periods
{
    public partial class PeriodCard
    {
        [Inject] private IPeriodService _periodService { get; set; } = default!;
        private CrudModalMode _modalMode;
        private List<PeriodDTO> _periods = [];
        private bool _isLoading;

        private PeriodDTO? _selectedPeriod;

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
                _periods = await _periodService.GetAllPeriodsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PeriodCard fejl: {ex}");
                _periods = [];
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OpenCreateModal()
        {
            _selectedPeriod = null;
            _modalMode = CrudModalMode.Create;
        }

        private void OpenEditModal(PeriodDTO period)
        {
            _selectedPeriod = period;
            _modalMode = CrudModalMode.Edit;
        }

        private void OpenDeleteModal(PeriodDTO period)
        {
            _selectedPeriod = period;
            _modalMode = CrudModalMode.Delete;
        }

        private void CloseModals()
        {
            _modalMode = CrudModalMode.None;
            _selectedPeriod = null;
        }

        private async Task HandleSaved()
        {
            CloseModals();
            await LoadPeriodsAsync();
            StateHasChanged();
        }
    }
}
