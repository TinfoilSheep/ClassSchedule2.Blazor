using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.LessonTemplateLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.LessonTemplates
{
    public partial class LessonTemplateCard
    {
        [Inject]
        private ILessonTemplateService _lessonTemplateService { get; set; } = default!;

        private List<LessonTemplateDTO> _lessonTemplates = [];

        private bool _isLoading;
        private CrudModalMode _modalMode = CrudModalMode.None;

        private LessonTemplateDTO? _selectedLessonTemplate;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await LoadAsync();

            StateHasChanged();
        }

        private async Task LoadAsync()
        {
            _isLoading = true;

            try
            {
                var result = await _lessonTemplateService.GetAllAsync();

                _lessonTemplates = result ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LessonTemplateCard fejl: {ex}");
                _lessonTemplates = [];
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OpenCreateModal()
        {
            _selectedLessonTemplate = null;
            _modalMode = CrudModalMode.Create;
        }

        private void OpenEditModal(LessonTemplateDTO lessonTemplate)
        {
            _selectedLessonTemplate = lessonTemplate;
            _modalMode = CrudModalMode.Edit;
        }

        private void OpenDeleteModal(LessonTemplateDTO lessonTemplate)
        {
            _selectedLessonTemplate = lessonTemplate;
            _modalMode = CrudModalMode.Delete;
        }

        private void CloseModals()
        {
            _selectedLessonTemplate = null;
            _modalMode = CrudModalMode.None;
        }

        private async Task HandleSaved()
        {
            CloseModals();

            await LoadAsync();
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
    }
}