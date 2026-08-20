namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class HoldLibrary
    {
        public record HoldDTO(Guid Id, string Name, Guid TermId, Guid SubjectId);
        public record CreateHoldDTO(string Name, Guid TermId, Guid SubjectId);
    }
}
