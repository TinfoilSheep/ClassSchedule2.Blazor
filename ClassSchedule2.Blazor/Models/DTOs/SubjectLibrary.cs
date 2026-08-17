namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class SubjectLibrary
    {
        public record SubjectDTO(Guid Id, string Name);

        public record CreateSubjectDTO(string Name);
    }
}
