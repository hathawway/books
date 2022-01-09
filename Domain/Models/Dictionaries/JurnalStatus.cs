using FileExchanger.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace FileExchanger.Domain.Models.Dictionaries
{
    enum JurnalStatus 
    {
        BAK,
        ERIH,
        WOS,
        SCOPUS,
        RINC,
        /// <summary>
        /// Переводной
        /// </summary>
        Translated,
        /// <summary>
        /// Зарубежный журнал
        /// </summary>
        Foreign
    }
}
