using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface INonTeachingDayService
    {
        public Task<List<NonTeachingDayDTO>> GetAllNonTeachingDaysAsync();
        public Task<NonTeachingDayDTO?> GetNonTeachingDayByIdAsync(Guid nonTeachingDayId);
        public Task<bool> CreateNonTeachingDayAsync(CreateNonTeachingDayDTO dto);
        public Task<bool> UpdateNonTeachingDayAsync(NonTeachingDayDTO dto);
        public Task<bool> DeleteNonTeachingDayAsync(Guid nonTeachingDayId);
    }
}
