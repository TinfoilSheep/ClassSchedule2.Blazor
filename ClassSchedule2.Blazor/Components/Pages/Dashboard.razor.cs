using ClassSchedule2.Blazor.Providers;
using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace ClassSchedule2.Blazor.Components.Pages
{
    public partial class Dashboard
    {
        [Inject] private SchoolAuthenticationStateProvider AuthenticationProvider { get; set; } = default!;

        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private async Task LogoutAsync()
        {
            await AuthenticationProvider.LogoutAsync();

            Navigation.NavigateTo("/login");
        }

    }
}
