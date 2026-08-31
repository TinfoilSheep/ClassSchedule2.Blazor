using ClassSchedule2.Blazor.Interfaces;
using Microsoft.AspNetCore.Components;
using static ClassSchedule2.Blazor.Models.DTOs.LessonLibrary;
using static ClassSchedule2.Blazor.Models.DTOs.LessonNoteLibrary;

namespace ClassSchedule2.Blazor.Components.Pages.Schedule
{
    public partial class SetLessonNote
    {
        [Inject] private ILessonNoteService NoteService { get; set; } = default!;

        [Parameter, EditorRequired]
        public LessonDTO Lesson { get; set; } = default!;

        [Parameter] public EventCallback OnCancel { get; set; }

        [Parameter] public EventCallback OnSaved { get; set; }

        private string _content = string.Empty;
        private bool _isSubmitting;

        private string _submitButtonText
        {
            get
            {
                if (_isSubmitting)
                    return "Gemmer...";

                bool hasExistingNote = Lesson?.Note != null;
                bool hasText = !string.IsNullOrWhiteSpace(_content);

                if (hasExistingNote && hasText)
                {
                    return "Gem note";
                }
                if (hasExistingNote && !hasText)
                {
                    return "Slet note";
                }

                return "Opret note";
            }
        }

        protected override void OnInitialized()
        {
            if (Lesson.Note != null && !string.IsNullOrEmpty(Lesson.Note.Content)) _content = Lesson.Note.Content;
        }

        private async Task HandleSubmitAsync()
        {
            _isSubmitting = true;
            try
            {
                bool result = false;
                if (Lesson.Note == null && string.IsNullOrWhiteSpace(_content))
                {
                    await OnCancel.InvokeAsync();
                }
                else if (Lesson.Note == null && !string.IsNullOrWhiteSpace(_content))
                {
                    CreateLessonNoteDTO dto = new(Lesson.Id, _content);
                    result = await NoteService.AddNote(dto);
                }
                else if (Lesson.Note != null && !string.IsNullOrWhiteSpace(_content))
                {
                    UpdateLessonNoteDTO dto = new(Lesson.Note.Id, _content);
                    result = await NoteService.UpdateNote(dto);
                }
                else if (Lesson.Note != null && string.IsNullOrWhiteSpace(_content))
                {
                    await NoteService.DeleteNote(Lesson.Note.Id);
                    await OnSaved.InvokeAsync();
                    return;
                }

                if (!result)
                {
                    return;
                }

                await OnSaved.InvokeAsync();
            }
            catch
            {

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
