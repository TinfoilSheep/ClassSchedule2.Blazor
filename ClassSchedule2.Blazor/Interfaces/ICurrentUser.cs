using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Models;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ICurrentUser
    {
        public Task<CurrentUserData?> GetAsync();
    }
}
