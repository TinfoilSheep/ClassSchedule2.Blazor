using static ClassSchedule2.Blazor.Models.DTOs.LessonGeneratorLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ILessonGenerationService
    {
        public Task<int> GenerateForTermAsync(GenerateLessonDTO dto);
        public Task<int> DeleteLessonFromTemplate(DeleteLessonDTO dto);
    }
}
