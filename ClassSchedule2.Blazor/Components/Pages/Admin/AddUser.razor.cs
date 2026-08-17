using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Enums;
using ClassSchedule2.Blazor.Models.Forms.Users;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.UserLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin;

public partial class AddUser
{
    [Inject]
    private IUserService UserService { get; set; } = default!;

    [Parameter]
    public EventCallback OnSaved { get; set; }

    [Parameter]
    public EventCallback OnCancel { get; set; }

    private CreateUserFormModel _form = new()
    {
        Role = UserRoles.Student
    };

    private bool _isSubmitting;
    private string? _errorMessage;

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
            var dto = new CreateUserRequestDTO {
                FirstName = _form.FirstName,
                LastName = _form.LastName,
                DateOfBirth = _form.DateOfBirth,
                Username = _form.Username,
                Password = _form.Password,
                Email = _form.Email,
                Role = _form.Role
            };

            var success = await UserService.AddUserAsync(dto);

            if (!success)
            {
                _errorMessage = "Brugeren kunne ikke oprettes. Kontroller oplysningerne og prøv igen.";
                return;
            }

            await OnSaved.InvokeAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fejl ved oprettelse af bruger: {ex}");

            _errorMessage = "Der opstod en fejl under oprettelsen af brugeren.";
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