using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.NonTeachingDays
{
    public partial class DeleteNonTeachingDay
    {
        [Inject]
        private INonTeachingDayService _nonTeachingDayService { get; set; } = default!;

        [Parameter]
        public NonTeachingDayDTO? NonTeachingDay { get; set; }

        [Parameter]
        public EventCallback OnDeleted { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private bool _isDeleting;

        private async Task DeleteAsync()
        {
            if (_isDeleting || NonTeachingDay is null)
            {
                return;
            }

            _isDeleting = true;

            try
            {
                var success = await _nonTeachingDayService.DeleteNonTeachingDayAsync(NonTeachingDay!.Id);

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
