using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Forms.Users;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Profile
{
    public partial class EditCredentials
    {
        [Inject] private IUserService UserService { get; set; } = default!;

        [Parameter, EditorRequired] public GetUserInformationResponseDTO User { get; set; } = default!;
        [Parameter] public EventCallback<ProfileModalMode> OnSaved { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }

        private EditCredentialsFormModel _form = new();

        private string? _currentUsername;
        private bool _isCheckingUsername;
        private bool? _usernameIsAvailable;
        private bool _isSubmitting;
        private string? _errorMessage;


        protected override void OnParametersSet()
        {
            _currentUsername = User.Username;

            _form = new EditCredentialsFormModel
            {
                Username = User.Username
            };

            _errorMessage = null;
        }

        private async Task CheckUsernameAsync()
        {
            if (_isSubmitting || _isCheckingUsername)
            {
                return;
            }

            var username = _form.Username?.Trim();

            if (string.IsNullOrWhiteSpace(username) || username == _currentUsername)
            {
                return;
            }

            _isCheckingUsername = true;
            _usernameIsAvailable = null;
            _errorMessage = null;

            try
            {
                _usernameIsAvailable = await UserService.CheckUsernameIsAvailableAsync(username);
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under tjek af brugernavnet.";
            }
            finally
            {
                _isCheckingUsername = false;
            }
        }

        private async Task HandleSubmitAsync()
        {
            if (_isSubmitting)
            {
                return;
            }

            _isSubmitting = true;
            _errorMessage = null;

            try
            {
                var dto = new ChangeUserCredentialsDTO(_form.Username.Trim(), _form.OldPassword, _form.NewPassword);

                var result = await UserService.ChangeUserCredentialsAsync(dto);

                if (!result)
                {
                    _errorMessage = "Der opstod en fejl under opdateringen af dine loginoplysninger.";
                    return;
                }

                await OnSaved.InvokeAsync(ProfileModalMode.EditCredentials);
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under opdateringen af dine loginoplysninger.";
            }
            finally
            {
                _isSubmitting = false;
            }
        }


        private async Task Cancel()
        {
            if (_isSubmitting)
            {
                return;
            }

            await OnCancel.InvokeAsync();
        }
    }
}