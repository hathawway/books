using System.ComponentModel.DataAnnotations;

namespace FileExchanger.ViewModels.User
{
    public class NewUserViewModel
    {
        /// <summary>
        /// Почта
        /// </summary>
        [Required(ErrorMessage = "Не указан email")]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }
        /// <summary>
        /// Логин
        /// </summary>
        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя пользователя")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Повторение пароля
        /// </summary>
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
    }
}
