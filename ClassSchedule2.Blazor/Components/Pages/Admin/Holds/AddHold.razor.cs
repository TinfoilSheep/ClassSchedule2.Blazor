using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.Holds;
using ClassSchedule2.Blazor.Models.Forms.NonTeachingDays;
using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Holds
{
    public partial class AddHold
    {
        [Inject]
        private IHoldService _holdService { get; set; } = default!;
        [Inject]
        private ITermService _termService { get; set; } = default!;
        [Inject]
        private ISubjectService _subjectService { get; set; } = default!;

        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private CreateHoldFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;
        private bool _isLoading;

        private List<TermDTO> _terms = [];
        private List<SubjectDTO> _subjects = [];

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await LoadPeriodsAsync();

            StateHasChanged();
        }

        private async Task LoadPeriodsAsync()
        {
            _isLoading = true;

            try
            {
                _terms = await _termService.GetAllTermsAsync();
                _subjects = await _subjectService.GetAllSubjectsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HoldCard fejl: {ex}");
                _terms = [];
                _subjects = [];
            }
            finally
            {
                _isLoading = false;
            }
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
                var dto = new CreateHoldDTO(_form.Name, _form.TermId, _form.SubjectId);
                var result = await _holdService.Create(dto);

                if (result is null)
                {
                    _errorMessage = "Hold kunne ikke oprettes. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under oprettelsen af Hold.";
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
