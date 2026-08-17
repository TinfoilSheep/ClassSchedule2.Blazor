using static ClassSchedule2.Blazor.Models.DTOs.InstitutionLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IInstitutionService
    {
        public Task CreateInstitution(CreateInstitutionDTO dto);
        public Task<GetInstitutionListResponseDTO> UpdateInstitution(UpdateInstitutionRequestDTO dto);
        public Task<List<GetInstitutionListResponseDTO>> GetAllInstitutions();
        public Task<bool> DeleteInstitution(DeleteInstitutionRequestDTO dto);
        public Task<GetInstitutionListResponseDTO?> GetInstitutionById(Guid id);
    }
}
