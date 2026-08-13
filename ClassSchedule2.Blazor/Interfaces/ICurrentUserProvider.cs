using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Models;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ICurrentUserProvider
    {
        public Task<CurrentUserData?> GetAsync();
    }
}
