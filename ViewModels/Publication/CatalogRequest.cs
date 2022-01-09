using FileExchanger.ViewModels.Common;
using System;

namespace FileExchanger.ViewModels.Publication
{
    public class CatalogRequest : Request
    {
        /// <summary>
        /// Поисковик/Фильтрация
        /// </summary>
        public CatalogFilter Filter { get; set; }
    }

    /// <summary>
    /// Фильт для поиска
    /// </summary>
    public class CatalogFilter
    {
        /// <summary>
        /// Поисковая строка
        /// </summary>
        public string Line { get; set; }
        
    }

}
