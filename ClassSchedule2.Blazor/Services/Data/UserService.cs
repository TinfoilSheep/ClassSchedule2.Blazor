using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;
        private readonly BrowserAuthService _browserAuthService;
        private readonly string _userBaseUrl = "api/User/";

        public UserService(HttpClient httpClient, IConfiguration configuration, ILogger<UserService> logger, BrowserAuthService browserAuthService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _browserAuthService = browserAuthService;
        }

        public async Task<bool> AddUserAsync(UserLibrary.CreateUserRequestDTO dto)
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

        public Task DeleteUserAsync(UserLibrary.DeleteUserRequestDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task GetAllUsersListAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetUserInformationAsync(UserLibrary.GetUserInformationRequestDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
