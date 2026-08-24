using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using Newtonsoft.Json;
using System.Runtime.Intrinsics.Arm;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class HoldService : IHoldService
    {
        private readonly BrowserAuthService _browserAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<InstitutionService> _logger;
        private readonly string _HoldBaseUrl = "api/Hold/";
        private readonly string _ApiBase;

        public HoldService(IConfiguration configuration, ILogger<InstitutionService> logger, BrowserAuthService browserAuthService)
        {
            _configuration = configuration;
            _logger = logger;
            _browserAuthService = browserAuthService;
            _ApiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";
        }

        public async Task<bool> Create(CreateHoldDTO dto)
        {
            string url = new Uri(new Uri(_ApiBase), _HoldBaseUrl + "Create").ToString();

            try
            {
                var result = await _browserAuthService.PostAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved oprettelse. Status: {Status} fejlbesked: {ErrorMessage}", result.Status, result.ResponseText);
                    return result.Success;
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved oprettelse af Hold.");
                return false;
            }
        }

        public async Task<bool> Delete(Guid holdId)
        {
            string url = new Uri(new Uri(_ApiBase), _HoldBaseUrl + $"Delete?id={holdId}").ToString();

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
                _logger.LogError(ex, "Fejl ved sletning af Hold {holdId}.", holdId);
                return false;
            }
        }

        public async Task<HoldDTO?> Get(Guid holdId)
        {
            string url = new Uri(new Uri(_ApiBase), _HoldBaseUrl + $"Get?id={holdId}").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente Hold. Status: {Status}", result.Status);
                    return null;
                }

                return JsonConvert.DeserializeObject<HoldDTO>(result.ResponseText ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hetning af Hold {holdId}.", holdId);
                return null;
            }
        }

        public async Task<List<HoldDTO>> GetAll()
        {
            string url = new Uri(new Uri(_ApiBase), _HoldBaseUrl + $"Get-All").ToString();

            try
            {
                var result = await _browserAuthService.GetAsync(url);

                if (!result.Success)
                {
                    _logger.LogWarning("Kunne ikke hente alle Hold's. Status: {Status}", result.Status);
                    return [];
                }

                return JsonConvert.DeserializeObject<List<HoldDTO>>(result.ResponseText ?? string.Empty) ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved hetning af alle Hold's");
                return [];
            }
        }

        public async Task<bool> Update(HoldDTO dto)
        {
            string url = new Uri(new Uri(_ApiBase), _HoldBaseUrl + "Update").ToString();

            try
            {
                var result = await _browserAuthService.PatchAsync(url, dto);

                if (!result.Success)
                {
                    _logger.LogWarning("Fejl ved opdatering. Status: {Status}", result.Status);
                    return result.Success;
                }

                return result.Success;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fejl ved opdatering af Hold.");
                return false;
            }
        }
    }
}
