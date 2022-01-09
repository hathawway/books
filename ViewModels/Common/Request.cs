using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExchanger.ViewModels.Common
{
    public class Request
    {
        /// <summary>
        /// Пагинация
        /// </summary>
        public Page Page { get; set; }
        
        /// <summary>
        /// Сортировка
        /// </summary>
        public Sort Sort { get; set; }

        /// <summary>
        /// Запрос который пользователь ввёл в поисковике
        /// </summary>
        public string RequestLint { get; set; }
    }
    
    public class Page
    {
        /// <summary>
        /// Число записей, которое запрашивается
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Смещение записей
        /// </summary>
        public int OffSet { get; set; }
    }

    public class Sort
    {
        /// <summary>
        /// Поле для сортировки
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// По возрастанию
        /// </summary>
        public bool Acs { get; set; }
    }
}
