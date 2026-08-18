using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.TermLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.Terms
{
    public partial class DeleteTerm
    {
        [Inject]
        private ITermService _termService { get; set; } = default!;

        [Parameter]
        public TermDTO? Term { get; set; }

        [Parameter]
        public EventCallback OnDeleted { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private bool _isDeleting;

        private async Task DeleteAsync()
        {
            if (_isDeleting || Term is null)
            {
                return;
            }

            _isDeleting = true;

            try
            {
                var success = await _termService.DeleteTermAsync(Term.Id);

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
