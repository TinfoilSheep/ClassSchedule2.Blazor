namespace ClassSchedule2.Blazor.Models.DTOs
{
    public class InstitutionLibrary
    {
        public record CreateInstitutionDTO(string Name);
        public record UpdateInstitutionRequestDTO(Guid Id, string Name);
        public record UpdateInstitutionResponseDTO(Guid Id, string Name);
        public record GetInstitutionListResponseDTO(Guid Id, string Name);
        public record GetInstitutionByIdRequestDTO(Guid Id);
        public record DeleteInstitutionRequestDTO(Guid Id);
    }
}
