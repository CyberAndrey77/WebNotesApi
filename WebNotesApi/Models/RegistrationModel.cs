using System.ComponentModel.DataAnnotations;

namespace WebNotesApi.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Введите эл. почту")]
        [EmailAddress(ErrorMessage = "Введите коректную эл. почту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите подтверждающий пароль")]
        public string RepeatedPassword { get; set; }
    }
}
