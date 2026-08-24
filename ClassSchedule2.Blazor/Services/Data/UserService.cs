using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using ClassSchedule2.Blazor.Models.Enums;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly BrowserAuthService _browserAuthService;
        private readonly string _userBaseUrl = "api/User/";

        public UserService(IConfiguration configuration, ILogger<UserService> logger, BrowserAuthService browserAuthService)
        {
            _configuration = configuration;
            _logger = logger;
            _browserAuthService = browserAuthService;
        }

        public async Task<bool> AddUserAsync(CreateUserRequestDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";
            var addUserUrl = new Uri(new Uri(apiBase), _userBaseUrl + "Add").ToString();

            try
            {
                var result = await _browserAuthService.PostAsync(addUserUrl, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Oprettelse af bruger fejlede. StatusCode: {StatusCode}, Username: {Username}", result.Status, dto.Username);

                    return false;
                }

                _logger.LogInformation("Bruger oprettet. Username: {Username}, Role: {Role}", dto.Username, dto.Role);

                return result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse aaf bruger. Username: {Username}, Role: {Role}", dto.Username, dto.Role);

                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), _userBaseUrl + "Delete").ToString();

            try
            {
                var result = await _browserAuthService.DeleteAsync(url, userId);

                if (!result.Success)
                {
                    _logger.LogWarning("Sletning af bruger fejlede. StatusCode: {StatusCode}, UserId: {UserId}", result.Status, userId);

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved sletning af bruger {UserId}", userId);

                return false;
            }
        }

        public async Task<List<GetUserInformationResponseDTO>> GetAllUsersListAsync(UserRoles? role = null)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            string url = new Uri(new Uri(apiBase), $"api/User/Get-All-Users?role={role}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Hentning af brugere fejlede. StatusCode: {StatusCode}", result.Status);

                    return [];
                }

                return JsonConvert.DeserializeObject<List<GetUserInformationResponseDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af brugere.");

                return [];
            }
        }

        public Task GetUserInformationAsync(GetUserInformationRequestDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
