using ClassSchedule2.Blazor.Interfaces;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.LessonGeneratorLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.LessonTemplateLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class LessonGenerationService : ILessonGenerationService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LessonGenerationService> _logger;
        private readonly string _ApiBase;

        public LessonGenerationService(IConfiguration configuration, ILogger<LessonGenerationService> logger, BrowserAuthService browserAuthService)
        {
            _configuration = configuration;
            _logger = logger;
            _browserAuthService = browserAuthService;
            _ApiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";
        }

        public async Task<int> GenerateForTermAsync(Guid termId)
        {
            string url = new Uri(new Uri(_ApiBase), $"api/terms/{termId}/lessons/generate-lessons").ToString();

            try
            {
                var result = await _browserAuthService.PostNoBodyAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved oprettelse. Status: {Status} fejlbesked: {ErrorMessage}", result.Status, result.ResponseText);
                    return -1;
                }

                var response = JsonConvert.DeserializeObject<LessonGeneratorDTO>(result.ResponseText ?? string.Empty);

                return response?.Created ?? -1;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af Hold.");
                return -1;
            }
        }
    }
}
