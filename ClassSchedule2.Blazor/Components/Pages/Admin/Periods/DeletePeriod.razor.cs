using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Periods
{
    public partial class DeletePeriod
    {
        [Inject]
        private IPeriodService _periodService { get; set; } = default!;

        [Parameter]
        public PeriodDTO? Period { get; set; }

        [Parameter]
        public EventCallback OnDeleted { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private bool _isDeleting;

        private async Task DeleteAsync()
        {
            if (_isDeleting || Period is null)
            {
                return;
            }

            _isDeleting = true;

            try
            {
                var success = await _periodService.DeletePeriodAsync(Period.Id);

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
