using ClassSchedule2.Blazor.Models.Enums;

namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class UserLibrary
    {
        public class CreateUserRequestDTO
        {
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public DateOnly? DateOfBirth { get; set; }
            public string? Username { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
            public UserRoles Role { get; set; }
        }

        public class DeleteUserRequestDTO
        {
            public Guid UserId { get; set; }
        }

        public class GetUserInformationRequestDTO
        {
            public Guid RequestedUserId { get; set; }
        }

        public class GetUserInformationResponseDTO
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
            public string? InstitutionName { get; set; }
        }

        public class GetAllUsersResponseDTO
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public DateOnly DateOfBirth { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public UserRoles Role { get; set; }
            public Guid InstitutionId { get; set; }
            public string InstitutionName { get; set; } = string.Empty;
        }
    }
}
