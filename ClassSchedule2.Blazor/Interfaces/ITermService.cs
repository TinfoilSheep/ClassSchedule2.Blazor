using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ITermService
    {
        public Task<List<TermDTO>> GetAllTermsAsync();
        public Task<TermDTO?> GetTermByIdAsync(Guid termId);
        public Task<bool> CreateTermAsync(CreateTermDTO dto);
        public Task<bool> UpdateTermAsync(TermDTO dto);
        public Task<bool> DeleteTermAsync(Guid termId);
    }
}
