using ClassSchedule2.Blazor.Models.DTOs.Response;

namespace ClassSchedule2.Blazor.Models.Models
{
    public class BrowserLoginResult
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public string? ResponseText { get; set; }
        public LoginResponseDTO? User { get; set; }
    }
}
