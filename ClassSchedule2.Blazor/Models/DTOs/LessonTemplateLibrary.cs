namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class LessonTemplateLibrary
    {
        public record LessonTemplateDTO(Guid Id, int WeekDay, DateOnly ValidFrom, DateOnly ValidTo, string HoldName, string PeriodName, string? RoomName = "");
        public record CreateLessonTemplateDTO(int WeekDay, DateOnly ValidFrom, DateOnly ValidTo, Guid HoldId, Guid PeriodId, Guid? RoomId = null);
        public record UpdateLessonTemplateDTO(Guid Id, int WeekDay, DateOnly ValidFrom, DateOnly ValidTo, Guid HoldId, Guid PeriodId, Guid? RoomId = null);
    }
}
