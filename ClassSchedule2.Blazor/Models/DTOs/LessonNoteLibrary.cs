namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class LessonNoteLibrary
    {
        public record LessonNoteDTO(Guid Id, Guid LessonId, Guid AuthorId, Guid? EditorId, string Content, DateTime CreatedAt, DateTime? EditedAt);
        public record CreateLessonNoteDTO(Guid LessonId, string Content);
        public record UpdateLessonNoteDTO(Guid Id, string Content);
    }
}
