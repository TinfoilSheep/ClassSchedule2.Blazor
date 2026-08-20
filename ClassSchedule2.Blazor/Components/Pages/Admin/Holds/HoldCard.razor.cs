using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Holds
{
    public partial class HoldCard
    {
        [Inject] private IHoldService _holdService { get; set; } = default!;
        private CrudModalMode _modalMode;
        private List<HoldDTO> _holdDTOs = [];
        private bool _isLoading;

        private HoldDTO? _selectedHold;

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
                _holdDTOs = await _holdService.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HoldCard fejl: {ex}");
                _holdDTOs = [];
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OpenCreateModal()
        {
            _selectedHold = null;
            _modalMode = CrudModalMode.Create;
        }

        private void OpenEditModal(HoldDTO holdDTO)
        {
            _selectedHold = holdDTO;
            _modalMode = CrudModalMode.Edit;
        }

        private void OpenDeleteModal(HoldDTO holdDTO)
        {
            _selectedHold = holdDTO;
            _modalMode = CrudModalMode.Delete;
        }

        private void CloseModals()
        {
            _modalMode = CrudModalMode.None;
            _selectedHold = null;
        }

        private async Task HandleSaved()
        {
            CloseModals();
            await Load();
            StateHasChanged();
        }
    }
}
