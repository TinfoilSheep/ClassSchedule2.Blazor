namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class LessonGeneratorLibrary
    {
        public record LessonGeneratorDTO(int Created);
        public record DeletedLessonDTO(int Deleted);
        public record GenerateLessonDTO(Guid TermId, List<Guid> LessonTemplateIds);
        public record DeleteLessonDTO(List<Guid> LessonTemplateIds);
    }
}
