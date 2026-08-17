using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ISubjectService
    {
        public Task<List<SubjectDTO>> GetAllSubjectsAsync();
        public Task<SubjectDTO?> GetSubjectByIdAsync(Guid subjectId);
        public Task<SubjectDTO?> CreateSubjectAsync(CreateSubjectDTO dto);
        public Task<SubjectDTO?> UpdateSubjectAsync(SubjectDTO dto);
        public Task<bool> DeleteSubjectAsync(Guid subjectId);
    }
}
