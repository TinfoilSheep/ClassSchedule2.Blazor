using System.ComponentModel.DataAnnotations;

namespace ClassSchedule2.Blazor.Models.Forms.Rooms
{
    public class CreateRoomFormModel
    {
        [Required(ErrorMessage = "Navn på lokale er påkrævet.")]
        [StringLength(100, ErrorMessage = "Navnet må højst være 100 tegn.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kapacitet er påkrævet.")]
        [Range(1, int.MaxValue, ErrorMessage = "Kapacitet skal være et positivt tal.")]
        public int? Capacity { get; set; } = 20;
    }
}
