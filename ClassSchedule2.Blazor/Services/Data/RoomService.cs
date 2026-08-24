using ClassSchedule2.Blazor.Interfaces;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.RoomLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class RoomService : IRoomService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SubjectService> _logger;

        private const string RoomBaseUrl = "api/Room/";

        public RoomService(BrowserAuthService browserAuthService, IConfiguration configuration, ILogger<SubjectService> logger)
        {
            _browserAuthService = browserAuthService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<RoomDTO?> CreateRoomAsync(CreateRoomDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), RoomBaseUrl + "create").ToString();

            try
            {
                var result = await _browserAuthService.PostAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved oprettelse. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<RoomDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af lokale.");
                return null;
            }
        }

        public async Task<bool> DeleteRoomAsync(Guid roomId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), RoomBaseUrl + $"delete?id={roomId}").ToString();

            try
            {
                var result = await _browserAuthService.DeleteAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved sletning. Status: {Status}", result.Status);
                    return false;
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved sletning af lokale {RoomId}.", roomId);
                return false;
            }
        }

        public async Task<List<RoomDTO>> GetAllRoomsAsync()
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), RoomBaseUrl + "get-all").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente lokale. Status: {Status}", result.Status);

                    return [];
                }

                return JsonConvert.DeserializeObject<List<RoomDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af lokale.");
                return [];
            }
        }

        public async Task<RoomDTO?> GetRoomByIdAsync(Guid roomId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), RoomBaseUrl + $"get?id={roomId}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente lokale. Status: {Status}", result.Status);

                    return null;
                }

                return JsonConvert.DeserializeObject<RoomDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af lokale {RoomId}.", roomId);
                return null;
            }
        }

        public async Task<RoomDTO?> UpdateRoomAsync(RoomDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), RoomBaseUrl + "update").ToString();

            try
            {
                var result = await _browserAuthService.PatchAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke opdatere lokale. Status: {Status}", result.Status);

                    return null;
                }

                return JsonConvert.DeserializeObject<RoomDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af lokale.");
                return null;
            }
        }
    }
}
