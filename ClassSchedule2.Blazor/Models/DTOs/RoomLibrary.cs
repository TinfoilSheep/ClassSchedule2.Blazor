namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class RoomLibrary
    {
        public record RoomDTO(Guid Id, string Name, int? Capacity);

        public record CreateRoomDTO(string Name, int? Capacity);
    }
}
