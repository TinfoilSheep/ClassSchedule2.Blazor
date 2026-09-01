using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.AbsenceLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class AbsenceService : IAbsenceService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AbsenceService> _logger;
        private const string AbsenceBaseUrl = "api/Absence/";

        public AbsenceService(BrowserAuthService browserAuthService, IConfiguration configuration, ILogger<AbsenceService> logger)
        {
            _browserAuthService = browserAuthService;
            _configuration = configuration;
            _logger = logger;
        }

        
        public async Task<List<AbsenceDTO>> GetAllAbsencesFromLesson(Guid lessonId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), AbsenceBaseUrl + $"get-all?lessonId={lessonId}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente fravær. Status: {Status}", result.Status);

                    return [];
                }

                return JsonConvert.DeserializeObject<List<AbsenceDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af fravær.");
                return [];
            }
        }

        public async Task<bool> RegisterAbsence(Guid lessonId, List<SetAbsenceDTO> dtos)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), AbsenceBaseUrl + $"set-absence?lessonId={lessonId}").ToString();

            try
            {
                var result = await _browserAuthService.PutAsync(url, dtos);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke registrere fravær. Status: {Status}", result.Status);

                    return result.Success;
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved registrering af fravær.");
                return false;
            }
        }
    }
}
