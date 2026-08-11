using ClassSchedule2.Blazor.Models.Enums;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ICurrentUser
    {
        Guid UserId { get; }
        Guid InstitutionId { get; }
        UserRoles Role { get; }
    }
}
