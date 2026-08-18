using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.Terms;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Terms
{
    public partial class AddTerm
    {
        [Inject]
        private ITermService _termService { get; set; } = default!;
        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private CreateTermFormModel _form = new();
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
                var dto = new CreateTermDTO(Name: _form.Name, StartDate: _form.StartDate, EndDate: _form.EndDate);
                var result = await _termService.CreateTermAsync(dto);

                if (result is null)
                {
                    _errorMessage = "Terminen kunne ikke oprettes. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under oprettelsen af terminen.";
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
