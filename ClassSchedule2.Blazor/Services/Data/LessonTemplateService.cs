using ClassSchedule2.Blazor.Interfaces;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.LessonTemplateLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class LessonTemplateService : ILessonTemplateService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LessonTemplateService> _logger;

        private const string LessonTemplateBaseUrl = "api/LessonTemplate/";

        public LessonTemplateService(BrowserAuthService browserAuthService, IConfiguration configuration, ILogger<LessonTemplateService> logger)
        {
            _browserAuthService = browserAuthService;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<LessonTemplateDTO?> CreateAsync(CreateLessonTemplateDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), LessonTemplateBaseUrl + "create").ToString();

            try
            {
                var result = await _browserAuthService.PostAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved oprettelse. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<LessonTemplateDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af lektionsplan.");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), LessonTemplateBaseUrl + $"delete?id={id}").ToString();

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
                _logger.LogError(ex, "Fejl ved sletning af lektionsplan {LessonTemplateId}.", id);
                return false;
            }
        }

        public async Task<List<LessonTemplateDTO>?> GetAllAsync()
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), LessonTemplateBaseUrl + "get-all").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved hentning af alle lektionsplaner. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<List<LessonTemplateDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af alle lektionsplaner.");
                return null;
            }
        }

        public async Task<LessonTemplateDTO?> GetByIdAsync(Guid id)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), LessonTemplateBaseUrl + $"get-by-id?id={id}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved hentning af lektionsplan. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<LessonTemplateDTO>(result.ResponseText ?? string.Empty) ?? null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af lektionsplan {LessonTemplateId}.", id);
                return null;
            }
        }

        public async Task<LessonTemplateDTO?> UpdateAsync(UpdateLessonTemplateDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), LessonTemplateBaseUrl + "update").ToString();

            try
            {
                var result = await _browserAuthService.PatchAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved opdatering. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<LessonTemplateDTO>(result.ResponseText ?? string.Empty) ?? null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af lektionsplan.");
                return null;
            }
        }
    }
}
