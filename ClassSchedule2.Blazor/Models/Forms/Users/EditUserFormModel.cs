using ClassSchedule2.Blazor.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Forms.Users
{
    public class EditUserFormModel
    {
        [Required(ErrorMessage = "Fornavn er påkrævet.")]
        [StringLength(100, ErrorMessage = "Fornavnet må højst være 100 tegn.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Efternavn er påkrævet.")]
        [StringLength(100, ErrorMessage = "Efternavnet må højst være 100 tegn.")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fødselsdato er påkrævet.")]
        public DateOnly? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email er påkrævet.")]
        [EmailAddress(ErrorMessage = "Indtast en gyldig emailadresse.")]
        public string Email { get; set; } = string.Empty;

        public void Clear()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = null;
            Email = string.Empty;
        }
    }
}
