using ClassSchedule2.Blazor.Models.DTOs.Request;
using ClassSchedule2.Blazor.Models.DTOs.Response;
using System.Security.Cryptography.X509Certificates;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface IInstitutionService
    {
        public Task CreateInstitution(string name);
        public Task<InstituionResponseDTO> UpdateInstitution(InstitutionRequestDTO dto);
        public Task<List<InstituionResponseDTO>> GetAllInstitutions();
        public Task<bool> DeleteInstitution(Guid id);
        public Task<InstituionResponseDTO> GetInstitutionById(Guid id);
    }
}
