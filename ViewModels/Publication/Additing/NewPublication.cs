using System;
using System.ComponentModel.DataAnnotations;

namespace FileExchanger.ViewModels.Publication.Additing
{
    public class NewPublication
    {
        /// <summary>
        /// Название публикации
        /// </summary>
        [Required(ErrorMessage = "Не указано название")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        /// <summary>
        /// Автор(ы) 
        /// </summary>
        [Required(ErrorMessage = "Не указан автор")]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        /// <summary>
        /// Соавторы
        /// </summary>
        [Required(ErrorMessage = "Не указаны соавторы")]
        [Display(Name = "Соавторы")]
        public string OtherAuthors { get; set; }

        /// <summary>
        /// Год публикации
        /// </summary>
        public string PublishingYear { get; set; }

        /// <summary>
        /// Издательство
        /// </summary>
        [Required(ErrorMessage = "Не указано издательство")]
        [Display(Name = "Издательство")]
        public string PublishedBy { get; set; }

        /// <summary>
        /// Тематика 
        /// </summary>
        public long Thematics { get; set; }

        /// <summary>
        /// Язык публикации
        /// </summary>
        [Required(ErrorMessage = "Не указан язык публикации")]
        [Display(Name = "Язык публикации")]
        public long PublicationLanguage { get; set; }

        /// <summary>
        /// Название типа публикации
        /// </summary>
        [Required(ErrorMessage = "Не указан тип публикации")]
        [Display(Name = "Тип публикации")]
        public long PublicationTypeName { get; set; }

        /// <summary>
        /// Ссылка на публикацию
        /// </summary>
        [Required(ErrorMessage = "Не указана ссылка на публикацию")]
        [Display(Name = "Ссылка")]
        public string ResourseLink { get; set; }

        /// <summary>
        /// Количество страниц
        /// </summary>
        [Required(ErrorMessage = "Не указано кол-во стр")]
        [Display(Name = "Кол-во стр")]
        public long PagesAmount { get; set; }

    }
}
