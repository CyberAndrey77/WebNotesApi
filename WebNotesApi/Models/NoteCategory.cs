using System.ComponentModel.DataAnnotations;

namespace WebNotesApi.Models
{
    /// <summary>
    /// Категоря заметок.
    /// </summary>
    public class NoteCategory
    {
        /// <summary>
        /// Id категории заметки.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Название категории.
        /// </summary>
        public string CategoryName { get; set; }
    }
}
