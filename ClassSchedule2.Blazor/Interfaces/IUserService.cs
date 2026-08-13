namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IUserService
    {
        public Task AddUserAsync();
        public Task DeleteUserAsync();
        public Task GetUserInformationAsync();
        public Task GetAllUsersListAsync();
    }
}
