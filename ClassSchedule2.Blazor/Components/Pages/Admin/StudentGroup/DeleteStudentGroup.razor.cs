using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.NonTeachingDayLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.StudentGroupLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.StudentGroup
{
    public partial class DeleteStudentGroup
    {
        [Inject]
        private IStudentGroupService _studentGroupService { get; set; } = default!;

        [Parameter]
        public StudentGroupDTO? StudentGroup { get; set; }

        [Parameter]
        public EventCallback OnDeleted { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private bool _isDeleting;

        private async Task DeleteAsync()
        {
            if (_isDeleting || StudentGroup is null)
            {
                return;
            }

            _isDeleting = true;

            try
            {
                var success = await _studentGroupService.Delete(StudentGroup.Id);

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
