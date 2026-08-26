using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.LessonTemplateLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.LessonTemplates
{
    public partial class DeleteLessonTemplate
    {
        [Inject]
        private ILessonTemplateService _lessonTemplateService { get; set; } = default!;

        [Parameter]
        public LessonTemplateDTO? LessonTemplate { get; set; }

        [Parameter]
        public EventCallback OnDeleted { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private bool _isDeleting;

        private async Task DeleteAsync()
        {
            if (_isDeleting || LessonTemplate is null)
            {
                return;
            }

            _isDeleting = true;

            try
            {
                var success = await _lessonTemplateService.DeleteAsync(LessonTemplate.Id);

                if (success)
                {
                    await OnDeleted.InvokeAsync();
                }
            }
            finally
            {
                _isDeleting = false;
            }
        }

        private async Task Cancel()
        {
            if (_isDeleting)
            {
                return;
            }

            await OnCancel.InvokeAsync();
        }
    }
}
