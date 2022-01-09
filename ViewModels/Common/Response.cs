using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExchanger.ViewModels.Common
{
    public class Response
    {
        /// <summary>
        /// Общее число записей по такому запросу
        /// </summary>
        public long Amount { get; set; }
    }
}
