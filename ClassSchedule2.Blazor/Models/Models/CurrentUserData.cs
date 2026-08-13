using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;

namespace ClassSchedule2.Blazor.Models.Models
{
    public class CurrentUserData : ICurrentUserData
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Username { get; set; } = "";
        public UserRoles Role { get; set; }

        public string Initials => $"{FirstName.FirstOrDefault()}{LastName.FirstOrDefault()}".ToUpper();
    }
}
