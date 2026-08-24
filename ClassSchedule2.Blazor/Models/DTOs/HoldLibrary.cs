namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class HoldLibrary
    {
        public record HoldDTO(Guid Id, string Name, Guid SubjectId, Guid TermId, string SubjectName, string TermName, List<Guid> Teachers, List<Guid> Students);
        public record CreateHoldDTO(string Name, Guid TermId, Guid SubjectId, List<Guid> Teachers, List<Guid> Students);
    }
}
