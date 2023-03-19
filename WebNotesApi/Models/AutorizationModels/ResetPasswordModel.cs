using System.ComponentModel.DataAnnotations;

namespace WebNotesApi.Models.AutorizationModels
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Нет токена")]
        public string Token { get; set; }

        [Required, MinLength(6, ErrorMessage = "Пароль должен быть больше 6 символов")]
        public string Password { get; set; }

        [Required, Compare(nameof(Password), ErrorMessage = "Пароли должны совпадать")]
        public string RepetedPassword { get; set; }
    }
}
