using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Forms.Users
{
    public class EditCredentialsFormModel
    {
        [Required(ErrorMessage = "Indtast brugernavn")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Indtast gammel adgangskode")]
        [MinLength(8, ErrorMessage = "Adgangskoden skal være på mindst 8 tegn")]
        public string OldPassword { get; set; } = "";

        [Required(ErrorMessage = "Indtast ny adgangskode")]
        [MinLength(8, ErrorMessage = "Adgangskoden skal være på mindst 8 tegn")]
        public string NewPassword { get; set; } = "";

        [Required(ErrorMessage = "Bekræft ny adgangskode")]
        [Compare("NewPassword", ErrorMessage = "De indtastede adgangskoder matcher ikke")]
        public string ConfirmPassword { get; set; } = "";
    }
}
