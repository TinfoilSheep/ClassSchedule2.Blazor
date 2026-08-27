namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class ScheduleLessonLibrary
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

        public record GetScheduleLessonDTO(Guid TargetId, DateOnly From, DateOnly To);
    }
}
