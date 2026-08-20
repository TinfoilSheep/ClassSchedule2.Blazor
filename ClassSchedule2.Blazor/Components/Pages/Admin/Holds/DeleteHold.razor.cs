using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Holds
{
    public partial class DeleteHold
    {
        [Inject]
        private IHoldService _holdService { get; set; } = default!;

        [Parameter]
        public HoldDTO? UpdateHold { get; set; }

        [Parameter]
        public EventCallback OnDeleted { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private bool _isDeleting;

        private async Task DeleteAsync()
        {
            if (_isDeleting || UpdateHold is null)
            {
                return;
            }

            _isDeleting = true;

            try
            {
                var success = await _holdService.Delete(UpdateHold!.Id);

                if (success)
                {
                    await OnDeleted.InvokeAsync();
                }
            }
            finally
            {
                _isDeleting = false;
            }
        }

        private async Task Cancel()
        {
            if (_isDeleting)
            {
                return;
            }

            await OnCancel.InvokeAsync();
        }
    }
}
