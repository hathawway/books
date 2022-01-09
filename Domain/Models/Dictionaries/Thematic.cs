using FileExchanger.Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace FileExchanger.Domain.Models.Dictionaries
{
    /// <summary>
    /// Тема. Пока без конкретных значений. Только тестовые
    /// </summary>
    public class Thematic : Entity
    {
        /// <summary>
        /// Название тематики
        /// </summary>
        public string Name { get; set; }
    }
}
