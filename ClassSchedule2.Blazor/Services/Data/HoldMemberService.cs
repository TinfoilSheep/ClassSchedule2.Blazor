using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using Newtonsoft.Json;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class HoldMemberService : IHoldMemberService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<HoldMemberService> _logger;
        private readonly string _HoldBaseUrl = "api/Hold/";
        private readonly string _ApiBase;

        public HoldMemberService(IConfiguration configuration, ILogger<HoldMemberService> logger, BrowserAuthService browserAuthService)
        {
            _configuration = configuration;
            _logger = logger;
            _browserAuthService = browserAuthService;
            _ApiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";
        }
        public async Task<List<MinimalUserInformationDTO>> GetStudentsAsync(Guid holdId)
        {
            string url = new Uri(new Uri(_ApiBase), _HoldBaseUrl + $"{holdId}/students").ToString();

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

        public async Task<List<MinimalUserInformationDTO>> GetTeachersAsync(Guid holdId)
        {
            string url = new Uri(new Uri(_ApiBase), _HoldBaseUrl + $"{holdId}/teachers").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente alle Lærere. Status: {Status}", result.Status);
                    return [];
                }

                return JsonConvert.DeserializeObject<List<MinimalUserInformationDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hetning af alle Lærere");
                return [];
            }
        }
    }
}
