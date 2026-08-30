using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.Periods;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Periods
{
    public partial class EditPeriod
    {
        [Inject]
        private IPeriodService _periodService { get; set; } = default!;

        [Parameter]
        public PeriodDTO? Period { get; set; }
        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private EditPeriodFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;


        protected override void OnParametersSet()
        {
            if (Period is null)
            {
                return;
            }

            _form.Id = Period.Id;
            _form.Name = Period.Name;
            _form.StartTime = Period.StartTime;
            _form.EndTime = Period.EndTime;

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
                var dto = new PeriodDTO(Id: _form.Id, Name: _form.Name, StartTime: _form.StartTime, EndTime: _form.EndTime);
                var result = await _periodService.UpdatePeriodAsync(dto);

                if (!result)
                {
                    _errorMessage = "Perioden kunne ikke opdateres. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under opdateringen af perioden.";
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
