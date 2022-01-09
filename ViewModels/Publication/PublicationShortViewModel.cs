using FileExchanger.Domain.Models.Dictionaries;
using FileExchanger.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExchanger.ViewModels.Publication
{
    /// <summary>
    /// Краткая информация по публикации
    /// </summary>
    public class PublicationShortViewModel : Response
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название ресурса(название)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Автор(ФИО)
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Место издания(если есть в БД)
        /// </summary>
        public string PublicationInfo { get; set; }

        /// <summary>
        /// Дата добавления на сайт
        /// </summary>
        public DateTime AddedTime { get; set; }

        /// <summary>
        /// Дата публикации
        /// </summary>
        public DateTime PublicationYear { get; set; }

        /// <summary>
        /// Ссылка
        /// </summary>
        public string ResourseLink { get; set; }
        
        /// <summary>
        /// Добавлено пользователю в корзину
        /// </summary>
        public bool IsInCurrentUserCart { get; set; }

        /// <summary>
        /// Добавлено пользователю в владение
        /// </summary>
        public bool IsInCurrentUserAccured { get; set; }

        /// <summary>
        /// Ключевые слова
        /// </summary>
        public List<string> KeyWords { get; set; }

        /// <summary>
        /// Тематика
        /// </summary>
        public Thematic Thematic { get; set; }
    }
}
