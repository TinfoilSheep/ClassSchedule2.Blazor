using ClassSchedule2.Blazor.Interfaces;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.InstitutionLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class InstitutionService : IInstitutionService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _InstitutionBaseUrl = "api/Institution/";

        public InstitutionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
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
                Console.WriteLine($"Fejl ved hentning af institutioner: {ex.Message}");
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
