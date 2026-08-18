using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.RoomLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Rooms
{
    public partial class DeleteRoom
    {
        [Inject]
        private IRoomService _roomService { get; set; } = default!;

        [Parameter]
        public RoomDTO? Room { get; set; }

        [Parameter]
        public EventCallback OnDeleted { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private bool _isDeleting;

        private async Task DeleteAsync()
        {
            if (_isDeleting || Room is null)
            {
                return;
            }

            _isDeleting = true;

            try
            {
                var success = await _roomService.DeleteRoomAsync(Room.Id);

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
