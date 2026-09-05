using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Models;
using ClassSchedule2.Blazor.Providers;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Profile
{
    public partial class Profile
    {
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private ICurrentUserProvider CurrentUserProvider { get; set; } = default!;
        [Inject] private SchoolAuthenticationStateProvider AuthenticationProvider { get; set; } = default!;

        [Parameter] public Guid? TargetUserId { get; set; }

        private ProfileModalMode _modalMode { get; set; }

        private GetUserInformationResponseDTO? _user;
        private Guid _currentUserId;

        private bool _showSuccessMessage;
        private string? _successMessage;
        private CancellationTokenSource? _successMessageCts;

        private bool _isLoading = true;
        private bool CanEdit => _user is not null && _user.Id == _currentUserId;

        protected override async Task OnParametersSetAsync()
        {
            await Load();
            StateHasChanged();
        }

        private async Task Load() 
        {
            try
            {
                await AuthenticationProvider.InitializeAsync();

                var currentUser = await CurrentUserProvider.GetAsync();

                if (currentUser is null)
                {
                    return;
                }

                _currentUserId = currentUser.UserId;

                if (TargetUserId.HasValue && TargetUserId != Guid.Empty)
                {
                    _user = await UserService.GetUserInformationAsync(TargetUserId.Value);
                }
                else
                {
                    _user = await UserService.GetUserInformationAsync(_currentUserId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved hentning af profil: {ex}");
                _user = null;
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task ShowSuccessMessageAsync(string message)
        {
            _successMessageCts?.Cancel();
            _successMessageCts?.Dispose();

            var cts = new CancellationTokenSource();
            _successMessageCts = cts;

            _successMessage = message;
            _showSuccessMessage = true;

            await InvokeAsync(StateHasChanged);

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(5), cts.Token);

                if (!cts.IsCancellationRequested)
                {
                    _showSuccessMessage = false;

                    await InvokeAsync(StateHasChanged);

                    // Vent på at exit-animationen er færdig
                    await Task.Delay(350, cts.Token);

                    if (!cts.IsCancellationRequested)
                    {
                        _successMessage = null;
                        await InvokeAsync(StateHasChanged);
                    }
                }
            }
            catch (TaskCanceledException)
            {
            }
        }

        private void ClearSuccessMessage()
        {
            _successMessageCts?.Cancel();
            _successMessageCts?.Dispose();
            _successMessageCts = null;

            _successMessage = null;
        }

        private void OpenEditUserModal()
        {
            ClearSuccessMessage();
            _modalMode = ProfileModalMode.EditProfile;
        }

        private void OpenEditCredentialsModal()
        {
            ClearSuccessMessage();
            _modalMode = ProfileModalMode.EditCredentials;
        }

        private string GetInitials()
        {
            if (_user is null)
                return "";

            var first = _user.FirstName.FirstOrDefault();
            var last = _user.LastName.FirstOrDefault();

            return $"{first}{last}".ToUpper();
        }

        private void CloseModals()
        {
            _modalMode = ProfileModalMode.None;
        }

        private async Task HandleSaved(ProfileModalMode updateType)
        {
            CloseModals();

            await Load();

            var message = updateType switch
            {
                ProfileModalMode.EditProfile => "Dine personlige oplysninger er blevet opdateret.",

                ProfileModalMode.EditCredentials => "Dine login oplysninger er blevet opdateret.",

                _ => null
            };

            if (message != null)
            {
                await ShowSuccessMessageAsync(message);
            }

            StateHasChanged();
        }
    }
}