using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.LessonLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Schedule
{
    public partial class LessonStudents
    {
        [Parameter, EditorRequired]
        public List<MinimalUserInformationDTO> Students { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;


        [Parameter]
        public EventCallback OnCancel { get; set; }

        private void ShowTargetProfile(MinimalUserInformationDTO user)
        {
            Navigation.NavigateTo($"/profile/{user.UserId}");
        }
    }
}
