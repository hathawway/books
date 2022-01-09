using FileExchanger.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace FileExchanger.Domain.Models.Dictionaries
{
    public class Language : Entity
    {
        /// <summary>
        /// Название 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Алиас
        /// </summary>
        public string Alias { get; set; }

    }
}
