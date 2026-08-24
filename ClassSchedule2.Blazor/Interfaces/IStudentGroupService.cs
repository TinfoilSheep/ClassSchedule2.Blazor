using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.StudentGroupLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IStudentGroupService
    {
        public Task<List<StudentGroupDTO>> GetAll();
        public Task<StudentGroupDTO?> Get(Guid studentGroupId);
        public Task<bool> Create(CreateStudentGroupDTO dto);
        public Task<bool> Update(EditStudentGroupDTO dto);
        public Task<bool> Delete(Guid studentGroupId);
    }
}
