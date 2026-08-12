using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Providers
{
    public class SchoolAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        private readonly BrowserAuthService _browserAuthService;

        private static readonly ClaimsPrincipal Anonymous =new(new ClaimsIdentity());

        private ClaimsPrincipal _currentUser = Anonymous;

        public SchoolAuthenticationStateProvider(
            IJSRuntime js,
            BrowserAuthService browserAuthService)
        {
            _js = js;
            _browserAuthService = browserAuthService;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // hent logged-in bruger-id fra LocalStorage
            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        public async Task InitializeAsync()
        {
            var userIdString = await _js.InvokeAsync<string?>("localStorage.getItem", "SchoolUserId");

            if (!Guid.TryParse(userIdString, out var userId))
            {
                return;
            }

            LoginResponseDTO? user = await _browserAuthService.GetUserAsync(userId);

            if (user is null)
            {
                return;
            }

            SetUser(user);
        }

        public void SetUser(LoginResponseDTO user)
        {
            _currentUser = CreatePrincipal(user);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        public void Logout()
        {
            _currentUser = Anonymous;

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        }

        private static ClaimsPrincipal CreatePrincipal(LoginResponseDTO user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),

                new(ClaimTypes.Name, user.Username ?? string.Empty),

                new(ClaimTypes.Role, user.Role.ToString()),

                new("InstitutionId", user.InstitutionId.ToString())
            };

            var identity = new ClaimsIdentity(claims, authenticationType: "SchoolSession");

            return new ClaimsPrincipal(identity);
        }
    }
}
