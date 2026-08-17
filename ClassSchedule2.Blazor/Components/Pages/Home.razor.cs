using ClassSchedule2.Blazor.Providers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClassSchedule2.Blazor.Components.Pages
{
    public partial class Home
    {
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private SchoolAuthenticationStateProvider AuthenticationProvider { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await AuthenticationProvider.InitializeAsync();

            var authState = await AuthStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity?.IsAuthenticated == true)
            {
                Navigation.NavigateTo("/dashboard", replace: true);
                return;
            }

            StateHasChanged();
        }

        protected void NavigateToLogin()
        {
            Navigation.NavigateTo("/login");
        }

        protected void NavigateToRegisterRequest()
        {
            Navigation.NavigateTo("/register");
        }
    }
}
