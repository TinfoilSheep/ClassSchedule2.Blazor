using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.LessonTemplates;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.LessonTemplateLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.RoomLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.LessonTemplates
{
    public partial class EditLessonTemplate
    {
        [Inject]
        private ILessonTemplateService _lessonTemplateService { get; set; } = default!;

        [Inject]
        private IHoldService _holdService { get; set; } = default!;

        [Inject]
        private IPeriodService _periodService { get; set; } = default!;

        [Inject]
        private IRoomService _roomService { get; set; } = default!;

        [Parameter]
        public LessonTemplateDTO? LessonTemplate { get; set; }

        [Parameter]
        public EventCallback OnSaved { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        private EditLessonTemplateFormModel _form = new();

        private bool _isSubmitting;
        private bool _isLoading;
        private string? _errorMessage;

        private List<HoldDTO> _holds = [];
        private List<PeriodDTO> _periods = [];
        private List<RoomDTO> _rooms = [];

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await LoadDataAsync();

            StateHasChanged();
        }

        private async Task LoadDataAsync()
        {
            _isLoading = true;

            try
            {
                var holdsTask = _holdService.GetAll();
                var periodsTask = _periodService.GetAllPeriodsAsync();
                var roomsTask = _roomService.GetAllRoomsAsync();

                await Task.WhenAll(holdsTask, periodsTask, roomsTask);

                _holds = await holdsTask ?? [];
                _periods = await periodsTask ?? [];
                _rooms = await roomsTask ?? [];

                SetFormFromLessonTemplate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EditLessonTemplate fejl: {ex}");

                _holds = [];
                _periods = [];
                _rooms = [];

                _errorMessage = "Der opstod en fejl under indlæsning af data.";
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void SetFormFromLessonTemplate()
        {
            if (LessonTemplate is null)
            {
                return;
            }

            _form.Id = LessonTemplate.Id;
            _form.WeekDay = LessonTemplate.WeekDay;
            _form.ValidFrom = LessonTemplate.ValidFrom;
            _form.ValidTo = LessonTemplate.ValidTo;

            _form.HoldId = _holds.FirstOrDefault(x => x.Name == LessonTemplate.HoldName)?.Id;

            _form.PeriodId = _periods.FirstOrDefault(x => x.Name == LessonTemplate.PeriodName)?.Id;

            if (!string.IsNullOrWhiteSpace(LessonTemplate.RoomName))
            {
                _form.RoomId = _rooms.FirstOrDefault(x => x.Name == LessonTemplate.RoomName)?.Id;
            }
            else
            {
                _form.RoomId = null;
            }
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
                if (_form.HoldId is null || _form.PeriodId is null)
                {
                    _errorMessage = "Hold og periode skal vælges.";

                    return;
                }

                var dto = new UpdateLessonTemplateDTO(
                    _form.Id,
                    _form.WeekDay,
                    _form.ValidFrom,
                    _form.ValidTo,
                    _form.HoldId.Value,
                    _form.PeriodId.Value,
                    _form.RoomId);

                var result = await _lessonTemplateService.UpdateAsync(dto);

                if (result is null)
                {
                    _errorMessage = "Lektionsplanen kunne ikke opdateres. Prøv igen.";
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EditLessonTemplate fejl ved gemning: {ex}");

                _errorMessage = "Der opstod en fejl under opdateringen af lektionsplanen.";
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
