using static ClassSchedule2.Blazor.Models.DTOs.LessonNoteLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class LessonLibrary
    {
        public record LessonDTO(
            Guid Id,
            DateOnly Date,
            TimeOnly StartTime,
            TimeOnly EndTime,
            string SubjectName,
            string HoldName,
            string? RoomName,
            string Status,
            LessonNoteDTO? Note,
            List<MinimalUserInformationDTO> AbsentStudents,
            List<MinimalUserInformationDTO> Teachers);

        public record GetLessonDTO(Guid TargetId, DateOnly From, DateOnly To);
    }
}
