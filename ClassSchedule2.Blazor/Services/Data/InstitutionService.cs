using ClassSchedule2.Blazor.Interfaces;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.InstitutionLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class InstitutionService : IInstitutionService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<InstitutionService> _logger;
        private readonly string _InstitutionBaseUrl = "api/Institution/";

        public InstitutionService(HttpClient httpClient, IConfiguration configuration, ILogger<InstitutionService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }
        public Task CreateInstitution(CreateInstitutionDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteInstitution(DeleteInstitutionRequestDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetInstitutionListResponseDTO>> GetAllInstitutions()
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";
            var getAllInstitutionsUrl = new Uri(new Uri(apiBase), _InstitutionBaseUrl + "get-all-institution").ToString();

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

        public Task<GetInstitutionListResponseDTO> GetInstitutionById(GetInstitutionByIdRequestDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<GetInstitutionListResponseDTO> UpdateInstitution(UpdateInstitutionRequestDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
