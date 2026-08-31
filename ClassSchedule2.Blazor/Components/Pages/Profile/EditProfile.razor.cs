using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Forms.Users;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Profile
{
    public partial class EditProfile
    {
        [Inject] private IUserService UserService { get; set; } = default!;

        [Parameter, EditorRequired] public GetUserInformationResponseDTO User { get; set; } = default!;
        [Parameter] public EventCallback<ProfileModalMode> OnSaved { get; set; }
        [Parameter] public EventCallback OnCancel { get; set; }

        private EditUserFormModel _form = new();

        private bool _isSubmitting;
        private string? _errorMessage;


        protected override void OnParametersSet()
        {
            _form = new EditUserFormModel
            {
                FirstName = User.FirstName,
                LastName = User.LastName,
                DateOfBirth = User.DateOfBirth,
                Email = User.Email
            };

            _errorMessage = null;
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
                var dto = new UpdateUserInformationDTO(
                    _form.FirstName.Trim(),
                    _form.LastName.Trim(),
                    _form.DateOfBirth!.Value,
                    _form.Email.Trim());

                var updatedUser = await UserService.UpdateUserAsync(dto);

                await OnSaved.InvokeAsync(ProfileModalMode.EditProfile);
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under opdateringen af dine oplysninger.";
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