using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ClassSchedule2.Blazor.Providers
{
    public class CurrentUserProvider : ICurrentUserProvider
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CurrentUserProvider(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<CurrentUserData?> GetAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

            var user = authState.User;

            if (user.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            return new CurrentUserData
            {
                UserId = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!),

                FirstName = user.FindFirstValue(ClaimTypes.GivenName) ?? "",

                LastName = user.FindFirstValue(ClaimTypes.Surname) ?? "",

                Username = user.FindFirstValue(ClaimTypes.Name) ?? "",

                Role = Enum.Parse<UserRoles>(user.FindFirstValue(ClaimTypes.Role)!),

                InstitutionId = Guid.Parse(user.FindFirstValue("InstitutionId")!)
            };
        }
    }
}

