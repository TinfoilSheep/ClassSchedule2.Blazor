using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.StudentGroupLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class StudentGroupService : IStudentGroupService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<InstitutionService> _logger;
        private readonly string _StudentGroupBaseUrl = "api/StudentGroup/";
        private readonly string _ApiBase;

        public StudentGroupService(IConfiguration configuration, ILogger<InstitutionService> logger, BrowserAuthService browserAuthService)
        {
            _configuration = configuration;
            _logger = logger;
            _browserAuthService = browserAuthService;
            _ApiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";
        }

        public async Task<bool> Create(CreateStudentGroupDTO dto)
        {
            string? url = new Uri(new Uri(_ApiBase), _StudentGroupBaseUrl + "create").ToString();

            try
            {
                var result = await _browserAuthService.PostAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved oprettelse. Status: {Status}", result.Status);
                    return result.Success;
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af Klassen.");
                return false;
            }
        }

        public async Task<bool> Update(EditStudentGroupDTO dto)
        {
            string? url = new Uri(new Uri(_ApiBase), _StudentGroupBaseUrl + "update").ToString();

            try
            {
                var result = await _browserAuthService.PatchAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved oprettelse. Status: {Status}", result.Status);
                    return result.Success;
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af Klassen {studentGroupId}.", dto.Id);
                return false;
            }
        }

        public async Task<bool> Delete(Guid studentGroupId)
        {
            string url = new Uri(new Uri(_ApiBase), _StudentGroupBaseUrl + $"delete?id={studentGroupId}").ToString();

            try
            {
                var result = await _browserAuthService.DeleteAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved sletning. Status: {Status}", result.Status);
                    return result.Success;
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved sletning af Klassen {studentGroupId}.", studentGroupId);
                return false;
            }
        }

        public async Task<StudentGroupDTO?> Get(Guid studentGroupId)
        {
            string url = new Uri(new Uri(_ApiBase), _StudentGroupBaseUrl + $"get?id={studentGroupId}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente Klassen. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<StudentGroupDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hetning af Klassen {studentGroupId}.", studentGroupId);
                return null;
            }
        }

        public async Task<List<StudentGroupDTO>> GetAll()
        {
            string url = new Uri(new Uri(_ApiBase), _StudentGroupBaseUrl + $"get-all").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente alle Klasser. Status: {Status}", result.Status);
                    return [];
                }

                return JsonConvert.DeserializeObject<List<StudentGroupDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hetning af alle Klasser");
                return [];
            }
        }

    }
}
