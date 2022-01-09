using FileExchanger.Domain.Models.Dictionaries;
using System.Collections.Generic;

namespace FileExchanger.Service.BasicData
{
    public interface IDictionaire
    {
        /// <summary>
        /// Языки
        /// </summary>
        /// <returns></returns>
        IEnumerable<Language> GetLanguages();

        /// <summary>
        /// Тематики
        /// </summary>
        /// <returns></returns>
        IEnumerable<Thematic> GetThematics();

        /// <summary>
        /// Типы публикации
        /// </summary>
        /// <returns></returns>
        IEnumerable<PublicationTypeName> GetPublicationTypeNames();
    }
}
