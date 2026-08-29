namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class LessonNoteLibrary
    {
        public record LessonNoteDTO(Guid Id, Guid LessonId, Guid AuthorId, string Content, DateTime CreatedAt);
        public record CreateLessonNoteDTO(Guid LessonId, Guid AuthorId, string Content);
        public record UpdateLessonNoteDTO(Guid Id, Guid LessonId, Guid AuthorId, string Content);
    }
}
