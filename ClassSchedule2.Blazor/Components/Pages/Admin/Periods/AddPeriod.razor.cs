using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.Periods;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Periods
{
    public partial class AddPeriod
    {
        [Inject]
        private IPeriodService _periodService { get; set; } = default!;
        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private CreatePeriodFormModel _form = new();
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
                var dto = new CreatePeriodDTO(Name: _form.Name, StartTime: _form.StartTime, EndTime: _form.EndTime);
                var result = await _periodService.CreatePeriodAsync(dto);

                if (result is null)
                {
                    _errorMessage = "Perioden kunne ikke oprettes. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under oprettelsen af perioden.";
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
