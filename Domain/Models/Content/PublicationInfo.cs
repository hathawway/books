using FileExchanger.Domain.Models.Common;
using FileExchanger.Domain.Models.Dictionaries;
using System;

namespace FileExchanger.Domain.Models.Content
{
    public class PublicationInfo : Entity
    {
        /// <summary>
        /// Название типа публикации
        /// </summary>
        public PublicationTypeName PublicationTypeName { get; set; }
        
        /// <summary>
        /// Ссылка на публикацию
        /// </summary>
        public string ResourseLink { get; set; }

        /// <summary>
        /// Издательство
        /// </summary>
        public string PublishedBy { get; set; }

        /// <summary>
        /// Дата издания
        /// </summary>
        public DateTime PublishingYear { get; set; }

        /// <summary>
        /// Количество страниц / номер страницы 
        /// с которой начинается статья в журнале
        /// </summary>
        public long Pages { get; set; }
        /// <summary>
        /// Иллюстрации
        /// </summary>
        public bool HasImages { get; set; }
        /// <summary>
        /// ISBN/ISSN код
        /// </summary>
        public string ISCode { get; set; }
        
        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public string AdditionalInfo { get; set; }

        public string PublishedPlaceAndTime()
        {
            return PublishedBy + " " + PublishingYear.ToString();
        }
    }
}
