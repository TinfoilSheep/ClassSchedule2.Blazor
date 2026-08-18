namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class TermLibrary
    {
        public record TermDTO(Guid Id, string Name, DateOnly StartDate, DateOnly EndDate);
        public record CreateTermDTO(string Name, DateOnly StartDate, DateOnly EndDate);
    }
}
