using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class NonTeachingDayService : INonTeachingDayService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SubjectService> _logger;

        private const string NonTeachingDayBaseUrl = "api/NonTeachingDay/";

        public NonTeachingDayService(BrowserAuthService browserAuthService, IConfiguration configuration, ILogger<SubjectService> logger)
        {
            _browserAuthService = browserAuthService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<NonTeachingDayDTO?> CreateNonTeachingDayAsync(CreateNonTeachingDayDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), NonTeachingDayBaseUrl + "create-nonteachingday").ToString();

            try
            {
                var result = await _browserAuthService.PostAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved oprettelse. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<NonTeachingDayDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af ikke undervisningsdag.");
                return null;
            }
        }

        public async Task<bool> DeleteNonTeachingDayAsync(Guid nonTeachingDayId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), NonTeachingDayBaseUrl + $"delete-nonteachingday?id={nonTeachingDayId}").ToString();

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
                _logger.LogError(ex, "Fejl ved sletning af ikke undervisningsdag {NonTeachingDayId}.", nonTeachingDayId);
                return false;
            }
        }

        public async Task<List<NonTeachingDayDTO>> GetAllNonTeachingDaysAsync()
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), NonTeachingDayBaseUrl + "get-all-nonteachingday").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente ikke undervisningsdage. Status: {Status}", result.Status);

                    return [];
                }

                return JsonConvert.DeserializeObject<List<NonTeachingDayDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af ikke undervisningsdage.");
                return [];
            }
        }

        public async Task<NonTeachingDayDTO?> GetNonTeachingDayByIdAsync(Guid nonTeachingDayId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), NonTeachingDayBaseUrl + $"get-nonteachingday?id={nonTeachingDayId}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente ikke undervisningsdag. Status: {Status}", result.Status);

                    return null;
                }

                return JsonConvert.DeserializeObject<NonTeachingDayDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af ikke undervisningsdag {NonTeachingDayId}.", nonTeachingDayId);
                return null;
            }
        }

        public async Task<NonTeachingDayDTO?> UpdateNonTeachingDayAsync(NonTeachingDayDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), NonTeachingDayBaseUrl + "update-nonteachingday").ToString();

            try
            {
                var result = await _browserAuthService.PatchAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke opdatere ikke undervisningsdag. Status: {Status}", result.Status);

                    return null;
                }

                return JsonConvert.DeserializeObject<NonTeachingDayDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af ikke undervisningsdag.");
                return null;
            }
        }
    }
}
