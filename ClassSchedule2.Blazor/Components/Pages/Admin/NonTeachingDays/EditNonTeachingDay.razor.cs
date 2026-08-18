using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.NonTeachingDays;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.NonTeachingDays
{
    public partial class EditNonTeachingDay
    {
        [Inject]
        private INonTeachingDayService _nonTeachingDayService { get; set; } = default!;

        [Parameter]
        public NonTeachingDayDTO? NonTeachingDay { get; set; }
        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private EditNonTeachingDayFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;


        protected override void OnParametersSet()
        {
            if (NonTeachingDay is null)
            {
                return;
            }

            _form.Id = NonTeachingDay!.Id;
            _form.Reason = NonTeachingDay!.Reason;
            _form.StartDate = NonTeachingDay!.StartDate;
            _form.EndDate = NonTeachingDay!.EndDate;

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
                var dto = new NonTeachingDayDTO(Id: _form.Id, Reason: _form.Reason, StartDate: _form.StartDate, EndDate: _form.EndDate);
                var result = await _nonTeachingDayService.UpdateNonTeachingDayAsync(dto);

                if (result is null)
                {
                    _errorMessage = "Fridagen kunne ikke opdateres. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under opdateringen af fridagen.";
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
