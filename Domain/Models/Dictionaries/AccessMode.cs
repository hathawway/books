using FileExchanger.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace FileExchanger.Domain.Models.Dictionaries
{
    enum AccessMode
    {
        /// <summary>
        /// Ограниченный доступ ( для просмотра доступны первые 
        /// 3 страницы или 30% публикации)
        /// </summary>
        LimitedAccess,

        /// <summary>
        /// Доступ на просмотр без ограничений
        /// </summary>
        UnlimitedAccess
    }
}
