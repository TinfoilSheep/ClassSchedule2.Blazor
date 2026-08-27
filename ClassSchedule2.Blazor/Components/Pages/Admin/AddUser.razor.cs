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

            var success = await UserService.CreateUserAsync(dto);

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

    private string GetRoleCardClass(UserRoles role)
    {
        var selected = _form.Role == role;

        if (role == UserRoles.Student)
        {
            return selected
                ? "flex w-full cursor-pointer items-center justify-between rounded-xl border border-sky-400 bg-sky-400/10 px-4 py-4 text-left shadow-sm ring-1 ring-sky-400/20 transition-all dark:border-sky-500/70 dark:bg-sky-400/10"
                : "flex w-full cursor-pointer items-center justify-between rounded-xl border border-slate-200 bg-white px-4 py-4 text-left transition-all hover:border-sky-300 hover:bg-sky-50/50 dark:border-slate-700 dark:bg-slate-950 dark:hover:border-sky-700 dark:hover:bg-sky-950/20";
        }

        return selected
            ? "flex w-full cursor-pointer items-center justify-between rounded-xl border border-amber-400 bg-amber-400/10 px-4 py-4 text-left shadow-sm ring-1 ring-amber-400/20 transition-all dark:border-amber-500/70 dark:bg-amber-400/10"
            : "flex w-full cursor-pointer items-center justify-between rounded-xl border border-slate-200 bg-white px-4 py-4 text-left transition-all hover:border-amber-300 hover:bg-amber-50/50 dark:border-slate-700 dark:bg-slate-950 dark:hover:border-amber-700 dark:hover:bg-amber-950/20";
    }

    private string GetRoleIconClass(UserRoles role)
    {
        if (role == UserRoles.Student)
        {
            return _form.Role == role
                ? "flex h-10 w-10 shrink-0 items-center justify-center rounded-xl bg-sky-400/15 text-sky-600 dark:bg-sky-400/10 dark:text-sky-400"
                : "flex h-10 w-10 shrink-0 items-center justify-center rounded-xl bg-slate-100 text-slate-500 dark:bg-slate-800 dark:text-slate-400";
        }

        return _form.Role == role
            ? "flex h-10 w-10 shrink-0 items-center justify-center rounded-xl bg-amber-400/15 text-amber-600 dark:bg-amber-400/10 dark:text-amber-400"
            : "flex h-10 w-10 shrink-0 items-center justify-center rounded-xl bg-slate-100 text-slate-500 dark:bg-slate-800 dark:text-slate-400";
    }
}