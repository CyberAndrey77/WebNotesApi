using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace WebNotesApi.Models.NoteModels
{
    /// <summary>
    /// Класс заметка.
    /// </summary>
    public class Note
    {
        /// <summary>
        /// Id заметки.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название заметки.
        /// </summary>
        public string NoteName { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public string CreatedDate { get; set; }

        /// <summary>
        /// Дата последнего изменения.
        /// </summary>
        public string UpdatedDate { get; set; }

        /// <summary>
        /// Текст заметки.
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Id категории.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Указатель на класс Category.
        /// </summary>
        [JsonIgnore]
        public NoteCategory Category { get; set; }

        /// <summary>
        /// id пользователя
        /// </summary>
        public int UserId { get; set; }
    }
}
