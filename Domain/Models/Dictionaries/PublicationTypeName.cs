using FileExchanger.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExchanger.Domain.Models.Dictionaries
{
    public class PublicationTypeName : Entity
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name{ get; set; }
    }
}
