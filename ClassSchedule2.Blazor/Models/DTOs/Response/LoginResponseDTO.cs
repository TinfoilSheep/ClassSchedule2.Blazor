using ClassSchedule2.Blazor.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.DTOs.Response
{
    public class LoginResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string Username { get; set; } = string.Empty;
        [EmailAddress] public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public UserRoles Role { get; set; }
        public Guid InstitutionId { get; set; }
    }
}
