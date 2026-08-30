using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.Rooms;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.RoomLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Rooms
{
    public partial class EditRoom
    {
        [Inject]
        private IRoomService _roomService { get; set; } = default!;

        [Parameter]
        public RoomDTO? Room { get; set; }
        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private EditRoomFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;


        protected override void OnParametersSet()
        {
            if (Room is null)
            {
                return;
            }

            _form.Id = Room.Id;
            _form.Name = Room.Name;
            _form.Capacity = Room.Capacity;

            _errorMessage = null;
        }


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
                var dto = new RoomDTO(Id: _form.Id, Name: _form.Name, Capacity: _form.Capacity);
                var result = await _roomService.UpdateRoomAsync(dto);

                if (!result)
                {
                    _errorMessage = "Lokale kunne ikke opdateres. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under opdateringen af lokale.";
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
