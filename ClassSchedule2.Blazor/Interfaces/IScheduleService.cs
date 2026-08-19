using ClassSchedule2.Blazor.Models.Models;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IScheduleService
    {
        Task<List<ScheduleLesson>> GetScheduleAsync();
    }
}
