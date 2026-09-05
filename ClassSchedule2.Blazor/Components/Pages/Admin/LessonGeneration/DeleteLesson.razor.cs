using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.LessonGeneration;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.LessonGeneratorLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.LessonTemplateLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.LessonGeneration
{
    public partial class DeleteLesson
    {
        [Inject]
        private ILessonGenerationService _lessonGenerationService { get; set; } = default!;

        [Inject]
        private ITermService _termService { get; set; } = default!;

        [Inject]
        private ILessonTemplateService _lessonTemplateService { get; set; } = default!;

        [Parameter]
        public EventCallback OnDeletion { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private GenerateLessonFormModel _form = new();

        private List<TermDTO> _terms = [];
        private List<LessonTemplateDTO> _lessonTemplates = [];
        private List<LessonTemplateDTO> _availableLessonTemplates = [];

        private List<LessonTemplateDTO> _selectedLessonTemplates = [];

        private bool _isLoading;
        private bool _isDeleting;

        private string? _errorMessage;
        private string? _successMessage;

        protected override async Task OnParametersSetAsync()
        {
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
                _lessonTemplates = await _lessonTemplateService.GetAllAsync();
                _availableLessonTemplates = await _lessonTemplateService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved hentning af termer og lektions planer: {ex}");
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task OnTermChanged()
        {
            TermDTO? selectedTerm = _terms.FirstOrDefault(t => t.Id == _form.TermId);

            if (selectedTerm == null)
            {
                _availableLessonTemplates = _lessonTemplates;
                return;
            }

            DateOnly termStartDate = selectedTerm.StartDate;
            DateOnly termEndDate = selectedTerm.EndDate;

            _availableLessonTemplates = _lessonTemplates
                .Where(lt => lt.ValidFrom >= termStartDate && lt.ValidTo <= termEndDate)
                .ToList();

            _selectedLessonTemplates.RemoveAll(lt => !_availableLessonTemplates.Contains(lt));
        }

        private void AddTemplate(LessonTemplateDTO lessonTemplate)
        {
            _availableLessonTemplates.Remove(lessonTemplate);
            _selectedLessonTemplates.Add(lessonTemplate);
        }

        private void RemoveTemplate(LessonTemplateDTO lessonTemplate)
        {
            _selectedLessonTemplates.Remove(lessonTemplate);
            _availableLessonTemplates.Add(lessonTemplate);
        }

        private static string GetWeekDayName(int weekDay)
        {
            return weekDay switch
            {
                1 => "Mandag",
                2 => "Tirsdag",
                3 => "Onsdag",
                4 => "Torsdag",
                5 => "Fredag",
                6 => "Lørdag",
                7 => "Søndag",
                _ => "Ukendt"
            };
        }

        private async Task DeleteLessonsAsync()
        {
            if (_isDeleting || _form.TermId == Guid.Empty)
            {
                return;
            }

            _isDeleting = true;
            _errorMessage = null;
            _successMessage = null;

            try
            {
                DeleteLessonDTO dto = new(_selectedLessonTemplates.Select(lt => lt.Id).ToList());

                var generatedCount = await _lessonGenerationService.DeleteLessonFromTemplate(dto);

                if (generatedCount == -1)
                {
                    _errorMessage = "Lektioner kunne slettes. Kontrollér dine lektionsplaner og prøv igen.";

                    return;
                }

                _successMessage = generatedCount == 1 ? "1 lektion blev slettet." : $"{generatedCount} lektioner blev slettet.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved sletning af lektioner: {ex}");

                _errorMessage = "Der opstod en fejl under sletningen af lektioner.";
            }
            finally
            {
                _isDeleting = false;
            }
        }

        private async Task ClosePage()
        {
            if (_isDeleting)
            {
                return;
            }

            await OnCancel.InvokeAsync();
        }
    }
}
