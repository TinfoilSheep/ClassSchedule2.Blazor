namespace ClassSchedule2.Blazor.Models.Models
{
    public class ScheduleLesson
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string HoldName { get; set; } = string.Empty;
        public string RoomName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public List<string> Teachers { get; set; } = [];
    }
}
