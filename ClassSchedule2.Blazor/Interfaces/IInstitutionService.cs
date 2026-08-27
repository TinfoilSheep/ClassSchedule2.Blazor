using static ClassSchedule2.Blazor.Models.DTOs.InstitutionLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IInstitutionService
    {
        public Task<List<GetInstitutionListResponseDTO>> GetAllInstitutions();
        public Task<GetInstitutionListResponseDTO?> GetInstitutionById(Guid id);
    }
}
