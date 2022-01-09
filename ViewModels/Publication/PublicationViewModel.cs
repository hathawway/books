using FileExchanger.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExchanger.ViewModels.Publication
{
    public class PublicationViewModel : Response
    {
        /// <summary>
        /// Название публикации
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Автор(ы) 
        /// </summary>
        public string Authors { get; set; }

        /// <summary>
        /// Год публикации
        /// </summary>
        public DateTime PublishingYear { get; set; }

        /// <summary>
        /// Тематика 
        /// </summary>
        private string Thematics { get; set; }

        /// <summary>
        /// Язык публикации
        /// </summary>
        public string PublicationLanguage { get; set; }

        /// <summary>
        /// Ссылка на содержимое (или группу ресурсов) публикации
        /// </summary>
        public string ResourceLink { get; set; }

        /// <summary>
        /// Тип публикации
        /// </summary>
        public string PublicationType { get; set; }
    }
}
