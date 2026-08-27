using ClassSchedule2.Blazor.Interfaces;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.ScheduleLessonLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class ScheduleLessonService : IScheduleService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ScheduleLessonService> _logger;
        private readonly BrowserAuthService _browserAuthService;
        private readonly string _userBaseUrl = "api/Schedule/";

        public ScheduleLessonService(IConfiguration configuration, ILogger<ScheduleLessonService> logger, BrowserAuthService browserAuthService)
        {
            _configuration = configuration;
            _logger = logger;
            _browserAuthService = browserAuthService;
        }

        public async Task<List<ScheduleLessonDTO>> GetScheduleAsync(GetScheduleLessonDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            string url = new Uri(new Uri(apiBase), _userBaseUrl + "get").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Hentning af skema fejlede. StatusCode: {StatusCode}", result.Status);

                    return [];
                }

                return JsonConvert.DeserializeObject<List<ScheduleLessonDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af skema.");

                return [];
            }
        }
    }
}
