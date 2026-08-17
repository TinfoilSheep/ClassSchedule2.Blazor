using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin
{
    public partial class EditUser
    {
        [Parameter] public GetAllUsersResponseDTO? User { get; set; }

        [Parameter]
        public EventCallback OnSaved { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }
    }
}
