using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.NonTeachingDays
{
    public partial class NonTeachingDayCard
    {
        [Inject] private INonTeachingDayService _nonTeachingDayService { get; set; } = default!;
        private CrudModalMode _modalMode;
        private List<NonTeachingDayDTO> _nonTeachingDays = [];
        private bool _isLoading;

        private NonTeachingDayDTO? _selectedNonTeachingDay;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await LoadNonTeachingDaysAsync();

            StateHasChanged();
        }

        private async Task LoadNonTeachingDaysAsync()
        {
            _isLoading = true;

            try
            {
                _nonTeachingDays = await _nonTeachingDayService.GetAllNonTeachingDaysAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NonTeachingDayCard fejl: {ex}");
                _nonTeachingDays = [];
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OpenCreateModal()
        {
            _selectedNonTeachingDay = null;
            _modalMode = CrudModalMode.Create;
        }

        private void OpenEditModal(NonTeachingDayDTO nonTeachingDay)
        {
            _selectedNonTeachingDay = nonTeachingDay;
            _modalMode = CrudModalMode.Edit;
        }

        private void OpenDeleteModal(NonTeachingDayDTO nonTeachingDay)
        {
            _selectedNonTeachingDay = nonTeachingDay;
            _modalMode = CrudModalMode.Delete;
        }

        private void CloseModals()
        {
            _modalMode = CrudModalMode.None;
            _selectedNonTeachingDay = null;
        }

        private async Task HandleSaved()
        {
            CloseModals();
            await LoadNonTeachingDaysAsync();
            StateHasChanged();
        }
    }
}
