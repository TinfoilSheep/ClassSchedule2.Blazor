using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Providers;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace ClassSchedule2.Blazor.Components.Layout
{
    public partial class MainLayout
    {
        [Inject] private SchoolAuthenticationStateProvider AuthenticationProvider { get; set; } = default!;
        [Inject] private ICurrentUser CurrentUser { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        private string _userName = string.Empty;
        private string _userInitials = string.Empty;
        private string _userRole = string.Empty;
        private bool _mobileMenuOpen;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await AuthenticationProvider.InitializeAsync();

            var user = await CurrentUser.GetAsync();

            if (user is not null)
            {
                _userName = $"{user.FirstName} {user.LastName}".Trim();

                _userRole = user.Role.ToString();

                _userInitials = user.Initials;
            }

            StateHasChanged();
        }

        private static string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "?";
            }

            var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
            {
                return parts[0][0].ToString().ToUpper();
            }

            return $"{parts[0][0]}{parts[^1][0]}".ToUpper();
        }

        private void ToggleMobileMenu()
        {
            _mobileMenuOpen = !_mobileMenuOpen;
        }

        private void CloseMobileMenu()
        {
            _mobileMenuOpen = false;
        }

        private async Task LogoutAsync()
        {
            await AuthenticationProvider.LogoutAsync();

            Navigation.NavigateTo("/login");
        }
    }
}
