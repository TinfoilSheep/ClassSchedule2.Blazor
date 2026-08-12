using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs.Request;
using ClassSchedule2.Blazor.Models.DTOs.Response;

namespace ClassSchedule2.Blazor.Services.Data
{
    public class InstitutionService : IInstitutionService
    {
        public Task CreateInstitution(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteInstitution(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<InstituionResponseDTO>> GetAllInstitutions()
        {
            throw new NotImplementedException();
        }

        public Task<InstituionResponseDTO> GetInstitutionById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<InstituionResponseDTO> UpdateInstitution(InstitutionRequestDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
