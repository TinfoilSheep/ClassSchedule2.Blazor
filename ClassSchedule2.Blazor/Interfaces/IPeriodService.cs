using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IPeriodService
    {
        public Task<List<PeriodDTO>> GetAllPeriodsAsync();
        public Task<PeriodDTO?> GetPeriodByIdAsync(Guid PeriodId);
        public Task<PeriodDTO?> CreatePeriodAsync(CreatePeriodDTO dto);
        public Task<PeriodDTO?> UpdatePeriodAsync(PeriodDTO dto);
        public Task<bool> DeletePeriodAsync(Guid PeriodId);
    }
}
