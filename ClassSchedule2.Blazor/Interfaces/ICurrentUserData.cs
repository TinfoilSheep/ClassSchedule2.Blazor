using ClassSchedule2.Blazor.Models.Enums;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ICurrentUserData
    {
        public Guid UserId { get; set; }
        public Guid InstitutionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public UserRoles Role { get; set; }

    }
}
