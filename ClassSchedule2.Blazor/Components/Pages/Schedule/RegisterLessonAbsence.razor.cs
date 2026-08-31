using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.LessonLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Schedule
{
    public partial class RegisterLessonAbsence
    {
        [Inject] private ILessonService LessonService { get; set; } = default!;

        [Parameter, EditorRequired] public LessonDTO Lesson { get; set; } = default!;
        [Parameter] public List<MinimalUserInformationDTO> Students { get; set; } = [];

        [Parameter] public EventCallback OnCancel { get; set; }
        [Parameter] public EventCallback OnSaved { get; set; }

        private string _content = string.Empty;
        private bool _isSubmitting;

        private async Task HandleSubmitAsync()
        {
            try
            {

                await OnSaved.InvokeAsync();
            }
            catch
            {

            }
            finally
            {
                _isSubmitting = false;
            }
        }

        private async Task Cancel()
        {
            if (_isSubmitting)
            {
                return;
            }

            await OnCancel.InvokeAsync();
        }
    }
}
