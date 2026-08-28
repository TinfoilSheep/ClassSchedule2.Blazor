using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class ScheduleLibrary
    {
        public record ScheduleLessonDTO(
            Guid Id,
            DateOnly Date,
            TimeOnly StartTime,
            TimeOnly EndTime,
            string SubjectName,
            string HoldName,
            string? RoomName,
            string Status,
            List<string> Teachers);
            //List<MinimalUserInformationDTO> Teachers);

        public record GetScheduleLessonDTO(Guid TargetId, DateOnly From, DateOnly To);
    }
}
