using ClassSchedule2.Blazor.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class AuthLibrary
    {
        public class LoginRequestDTO
        {
            [Required(ErrorMessage = "Indtast brugernavn")]
            public string? Username { get; set; }
            [Required(ErrorMessage = "Indtast adgangskode")]
            [MinLength(8, ErrorMessage = "Adgangskoden skal være på mindst 8 tegn")]
            public string? Password { get; set; }
            [Required(ErrorMessage = "Vælg Institution")]
            public Guid? InstitutionId { get; set; }
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

        public class BrowserApiResult
        {
            public bool Success { get; set; }
            public int Status { get; set; }
            public string? ResponseText { get; set; }
        }

        public class BrowserLoginResult
        {
            public bool Success { get; set; }
            public int Status { get; set; }
            public string? ResponseText { get; set; }
            public LoginResponseDTO? User { get; set; }
        }
    }
}
