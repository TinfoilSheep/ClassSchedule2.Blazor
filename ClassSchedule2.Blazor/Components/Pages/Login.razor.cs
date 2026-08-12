using ClassSchedule2.Blazor.Models.DTOs.Request;
using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace ClassSchedule2.Blazor.Components.Pages
{
    public partial class Login
    {

        [Inject]
        private NavigationManager Navigation { get; set; } = default!;
        [Inject]
        private BrowserAuthService BrowserAuthService { get; set; } = default!;
        protected LoginRequestDTO LoginModel { get; set; } = new();
        protected bool IsSubmitting { get; set; } = false;
        protected string? ErrorMessage { get; set; }

        protected override async Task OnInitialized(bool firstRender)
        {
            if (!firstRender)
                return;
            
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
                        403 => "Ugyldigt brugernavn eller adgangskode.",
                        _ => result.ResponseText ?? "Login mislykkedes."
                    };

                    return;
                }

                // TODO Tjek efter brugeren rolle da det er nok kun administrator der har et Dashboard.
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
