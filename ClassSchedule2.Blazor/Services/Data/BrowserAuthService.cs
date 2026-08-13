using ClassSchedule2.Blazor.Models.Models;
using Microsoft.JSInterop;
using System.Text.Json;
using static ClassSchedule2.Blazor.Models.DTOs.AuthLibrary;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class BrowserAuthService
    {
        private readonly IJSRuntime _js;
        private readonly IConfiguration _configuration;
        private readonly string _userUrl = "api/User/";

        public BrowserAuthService(IJSRuntime js, IConfiguration configuration)
        {
            _js = js;
            _configuration = configuration;
        }

        public async Task<BrowserLoginResult> LoginAsync(LoginRequestDTO login)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var loginUrl = new Uri(new Uri(apiBase), _userUrl + "Login").ToString();

            var result = await _js.InvokeAsync<JsonElement>("authLogin", loginUrl, login);

            var success = result.GetProperty("ok").GetBoolean();

            var status = result.GetProperty("status").GetInt32();

            var responseText = result.GetProperty("text").GetString();

            LoginResponseDTO? user = null;

            if (success && !string.IsNullOrWhiteSpace(responseText))
            {
                user = JsonSerializer.Deserialize<LoginResponseDTO>(responseText, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }

            if (user != null)
            {
                await _js.InvokeVoidAsync("localStorage.setItem", "SchoolUserId", user.Id.ToString());
            }

            return new BrowserLoginResult
            {
                Success = success,
                Status = status,
                ResponseText = responseText,
                User = user
            };
        }

        public async Task<LoginResponseDTO?> GetUserAsync(Guid userId)
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var userUrl = new Uri(new Uri(apiBase), _userUrl + $"Get-User-Information?id={userId}").ToString();

            var result = await _js.InvokeAsync<JsonElement>("authGet", userUrl);

            var success = result.GetProperty("ok").GetBoolean();

            if (!success)
            {
                return null;
            }

            var responseText = result.GetProperty("text").GetString();

            if (string.IsNullOrWhiteSpace(responseText))
            {
                return null;
            }

            return JsonSerializer.Deserialize<LoginResponseDTO>(responseText, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }

        public async Task<bool> LogoutAsync()
        {
            var apiBase = _configuration["ApiBaseUrl"] ?? "https://localhost:7053/";

            var logoutUrl = new Uri(new Uri(apiBase), _userUrl + "Logout").ToString();

            var result = await _js.InvokeAsync<JsonElement>("authLogout", logoutUrl);

            return result.GetProperty("ok").GetBoolean();
        }
    }
}
