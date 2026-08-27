using static ClassSchedule2.Blazor.Models.DTOs.LessonTemplateLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ILessonTemplateService
    {
        public Task<LessonTemplateDTO?> CreateAsync(CreateLessonTemplateDTO dto);
        public Task<List<LessonTemplateDTO>?> GetAllAsync();
        public Task<LessonTemplateDTO?> GetByIdAsync(Guid id);
        public Task<LessonTemplateDTO?> UpdateAsync(UpdateLessonTemplateDTO dto);
        public Task<bool> DeleteAsync(Guid id);

    }
}
