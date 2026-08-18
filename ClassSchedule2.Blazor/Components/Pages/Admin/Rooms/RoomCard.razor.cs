using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.RoomLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Rooms
{
    public partial class RoomCard
    {
        [Inject] private IRoomService _roomService { get; set; } = default!;
        private CrudModalMode _modalMode;
        private List<RoomDTO> _rooms = [];
        private bool _isLoading;

        private RoomDTO? _selectedRoom;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await LoadRoomsAsync();

            StateHasChanged();
        }

        private async Task LoadRoomsAsync()
        {
            _isLoading = true;

            try
            {
                _rooms = await _roomService.GetAllRoomsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RoomCard fejl: {ex}");
                _rooms = [];
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OpenCreateModal()
        {
            _selectedRoom = null;
            _modalMode = CrudModalMode.Create;
        }

        private void OpenEditModal(RoomDTO room)
        {
            _selectedRoom = room;
            _modalMode = CrudModalMode.Edit;
        }

        private void OpenDeleteModal(RoomDTO room)
        {
            _selectedRoom = room;
            _modalMode = CrudModalMode.Delete;
        }

        private void CloseModals()
        {
            _modalMode = CrudModalMode.None;
            _selectedRoom = null;
        }

        private async Task HandleSaved()
        {
            CloseModals();
            await LoadRoomsAsync();
            StateHasChanged();
        }
    }
}
