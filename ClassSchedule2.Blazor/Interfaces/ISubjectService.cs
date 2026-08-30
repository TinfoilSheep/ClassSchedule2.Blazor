using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ISubjectService
    {
        public Task<List<SubjectDTO>> GetAllSubjectsAsync();
        public Task<SubjectDTO?> GetSubjectByIdAsync(Guid subjectId);
        public Task<bool> CreateSubjectAsync(CreateSubjectDTO dto);
        public Task<bool> UpdateSubjectAsync(SubjectDTO dto);
        public Task<bool> DeleteSubjectAsync(Guid subjectId);
    }
}
