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

        private string _searchText = "";
        private bool _isLoading = true;
        private bool _showModal;
        private UserModalMode _modalMode;

        private enum UserSortColumn
        {
            Name,
            Username,
            Email,
            DateOfBirth,
            Role,
            CreatedAt
        }

        private UserSortColumn _sortColumn = UserSortColumn.Name;
        private bool _sortAscending = true;

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

                _users = await UserService.GetAllUsersListAsync(currentUser.InstitutionId, null);
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

        private void OpenEditModal(GetAllUsersResponseDTO user)
        {
            _selectedUser = user;
            _modalMode = UserModalMode.Edit;
            _showModal = true;
        }

        private void OpenDeleteModal(GetAllUsersResponseDTO user)
        {
            _selectedUser = user;
            _modalMode = UserModalMode.Delete;
            _showModal = true;
        }

        private async Task HandleUserChanged()
        {
            _showModal = false;

            await LoadUsersAsync();
        }

        private void CloseModal()
        {
            _showModal = false;
        }

        private void ClearSearch()
        {
            _searchText = "";
        }

        private void SortBy(UserSortColumn column)
        {
            if (_sortColumn == column)
            {
                _sortAscending = !_sortAscending;
            }
            else
            {
                _sortColumn = column;
                _sortAscending = true;
            }
        }

        private IEnumerable<GetAllUsersResponseDTO> FilteredAndSortedUsers
        {
            get
            {
                IEnumerable<GetAllUsersResponseDTO> result = _users;

                // Søgning
                if (!string.IsNullOrWhiteSpace(_searchText))
                {
                    var search = _searchText.Trim();

                    result = result.Where(user => $"{user.FirstName} {user.LastName}".Contains(search, StringComparison.OrdinalIgnoreCase)
                        || (user.Username?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false)
                        || (user.Email?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false));
                }

                // Sortering
                result = _sortColumn switch
                {
                    UserSortColumn.Name => _sortAscending ? result.OrderBy(x => x.FirstName).ThenBy(x => x.LastName) : result.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName),

                    UserSortColumn.Username => _sortAscending ? result.OrderBy(x => x.Username) : result.OrderByDescending(x => x.Username),

                    UserSortColumn.Email => _sortAscending ? result.OrderBy(x => x.Email) : result.OrderByDescending(x => x.Email),

                    UserSortColumn.DateOfBirth => _sortAscending ? result.OrderBy(x => x.DateOfBirth) : result.OrderByDescending(x => x.DateOfBirth),

                    UserSortColumn.Role => _sortAscending ? result.OrderBy(x => x.Role) : result.OrderByDescending(x => x.Role),

                    UserSortColumn.CreatedAt => _sortAscending ? result.OrderBy(x => x.CreatedAt) : result.OrderByDescending(x => x.CreatedAt),

                    _ => result
                };

                return result;
            }
        }
    }
}
