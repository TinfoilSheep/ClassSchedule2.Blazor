using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class SubjectService : ISubjectService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SubjectService> _logger;

        private const string SubjectBaseUrl = "api/Subject/";

        public SubjectService(BrowserAuthService browserAuthService, IConfiguration configuration, ILogger<SubjectService> logger)
        {
            _browserAuthService = browserAuthService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<SubjectDTO?> CreateSubjectAsync(CreateSubjectDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), SubjectBaseUrl + "create-subject").ToString();

            try
            {
                var result = await _browserAuthService.PostAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved oprettelse. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<SubjectDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af fag.");
                return null;
            }
        }

        public async Task<bool> DeleteSubjectAsync(Guid subjectId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), SubjectBaseUrl + $"delete-subject?id={subjectId}").ToString();

            try
            {
                var result = await _browserAuthService.DeleteAsync(url, subjectId);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved sletning. Status: {Status}", result.Status);
                    return false;
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved sletning af fag {SubjectId}.", subjectId);
                return false;
            }
        }

        public async Task<List<SubjectDTO>> GetAllSubjectsAsync()
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), SubjectBaseUrl + "get-all-subjects").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente fag. Status: {Status}", result.Status);

                    return [];
                }

                return JsonConvert.DeserializeObject<List<SubjectDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af fag.");
                return [];
            }
        }

        public async Task<SubjectDTO?> GetSubjectByIdAsync(Guid subjectId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), SubjectBaseUrl + $"get-subject?id={subjectId}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente fag. Status: {Status}", result.Status);

                    return null;
                }

                return JsonConvert.DeserializeObject<SubjectDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af fag {SubjectId}.", subjectId);
                return null;
            }
        }

        public async Task<SubjectDTO?> UpdateSubjectAsync(SubjectDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), SubjectBaseUrl + "update-subject").ToString();

            try
            {
                var result = await _browserAuthService.PatchAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke opdatere fag. Status: {Status}", result.Status);

                    return null;
                }

                return JsonConvert.DeserializeObject<SubjectDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af fag.");
                return null;
            }
        }
    }
}