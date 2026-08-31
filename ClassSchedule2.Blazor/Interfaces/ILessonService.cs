using static ClassSchedule2.Blazor.Models.DTOs.LessonLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ILessonService
    {
        public Task<List<MinimalUserInformationDTO>> GetAllStudents(Guid lessonId);

        public Task<LessonDTO> GetLesson(Guid lessonId);
    }
}
