using static ClassSchedule2.Blazor.Models.DTOs.AbsenceLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IAbsenceService
    {
        public Task<List<AbsenceDTO>> GetAllAbsencesFromLesson(Guid lessonId);
        public Task<bool> RegisterAbsence(Guid lessonId, List<SetAbsenceDTO> dtos);
    }
}
