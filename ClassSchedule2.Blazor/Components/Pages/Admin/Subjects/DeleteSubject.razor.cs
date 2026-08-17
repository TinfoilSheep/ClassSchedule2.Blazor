using ClassSchedule2.Blazor.Services.Data;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.SubjectLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Subjects
{
    public partial class DeleteSubject
    {
        [Inject]
        private SubjectService SubjectService { get; set; } = default!;

        [Parameter]
        public SubjectDTO? Subject { get; set; }

        [Parameter]
        public EventCallback OnDeleted { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private bool _isDeleting;

        private async Task DeleteAsync()
        {
            if (_isDeleting || Subject is null)
            {
                return;
            }

            _isDeleting = true;

            try
            {
                var success = await SubjectService.DeleteSubjectAsync(Subject.Id);

                if (success)
                {
                    await OnDeleted.InvokeAsync();
                }
            }
            finally
            {
                _isDeleting = false;
            }
        }

        private async Task Cancel()
        {
            if (_isDeleting)
            {
                return;
            }

            await OnCancel.InvokeAsync();
        }
    }
}
