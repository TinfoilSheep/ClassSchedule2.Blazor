using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IStudentGroupMemberService
    {
        public Task<List<MinimalUserInformationDTO>> GetStudentsAsync(Guid studentGroupId);
    }
}
