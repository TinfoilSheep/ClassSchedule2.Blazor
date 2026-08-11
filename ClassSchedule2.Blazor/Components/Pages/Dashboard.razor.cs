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
        [Inject]
        IJSRuntime JS { get; set; } = default!;
        [Inject]
        private BrowserAuthService BrowserAuthService { get; set; } = default!;
        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
        [Inject]
        private SchoolAuthenticationStateProvider SchoolAuthenticationProvider { get; set; } = default!;

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (!firstRender)
        //        return;

        //    await SchoolAuthenticationProvider.InitializeAsync();

        //    var authState = await AuthStateProvider.GetAuthenticationStateAsync();

        //    var user = authState.User;
        //}

        //protected override async Task OnInitializedAsync()
        //{
        //    var authState = await AuthStateProvider.GetAuthenticationStateAsync();

        //    var user = authState.User;

        //    Console.WriteLine($"Authenticated: {user.Identity?.IsAuthenticated}");

        //    Console.WriteLine($"Name: {user.Identity?.Name}");

        //    Console.WriteLine($"Role: {user.FindFirst(ClaimTypes.Role)?.Value}");
        //}
    }
}
