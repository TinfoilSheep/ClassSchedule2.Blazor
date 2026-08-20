using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IHoldService
    {
        public Task<List<HoldDTO>> GetAll();
        public Task<HoldDTO?> Get(Guid PeriodId);
        public Task<HoldDTO?> Create(CreateHoldDTO dto);
        public Task<HoldDTO?> Update(HoldDTO dto);
        public Task<bool> Delete(Guid PeriodId);
    }
}
