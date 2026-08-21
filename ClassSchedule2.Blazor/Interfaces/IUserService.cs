using ClassSchedule2.Blazor.Models.Enums;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IUserService
    {
        public Task<bool> AddUserAsync(CreateUserRequestDTO dto);
        public Task<bool> DeleteUserAsync(Guid userId);
        public Task GetUserInformationAsync(GetUserInformationRequestDTO dto);
        public Task<List<GetAllUsersResponseDTO>> GetAllUsersListAsync(Guid institutionId, UserRoles? role);
    }
}
