using ClassSchedule2.Blazor.Models.Enums;
using static ClassSchedule2.Blazor.Models.DTOs.ScheduleLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IScheduleService
    {
        public Task<List<ScheduleLessonDTO>> GetScheduleAsync(GetScheduleLessonDTO dto);
    }
}
