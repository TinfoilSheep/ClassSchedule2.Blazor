using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class PeriodService : IPeriodService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SubjectService> _logger;

        private const string PeriodBaseUrl = "api/Period/";

        public PeriodService(BrowserAuthService browserAuthService, IConfiguration configuration, ILogger<SubjectService> logger)
        {
            _browserAuthService = browserAuthService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<PeriodDTO?> CreatePeriodAsync(CreatePeriodDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), PeriodBaseUrl + "create").ToString();

            try
            {
                var result = await _browserAuthService.PostAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved oprettelse. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<PeriodDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af periode.");
                return null;
            }
        }

        public async Task<bool> DeletePeriodAsync(Guid periodId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), PeriodBaseUrl + $"delete?id={periodId}").ToString();

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
                _logger.LogError(ex, "Fejl ved sletning af periode {PeriodId}.", periodId);
                return false;
            }
        }

        public async Task<List<PeriodDTO>> GetAllPeriodsAsync()
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), PeriodBaseUrl + "get-all").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente periode. Status: {Status}", result.Status);

                    return [];
                }

                return JsonConvert.DeserializeObject<List<PeriodDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af periode.");
                return [];
            }
        }

        public async Task<PeriodDTO?> GetPeriodByIdAsync(Guid periodId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), PeriodBaseUrl + $"get?id={periodId}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente periode. Status: {Status}", result.Status);

                    return null;
                }

                return JsonConvert.DeserializeObject<PeriodDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af periode {PeriodId}.", periodId);
                return null;
            }
        }

        public async Task<PeriodDTO?> UpdatePeriodAsync(PeriodDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), PeriodBaseUrl + "update").ToString();

            try
            {
                var result = await _browserAuthService.PatchAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke opdatere periode. Status: {Status}", result.Status);

                    return null;
                }

                return JsonConvert.DeserializeObject<PeriodDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af periode.");
                return null;
            }
        }
    }
}
