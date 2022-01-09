using FileExchanger.Domain.Models.Common;
using FileExchanger.Domain.Models.Dictionaries;
using FileExchanger.Domain.Models.Dictionaries.Codes;
using FileExchanger.Domain.Models.People;
using System;
using System.Collections.Generic;

namespace FileExchanger.Domain.Models.Content
{
    /// <summary>
    /// Публикация
    /// </summary>
    public class Publication : Entity
    {
        /// <summary>
        /// Название публикации
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Автор 
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Соавторы
        /// </summary>
        public string OtherAuthors { get; set; }

        /// <summary>
        /// Человек, который добавил публикацию
        /// </summary>
        public User Owner { get; set; }

        /// <summary>
        /// Правообладатель 
        /// </summary>
        public string RightHolder { get; set; }

        /// <summary>
        /// Тематика 
        /// </summary>
        public Thematic Thematics { get; set; }

        /// <summary>
        /// УДК - Универсальная десятичная классификация
        /// </summary>
        public UDC UDC { get; set; }

        /// <summary>
        /// ББК - Библиотечно-библиографическая классификация
        /// </summary>
        public LBC LBC { get; set; }

        /// <summary>
        /// Язык публикации
        /// </summary>
        public Language PublicationLanguage { get; set; }

        /// <summary>
        /// Тип публикации
        /// </summary>
        public PublicationInfo PublicationInfo { get; set; }

        /// <summary>
        /// Дата добавления на сайт
        /// </summary>
        public DateTime AddedDated { get; set; }

        /// <summary>
        /// Пользователи у которыз в корзине
        /// </summary>
        public ICollection<User> UsersCart { get; set; }

        /// <summary>
        /// Пользователи которые приобрели
        /// </summary>
        public ICollection<User> UsersAcquired { get; set; }

        /// <summary>
        /// Ключевые слова для поиска
        /// </summary>
        public List<string> KeyWords() 
        {
            List<string> keyWords = new List<string>();
            
            keyWords.AddRange(Name.Split(" "));
            keyWords.AddRange(Author.Split(" "));

            if (PublicationInfo.AdditionalInfo != null)
            {
                keyWords.AddRange(PublicationInfo.AdditionalInfo.Split(" "));
            }

            if (PublicationInfo.PublicationTypeName != null)
            {
                keyWords.AddRange(PublicationInfo.PublicationTypeName.Name.Split(" "));
            }
            
            if (PublicationInfo.PublishedBy != null)
            {
                keyWords.AddRange(PublicationInfo.PublishedBy.Split(" "));
            }
            
            return keyWords;
        }
    }
}
