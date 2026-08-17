using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.DTOs;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Forms.Users;
using ClassSchedule2.Blazor.Providers;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin
{
    public partial class Users
    {
        [Inject] private SchoolAuthenticationStateProvider AuthenticationProvider { get; set; } = default!;
        [Inject] private IUserService UserService { get; set; } = default!;
        [Inject] private ICurrentUserProvider CurrentUser { get; set; } = default!;

        private List<GetAllUsersResponseDTO> _users = [];
        private GetAllUsersResponseDTO? _selectedUser;

        private bool _isLoading = true;
        private bool _showModal;
        private UserModalMode _modalMode;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await AuthenticationProvider.InitializeAsync();

            await LoadUsersAsync();

            StateHasChanged();
        }

        private async Task LoadUsersAsync()
        {
            _isLoading = true;

            try
            {
                var currentUser = await CurrentUser.GetAsync();

                if (currentUser is null)
                {
                    _users = [];
                    return;
                }

                _users = await UserService.GetAllUsersListAsync(currentUser.InstitutionId);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OpenCreateModal()
        {
            _modalMode = UserModalMode.Create;
            _showModal = true;
        }

        private async Task HandleUserSaved()
        {
            _showModal = false;

            await LoadUsersAsync();
        }

        private void OpenEditModal(GetAllUsersResponseDTO user)
        {
            _selectedUser = user;
            _modalMode = UserModalMode.Edit;
            _showModal = true;
        }

        private object ConfirmDelete(GetAllUsersResponseDTO user)
        {
            throw new NotImplementedException();
        }

        private void CloseModal()
        {
            _showModal = false;
        }
    }
}
