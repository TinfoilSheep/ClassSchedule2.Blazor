using ClassSchedule2.Blazor.Models.Enums;

namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class UserLibrary
    {
        public class LoginRequestDTO
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
            public Guid InstitutionId { get; set; }
        }

        public class LoginResponseDTO
        {
            public Guid Id { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public DateOnly DateOfBirth { get; set; }
            public string? Username { get; set; }
            public string? Email { get; set; }
            public DateTime CreatedAt { get; set; }
            public UserRoles Role { get; set; }
            public Guid InstitutionId { get; set; }
        }
    }
}
