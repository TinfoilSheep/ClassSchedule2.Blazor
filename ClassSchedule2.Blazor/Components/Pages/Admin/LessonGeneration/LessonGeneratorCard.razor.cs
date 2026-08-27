using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using Microsoft.AspNetCore.Components;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.LessonGeneration
{
    public partial class LessonGeneratorCard
    {
        [Inject]
        private ILessonGenerationService _lessonGenerationService { get; set; } = default!;

        [Inject]
        private ITermService _termService { get; set; } = default!;

        [Parameter]
        public EventCallback OnGenerated { get; set; }

        private List<TermLibrary.TermDTO> _terms = [];

        private Guid _selectedTermId = Guid.Empty;

        private bool _isLoading;
        private bool _isGenerating;

        private string? _errorMessage;
        private string? _successMessage;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await LoadTermsAsync();

            StateHasChanged();
        }


        private async Task LoadTermsAsync()
        {
            _isLoading = true;
            _errorMessage = null;

            try
            {
                _terms = await _termService.GetAllTermsAsync();

                if (_terms.Count == 0)
                {
                    _errorMessage = "Der blev ikke fundet nogen termer.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved hentning af termer: {ex}");

                _terms = [];
                _errorMessage = "Termer kunne ikke indlæses. Prøv igen.";
            }
            finally
            {
                _isLoading = false;
            }
        }


        private async Task GenerateLessonsAsync()
        {
            if (_isGenerating || _selectedTermId == Guid.Empty)
            {
                return;
            }

            _isGenerating = true;
            _errorMessage = null;
            _successMessage = null;

            try
            {
                var generatedCount = await _lessonGenerationService.GenerateForTermAsync(_selectedTermId);

                if (generatedCount == -1)
                {
                    _errorMessage = "Skemaet kunne ikke genereres. Kontrollér dine lektionsplaner og prøv igen.";

                    return;
                }

                _successMessage = generatedCount == 1 ? "1 lektion blev genereret." : $"{generatedCount} lektioner blev genereret.";

                await OnGenerated.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved generering af skema: {ex}");

                _errorMessage = "Der opstod en fejl under genereringen af skemaet.";
            }
            finally
            {
                _isGenerating = false;
            }
        }
    }
}
