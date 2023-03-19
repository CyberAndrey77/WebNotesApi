namespace WebNotesApi.Models.NoteModels
{
    public class NoteModel
    {
        /// <summary>
        /// Название заметки.
        /// </summary>
        public string NoteName { get; set; }

        /// <summary>
        /// Текст заметки.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Id категории.
        /// </summary>
        public int CategoryId { get; set; }
    }
}
