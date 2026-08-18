using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;

namespace ClassSchedule2.Blazor.Interfaces
{
    public interface ITermService
    {
        public Task<List<TermDTO>> GetAllTermsAsync();
        public Task<TermDTO?> GetTermByIdAsync(Guid termId);
        public Task<TermDTO?> CreateTermAsync(CreateTermDTO dto);
        public Task<TermDTO?> UpdateTermAsync(TermDTO dto);
        public Task<bool> DeleteTermAsync(Guid termId);
    }
}
