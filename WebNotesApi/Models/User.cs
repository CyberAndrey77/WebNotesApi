using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebNotesApi.Models
{
    public class User
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email полльзователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public byte[] Password { get; set; }

        /// <summary>
        /// Дата регистрации.
        /// </summary>
        public string CreationTime { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Список заметок пользователя.
        /// </summary>
        [JsonIgnore]
        public List<Note>? Notes { get; set; } = new List<Note>();
    }
}
