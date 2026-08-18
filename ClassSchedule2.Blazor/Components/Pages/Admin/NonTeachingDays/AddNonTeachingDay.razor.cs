using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.NonTeachingDays;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.NonTeachingDays
{
    public partial class AddNonTeachingDay
    {
        [Inject]
        private INonTeachingDayService _nonTeachingDayService { get; set; } = default!;
        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private CreateNonTeachingDayFormModel _form = new();
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
                var dto = new CreateNonTeachingDayDTO(Reason: _form.Reason, StartDate: _form.StartDate, EndDate: _form.EndDate);
                var result = await _nonTeachingDayService.CreateNonTeachingDayAsync(dto);

                if (result is null)
                {
                    _errorMessage = "Fridagen kunne ikke oprettes. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under oprettelsen af fridagen.";
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
