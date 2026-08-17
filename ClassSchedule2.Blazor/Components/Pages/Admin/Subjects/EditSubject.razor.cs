using ClassSchedule2.Blazor.Models.Forms.Subjects;
using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Subjects
{
    public partial class EditSubject
    {
        [Inject]
        private SubjectService SubjectService { get; set; } = default!;

        [Parameter]
        public SubjectDTO? Subject { get; set; }
        [Parameter]
        public EventCallback OnSaved { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }

        private EditSubjectFormModel _form = new();
        private bool _isSubmitting;
        private string? _errorMessage;


        protected override void OnParametersSet()
        {
            if (Subject is null)
            {
                return;
            }

            _form.Id = Subject.Id;
            _form.Name = Subject.Name;

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
                var dto = new SubjectDTO(Id: _form.Id, Name: _form.Name);
                var result = await SubjectService.UpdateSubjectAsync(dto);

                if (result is null)
                {
                    _errorMessage = "Faget kunne ikke opdateres. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception)
            {
                _errorMessage = "Der opstod en fejl under opdateringen af faget.";
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
