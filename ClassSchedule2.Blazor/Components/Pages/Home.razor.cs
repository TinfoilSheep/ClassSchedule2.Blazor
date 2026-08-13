using Microsoft.AspNetCore.Components;

namespace ClassSchedule2.Blazor.Components.Pages
{
    public partial class Home
    {
        [Inject] private NavigationManager Navigation { get; set; } = default!;

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
