using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin
{
    public partial class DeleteUser
    {
        [Parameter]
        public GetUserInformationResponseDTO User { get; set; } = default!;

        [Parameter]
        public EventCallback OnCancel { get; set; }

        [Parameter]
        public EventCallback OnDeleted { get; set; }

        [Inject] public IUserService UserService { get; set; } = default!;

        private bool _isDeleting;
        private string? _errorMessage;

        private string _userName => $"{User.FirstName} {User.LastName}".Trim();

        private string? _username => User.Username;

        private async Task DeleteAsync()
        {
            _isDeleting = true;
            _errorMessage = null;

            try
            {
                var success = await UserService.DeleteUserAsync(User.Id);

                if (!success)
                {
                    _errorMessage = "Brugeren kunne ikke slettes.";
                    return;
                }

                await OnDeleted.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = $"Der opstod en fejl under sletning af brugeren.";
            }
            finally
            {
                _isDeleting = false;
            }
        }

        private async Task Cancel()
        {
            await OnCancel.InvokeAsync();
        }
    }
}
