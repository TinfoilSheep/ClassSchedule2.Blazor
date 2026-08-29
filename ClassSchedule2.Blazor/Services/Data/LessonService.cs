using ClassSchedule2.Blazor.Interfaces;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class LessonService : ILessonService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LessonTemplateService> _logger;

        private const string LessonBaseUrl = "api/Lesson/";

        public LessonService(BrowserAuthService browserAuthService, IConfiguration configuration, ILogger<LessonTemplateService> logger)
        {
            _browserAuthService = browserAuthService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<List<MinimalUserInformationDTO>> GetAllStudents(Guid lessonId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), LessonBaseUrl + $"get-students?id={lessonId}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente periode. Status: {Status}", result.Status);

                    return [];
                }

                return JsonConvert.DeserializeObject<List<MinimalUserInformationDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af periode.");
                return [];
            }
        }
    }
}
