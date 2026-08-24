using ClassSchedule2.Blazor.Interfaces;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class TermService : ITermService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SubjectService> _logger;

        private const string TermBaseUrl = "api/Term/";

        public TermService(BrowserAuthService browserAuthService, IConfiguration configuration, ILogger<SubjectService> logger)
        {
            _browserAuthService = browserAuthService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<TermDTO?> CreateTermAsync(CreateTermDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), TermBaseUrl + "create").ToString();

            try
            {
                var result = await _browserAuthService.PostAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved oprettelse. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<TermDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af term.");
                return null;
            }
        }

        public async Task<bool> DeleteTermAsync(Guid termId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), TermBaseUrl + $"delete?id={termId}").ToString();

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
                _logger.LogError(ex, "Fejl ved sletning af term {TermId}.", termId);
                return false;
            }
        }

        public async Task<List<TermDTO>> GetAllTermsAsync()
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), TermBaseUrl + "get-all").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente termer. Status: {Status}", result.Status);

                    return [];
                }

                return JsonConvert.DeserializeObject<List<TermDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af termer.");
                return [];
            }
        }

        public async Task<TermDTO?> GetTermByIdAsync(Guid termId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), TermBaseUrl + $"get?id={termId}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente term. Status: {Status}", result.Status);

                    return null;
                }

                return JsonConvert.DeserializeObject<TermDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af term {TermId}.", termId);
                return null;
            }
        }

        public async Task<TermDTO?> UpdateTermAsync(TermDTO dto)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), TermBaseUrl + "update").ToString();

            try
            {
                var result = await _browserAuthService.PatchAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke opdatere term. Status: {Status}", result.Status);

                    return null;
                }

                return JsonConvert.DeserializeObject<TermDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af term.");
                return null;
            }
        }
    }
}
