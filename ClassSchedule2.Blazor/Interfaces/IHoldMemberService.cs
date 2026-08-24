using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IHoldMemberService
    {
        public Task<List<MinimalUserInformationDTO>> GetStudentsAsync(Guid holdId);
        public Task<List<MinimalUserInformationDTO>> GetTeachersAsync(Guid holdId);
    }
}
