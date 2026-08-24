namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class StudentGroupLibrary
    {
        public record StudentGroupDTO(Guid Id, string Name);
        public record CreateStudentGroupDTO(string Name, List<Guid> StudentIds);
        public record EditStudentGroupDTO(Guid Id, string Name, List<Guid> StudentIds);
    }
}
