using static ClassSchedule2.Blazor.Models.DTOs.RoomLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IRoomService
    {
        public Task<List<RoomDTO>> GetAllRoomsAsync();
        public Task<RoomDTO?> GetRoomByIdAsync(Guid roomId);
        public Task<RoomDTO?> CreateRoomAsync(CreateRoomDTO dto);
        public Task<RoomDTO?> UpdateRoomAsync(RoomDTO dto);
        public Task<bool> DeleteRoomAsync(Guid roomId);
    }
}
