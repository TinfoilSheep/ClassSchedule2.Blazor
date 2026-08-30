using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.Rooms;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.RoomLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Rooms
{
    public partial class AddRoom
    {
        [Inject]
        private IRoomService _roomService { get; set; } = default!;
        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private CreateRoomFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;


        private async Task HandleSubmitAsync()
        {
            if (_isSubmitting)
            {
                return;
            }

            _isSubmitting = true;
            _errorMessage = null;

            try
            {
                var dto = new CreateRoomDTO(Name: _form.Name, Capacity: _form.Capacity);
                var result = await _roomService.CreateRoomAsync(dto); 

                if (!result)
                {
                    _errorMessage = "lokalet kunne ikke oprettes. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under oprettelsen af lokalet.";
            }
            finally
            {
                _isSubmitting = false;
            }
        }


        private async Task Cancel()
        {
            if (_isSubmitting)
            {
                return;
            }

            await OnCancel.InvokeAsync();
        }
    }
}
