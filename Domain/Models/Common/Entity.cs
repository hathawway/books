using System.ComponentModel.DataAnnotations;

namespace FileExchanger.Domain.Models.Common
{
    /// <summary>
    /// Базовый класс для большинства стандартных сущностей таблицы
    /// </summary>
    public abstract class Entity
    {
        [Key]
        public virtual long Id { get; set; }
    }
}
