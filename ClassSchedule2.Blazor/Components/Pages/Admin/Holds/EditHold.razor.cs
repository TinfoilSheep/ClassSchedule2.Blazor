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
    public partial class EditHold
    {
        [Inject]
        private IHoldService _holdService { get; set; } = default!;
        [Inject]
        private ISubjectService _subjectService { get; set; } = default!;
        [Inject]
        private ITermService _termService { get; set; } = default!;

        [Parameter]
        public HoldDTO? UpdateHold { get; set; }

        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private EditHoldFormModel _form = new();
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

        protected override void OnParametersSet()
        {
            if (UpdateHold is null)
            {
                return;
            }

            _form.Id = UpdateHold!.Id;
            _form.Name = UpdateHold!.Name;

            _form.TermId = UpdateHold!.TermId;
            // Prefer the DTO name directly, fallback to list lookup if missing
            _form.TermName = UpdateHold.TermName
                ?? _terms.FirstOrDefault(s => s.Id == _form.TermId)?.Name
                ?? "";

            _form.SubjectId = UpdateHold!.SubjectId;
            _form.SubjectName = UpdateHold.SubjectName
                ?? _subjects.FirstOrDefault(s => s.Id == _form.SubjectId)?.Name
                ?? "";



            _errorMessage = null;
        }


        private async Task HandleSubmitAsync()
        {
            if (_isSubmitting || _isLoading)
            {
                return;
            }

            _isSubmitting = true;
            _errorMessage = null;

            try
            {
                var dto = new HoldDTO(_form.Id, _form.Name, _form.SubjectId, _form.TermId, _form.SubjectName, _form.TermName, _form.Teachers, _form.Students);
                var result = await _holdService.Update(dto);

                if (!result)
                {
                    _errorMessage = "Hold kunne ikke opdateres. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under opdateringen af Hold.";
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
