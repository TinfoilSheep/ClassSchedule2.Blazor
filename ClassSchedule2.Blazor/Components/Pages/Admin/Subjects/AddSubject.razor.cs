using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.Subjects;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Subjects
{
    public partial class AddSubject
    {
        [Inject]
        private ISubjectService _subjectService { get; set; } = default!;
        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private CreateSubjectFormModel _form = new();
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
                var dto = new CreateSubjectDTO(Name: _form.Name);
                var result = await _subjectService.CreateSubjectAsync(dto);

                if (!result)
                {
                    _errorMessage = "Faget kunne ikke oprettes. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under oprettelsen af faget.";
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
