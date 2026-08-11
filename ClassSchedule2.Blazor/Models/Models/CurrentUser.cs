using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using System.Security.Claims;

namespace ClassSchedule2.Blazor.Models.Models
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId =>
            Guid.Parse(
                _httpContextAccessor.HttpContext!
                    .User
                    .FindFirstValue(ClaimTypes.NameIdentifier)!
            );

        public Guid InstitutionId =>
            Guid.Parse(
                _httpContextAccessor.HttpContext!
                    .User
                    .FindFirstValue("InstitutionId")!
            );

        public UserRoles Role =>
            Enum.Parse<UserRoles>(
                _httpContextAccessor.HttpContext!
                    .User
                    .FindFirstValue(ClaimTypes.Role)!
            );
    }
}
