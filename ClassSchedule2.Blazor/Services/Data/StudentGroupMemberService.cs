using ClassSchedule2.Blazor.Interfaces;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class StudentGroupMemberService : IStudentGroupMemberService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<InstitutionService> _logger;
        private readonly string _StudentGroupBaseUrl = "api/StudentGroup/";
        private readonly string _ApiBase;

        public StudentGroupMemberService(IConfiguration configuration, ILogger<InstitutionService> logger, BrowserAuthService browserAuthService)
        {
            _configuration = configuration;
            _logger = logger;
            _browserAuthService = browserAuthService;
            _ApiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";
        }
        public async Task<List<MinimalUserInformationDTO>> GetStudentsAsync(Guid studentGroupId)
        {
            string url = new Uri(new Uri(_ApiBase), _StudentGroupBaseUrl + $"{studentGroupId}/get-students").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente alle Elever. Status: {Status}", result.Status);
                    return [];
                }

                return JsonConvert.DeserializeObject<List<MinimalUserInformationDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hetning af alle Elever");
                return [];
            }
        }
    }
}
