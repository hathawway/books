using System.ComponentModel.DataAnnotations;

namespace FileExchanger.ViewModels.User
{
    /// <summary>
    /// Модель для авторизации пользователя
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required(ErrorMessage = "Не указан логин")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Запомнить ли учетную запись в браузере
        /// </summary>
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
    }
}
