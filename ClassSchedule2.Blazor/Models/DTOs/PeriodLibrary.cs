namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class PeriodLibrary
    {
        public record PeriodDTO(Guid Id, string Name, TimeOnly StartTime, TimeOnly EndTime, int SortOrder);
        public record CreatePeriodDTO(string Name, TimeOnly StartTime, TimeOnly EndTime, int SortOrder);
    }
}
