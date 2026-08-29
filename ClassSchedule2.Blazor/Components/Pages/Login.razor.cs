using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Models;
using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using static ClassSchedule2.Blazor.Models.DTOs.InstitutionLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.AuthLibrary;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ClassSchedule2.Blazor.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClassSchedule2.Blazor.Components.Pages
{
    public partial class Login
    {

        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private BrowserAuthService BrowserAuthService { get; set; } = default!;
        [Inject] private IInstitutionService InstitutionService { get; set; } = default!;
        [Inject] private SchoolAuthenticationStateProvider AuthenticationProvider { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = default!;
        protected LoginRequestDTO LoginModel { get; set; } = new();
        private List<GetInstitutionListResponseDTO> Institutions { get; set; } = new();
        protected bool IsSubmitting { get; set; } = false;
        protected string? ErrorMessage { get; set; }
        private bool IsLoadingInstitutions = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var result = await InstitutionService.GetAllInstitutions();
                if (result is not null)
                {
                    Institutions = result;
                }
            }
            catch (Exception)
            {
                ErrorMessage = "Kunne ikke hente institutioner. Prøv venligst igen senere.";
            }
            finally
            {
                IsLoadingInstitutions = false;
            }
        }

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

        protected void NavigateToHome()
        {
            Navigation.NavigateTo("/");
        }

        protected async Task HandleLoginAsync()
        {
            IsSubmitting = true;
            ErrorMessage = null;

            try
            {
                var result = await BrowserAuthService.LoginAsync(LoginModel);

                if (!result.Success)
                {
                    ErrorMessage = result.Status switch
                    {
                        403 => "Ugyldigt brugernavn eller adgangskode.", _ => result.ResponseText ?? "Login mislykkedes."
                    };

                    return;
                }

                Navigation.NavigateTo("/dashboard");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Fejl under login: {ex.Message}";
            }
            finally
            {
                IsSubmitting = false;
            }
        }
    }
}
