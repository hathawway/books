using FileExchanger.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace FileExchanger.Domain.Models.Dictionaries.Codes
{
    /// <summary>
    /// УДК - Универсальная десятичная классификация
    /// </summary>
    public class UDC : Entity
    {
        /// <summary>
        /// Ссылка на группу
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// Именование
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Кодировка
        /// </summary>
        public string Code
        {
            get => ParentId.ToString() + Id.ToString();
        }
    }
}
