using ClassSchedule2.Blazor.Models.Enums;

namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class AbsenceLibrary
    {
        public record AbsenceDTO(Guid Id, Guid LessonId, Guid StudentIds, AbsenceStatus Status, Guid RegisteredById);
        public record SetAbsenceDTO(Guid StudentId, AbsenceStatus Status);
    }
}
