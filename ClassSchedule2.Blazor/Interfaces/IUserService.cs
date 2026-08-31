using ClassSchedule2.Blazor.Models.Enums;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CreateUserAsync(CreateUserRequestDTO dto);
        public Task<bool> DeleteUserAsync(Guid userId);
        public Task<GetUserInformationResponseDTO?> GetUserInformationAsync(Guid id);
        public Task<GetUserInformationResponseDTO?> GetCurrentUserInformationAsync();
        public Task<List<GetUserInformationResponseDTO>> GetAllUsersListAsync(UserRoles? role = null);
        public Task<GetUserInformationResponseDTO?> UpdateUserAsync(UpdateUserInformationDTO dto);
        public Task<bool> ChangeUserCredentialsAsync(ChangeUserCredentialsDTO dto);
        public Task<bool> CheckUsernameIsAvailableAsync(string userName);
    }
}
