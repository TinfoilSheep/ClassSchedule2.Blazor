using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.Terms;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Terms
{
    public partial class EditTerm
    {
        [Inject]
        private ITermService _termService { get; set; } = default!;

        [Parameter]
        public TermDTO? Term { get; set; }
        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private EditTermFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;


        protected override void OnParametersSet()
        {
            if (Term is null)
            {
                return;
            }

            _form.Id = Term.Id;
            _form.Name = Term.Name;
            _form.StartDate = Term.StartDate;
            _form.EndDate = Term.EndDate;

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
                var dto = new TermDTO(Id: _form.Id, Name: _form.Name, StartDate: _form.StartDate, EndDate: _form.EndDate);
                var result = await _termService.UpdateTermAsync(dto);

                if (result is null)
                {
                    _errorMessage = "Terminen kunne ikke opdateres. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under opdateringen af terminen.";
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
