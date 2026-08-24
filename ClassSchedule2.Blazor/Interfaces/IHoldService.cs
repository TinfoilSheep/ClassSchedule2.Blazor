using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IHoldService
    {
        public Task<List<HoldDTO>> GetAll();
        public Task<HoldDTO?> Get(Guid HoldId);
        public Task<bool> Create(CreateHoldDTO dto);
        public Task<bool> Update(HoldDTO dto);
        public Task<bool> Delete(Guid HoldId);
    }
}
