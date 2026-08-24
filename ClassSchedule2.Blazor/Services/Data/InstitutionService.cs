using ClassSchedule2.Blazor.Interfaces;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.InstitutionLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class InstitutionService : IInstitutionService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<InstitutionService> _logger;
        private readonly string _InstitutionBaseUrl = "api/Institution/";

        public InstitutionService(HttpClient httpClient, IConfiguration configuration, ILogger<InstitutionService> logger, BrowserAuthService browserAuthService)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
            _browserAuthService = browserAuthService;
        }

        public async Task<List<GetInstitutionListResponseDTO>> GetAllInstitutions()
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";
            var getAllInstitutionsUrl = new Uri(new Uri(apiBase), _InstitutionBaseUrl + "get-all").ToString();

            try
            {
                var jsonResponse = await _httpClient.GetStringAsync(getAllInstitutionsUrl);

                var result = JsonConvert.DeserializeObject<List<GetInstitutionListResponseDTO>>(jsonResponse);

                return result ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af alle institutioner.");
                return [];
            }
        }

        public async Task<GetInstitutionListResponseDTO?> GetInstitutionById(Guid id)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var url = new Uri(new Uri(apiBase), _InstitutionBaseUrl + $"get?id={id}"
            ).ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Hentning af institution fejlede. StatusCode: {StatusCode}, InstitutionId: {InstitutionId}", result.Status, id);

                    return null;
                }

                if (string.IsNullOrWhiteSpace(result.ResponseText))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<GetInstitutionListResponseDTO>(result.ResponseText);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hentning af institution {InstitutionId}", id);

                return null;
            }
        }
    }
}
