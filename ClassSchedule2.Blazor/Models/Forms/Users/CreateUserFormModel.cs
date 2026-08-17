using ClassSchedule2.Blazor.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Forms.Users
{
    public class CreateUserFormModel
    {
        [Required(ErrorMessage = "Fornavn er påkrævet.")]
        [StringLength(100, ErrorMessage = "Fornavnet må højst være 100 tegn.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Efternavn er påkrævet.")]
        [StringLength(100, ErrorMessage = "Efternavnet må højst være 100 tegn.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fødselsdato er påkrævet.")]
        public DateOnly? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Brugernavn er påkrævet.")]
        [MinLength(4, ErrorMessage = "Brugernavn skal være mindst 4 tegn.")]
        [MaxLength(16, ErrorMessage = "Brugernavn må højst være 16 tegn.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email er påkrævet.")]
        [EmailAddress(ErrorMessage = "Indtast en gyldig emailadresse.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adgangskode er påkrævet.")]
        [MinLength(8, ErrorMessage = "Adgangskoden skal være mindst 8 tegn.")]
        [MaxLength(16, ErrorMessage = "Adgangskoden må højst være 16 tegn.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vælg en rolle.")]
        public UserRoles Role { get; set; } = UserRoles.Student;

        public void Clear()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = null;
            Username = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Role = UserRoles.Student;
        }
    }
}
