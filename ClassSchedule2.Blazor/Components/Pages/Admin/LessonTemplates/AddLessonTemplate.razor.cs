using ClassSchedule2.Blazor.Interfaces;
using ClassSchedule2.Blazor.Models.Forms.LessonTemplates;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.HoldLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.LessonTemplateLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.PeriodLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.RoomLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Admin.LessonTemplates
{
    public partial class AddLessonTemplate
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
        public EventCallback OnSaved { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }


        private CreateLessonTemplateFormModel _form = new();

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
            _errorMessage = null;

            try
            {
                _holds = await _holdService.GetAll() ?? [];
                _periods = await _periodService.GetAllPeriodsAsync() ?? [];
                _rooms = await _roomService.GetAllRoomsAsync() ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddLessonTemplate fejl: {ex}");

                _holds = [];
                _periods = [];
                _rooms = [];

                _errorMessage = "Data kunne ikke indlæses. Prøv igen.";
            }
            finally
            {
                _isLoading = false;
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
                var dto = new CreateLessonTemplateDTO(
                    _form.WeekDay,
                    _form.ValidFrom,
                    _form.ValidTo,
                    _form.HoldId!.Value,
                    _form.PeriodId!.Value,
                    _form.RoomId);

                var result = await _lessonTemplateService.CreateAsync(dto);

                if (result is null)
                {
                    _errorMessage = "Lektionsplanen kunne ikke oprettes. Prøv igen.";

                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved oprettelse af lektionsplan: {ex}");

                _errorMessage = "Der opstod en fejl under oprettelsen af lektionsplanen.";
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
