using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Models;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class DummyScheduleService : IScheduleService
    {
        public Task<List<ScheduleLesson>> GetScheduleAsync()
        {
            var lessons = new List<ScheduleLesson>
            {
                new()
                {
                    Id = Guid.Parse("faf7e936-fbd0-43f3-ad49-22288adb3014"),
                    Date = new DateOnly(2026, 8, 10),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(9, 0),
                    SubjectName = "Matematik",
                    HoldName = "1a Matematik",
                    RoomName = "Lokale A1",
                    Status = "Scheduled",
                    Teachers = ["Anders Jensen"]
                },
                new()
                {
                    Id = Guid.Parse("420b11ec-5631-4fca-9b6a-35cccd5f6755"),
                    Date = new DateOnly(2026, 8, 10),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(10, 0),
                    SubjectName = "Spansk",
                    HoldName = "1a Spansk",
                    RoomName = "Lokale A1",
                    Status = "Scheduled",
                    Teachers = ["Anders Jensen"]
                },
                new()
                {
                    Id = Guid.Parse("04300559-e3ff-448d-99c5-a168f5459721"),
                    Date = new DateOnly(2026, 8, 10),
                    StartTime = new TimeOnly(10, 0),
                    EndTime = new TimeOnly(11, 0),
                    SubjectName = "Coding",
                    HoldName = "1a Coding",
                    RoomName = "Lokale A1",
                    Status = "Scheduled",
                    Teachers = ["Anders Jensen"]
                },
                new()
                {
                    Id = Guid.Parse("3630fd62-6256-418d-9a58-966e6f46701f"),
                    Date = new DateOnly(2026, 8, 10),
                    StartTime = new TimeOnly(11, 0),
                    EndTime = new TimeOnly(12, 0),
                    SubjectName = "Engelsk",
                    HoldName = "1a Engelsk",
                    RoomName = "Lokale A1",
                    Status = "Scheduled",
                    Teachers = ["Bente Nielsen"]
                },
                new()
                {
                    Id = Guid.Parse("37d7ca41-ac4c-4a34-8b67-bc3d367c0450"),
                    Date = new DateOnly(2026, 8, 11),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(9, 0),
                    SubjectName = "Engelsk",
                    HoldName = "1a Engelsk",
                    RoomName = "Lokale A1",
                    Status = "Scheduled",
                    Teachers = ["Bente Nielsen"]
                },
                new()
                {
                    Id = Guid.Parse("f4d840fb-f021-451e-8274-1ce12f8f93b5"),
                    Date = new DateOnly(2026, 8, 11),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(10, 0),
                    SubjectName = "Matematik",
                    HoldName = "1a Matematik",
                    RoomName = "Lokale A1",
                    Status = "Scheduled",
                    Teachers = ["Anders Jensen"]
                },
                new()
                {
                    Id = Guid.Parse("b755ab9e-a7c6-44a1-8473-3513ac4251e6"),
                    Date = new DateOnly(2026, 8, 12),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(9, 0),
                    SubjectName = "Spansk",
                    HoldName = "1a Spansk",
                    RoomName = "Lokale A2",
                    Status = "Scheduled",
                    Teachers = ["Anders Jensen"]
                },
                new()
                {
                    Id = Guid.Parse("11b77d44-6f90-473d-9239-25bec6195172"),
                    Date = new DateOnly(2026, 8, 12),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(10, 0),
                    SubjectName = "Idræt",
                    HoldName = "1a Idræt",
                    RoomName = "Lab 1",
                    Status = "Scheduled",
                    Teachers = ["Bente Nielsen"]
                },
                new()
                {
                    Id = Guid.Parse("b385e28e-c3c7-4904-abff-b3ca48c249ce"),
                    Date = new DateOnly(2026, 8, 18),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(9, 0),
                    SubjectName = "Engelsk",
                    HoldName = "1a Engelsk",
                    RoomName = "Lokale A1",
                    Status = "Scheduled",
                    Teachers = ["Bente Nielsen"]
                },
                new()
                {
                    Id = Guid.Parse("77e2e94e-c74f-437f-ad42-6aae9d7c9923"),
                    Date = new DateOnly(2026, 8, 18),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(10, 0),
                    SubjectName = "Matematik",
                    HoldName = "1a Matematik",
                    RoomName = "Lokale A1",
                    Status = "Scheduled",
                    Teachers = ["Anders Jensen"]
                },
                new()
                {
                    Id = Guid.Parse("853b52ea-896a-433a-a664-8df1aacf597d"),
                    Date = new DateOnly(2026, 8, 19),
                    StartTime = new TimeOnly(8, 0),
                    EndTime = new TimeOnly(9, 0),
                    SubjectName = "Spansk",
                    HoldName = "1a Spansk",
                    RoomName = "Lokale A2",
                    Status = "Scheduled",
                    Teachers = ["Anders Jensen"]
                },
                new()
                {
                    Id = Guid.Parse("83a1fe82-8111-42a7-b09e-f91f227b5100"),
                    Date = new DateOnly(2026, 8, 19),
                    StartTime = new TimeOnly(9, 0),
                    EndTime = new TimeOnly(10, 0),
                    SubjectName = "Idræt",
                    HoldName = "1a Idræt",
                    RoomName = "Lab 1",
                    Status = "Scheduled",
                    Teachers = ["Bente Nielsen"]
                }
            };

            return Task.FromResult(lessons);
        }
    }
}
