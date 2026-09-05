using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using Microsoft.AspNetCore.Components;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.LessonGeneration
{
    public partial class LessonGeneratorCard
    {
        private bool showGenerateLessonModal = false;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            StateHasChanged();
        }

        private void ShowGenerateLessonModal()
        {
            showGenerateLessonModal = true;
            StateHasChanged();
        }

        private void CloseModal()
        {
            showGenerateLessonModal = false;
            StateHasChanged();
        }

        private async Task HandleGenerate()
        {
            CloseModal();
            StateHasChanged();
        }
    }
}
