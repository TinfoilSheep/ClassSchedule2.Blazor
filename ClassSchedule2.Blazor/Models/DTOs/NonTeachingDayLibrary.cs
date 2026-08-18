namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class NonTeachingDayLibrary
    {
        public record NonTeachingDayDTO(Guid Id, DateOnly StartDate, DateOnly EndDate, string Reason);
        public record CreateNonTeachingDayDTO(DateOnly StartDate, DateOnly EndDate, string Reason);
    }
}
