using static ClassSchedule2.Blazor.Models.DTOs.LessonNoteLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ILessonNoteService
    {
        public Task<bool> AddNote(CreateLessonNoteDTO dto);
        public Task<bool> UpdateNote(UpdateLessonNoteDTO dto);
        public Task<bool> DeleteNote(Guid noteId);
    }
}
