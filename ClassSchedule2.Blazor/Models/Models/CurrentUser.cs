using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ClassSchedule2.Blazor.Models.Models
{
    public class CurrentUser : ICurrentUser
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public CurrentUser(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<CurrentUserData?> GetAsync()
        {
            var authState =
                await _authenticationStateProvider.GetAuthenticationStateAsync();

            var user = authState.User;

            if (user.Identity?.IsAuthenticated != true)
                return null;

            return new CurrentUserData
            {
                UserId = Guid.Parse(
                    user.FindFirstValue(ClaimTypes.NameIdentifier)!),

                FirstName = user.FindFirstValue(ClaimTypes.GivenName) ?? "",

                LastName = user.FindFirstValue(ClaimTypes.Surname) ?? "",

                Username = user.FindFirstValue(ClaimTypes.Name) ?? "",

                Role = Enum.Parse<UserRoles>(
                    user.FindFirstValue(ClaimTypes.Role)!),

                InstitutionId = Guid.Parse(
                    user.FindFirstValue("InstitutionId")!)
            };
        }
    }
}
