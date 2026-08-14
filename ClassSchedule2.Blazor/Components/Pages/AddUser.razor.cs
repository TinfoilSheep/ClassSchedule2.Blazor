using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components;

namespace ClassSchedule2.Blazor.Components.Pages
{
    public partial class AddUser
    {
        [Inject] private IUserService UserService { get; set; } = default!;

        private async Task TestAddUser()
        {
            var user = new UserLibrary.CreateUserRequestDTO
            {
                FirstName = "Peter",
                LastName = "Pan",
                DateOfBirth = new DateOnly(1990, 1, 1),
                Username = "peterpan",
                Email = "peterpan@test.dk",
                Password = "Passw0rd",
                Role = UserRoles.Student
            };

            var success = await UserService.AddUserAsync(user);

            Console.WriteLine($"Create user: {success}");
        }
    }
}
