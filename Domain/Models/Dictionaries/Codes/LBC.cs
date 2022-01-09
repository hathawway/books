using FileExchanger.Domain.Models.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FileExchanger.Domain.Models.Dictionaries.Codes
{
    /// <summary>
    /// ББК - Библиотечно-библиографическая классификация
    /// </summary>
    public class LBC : Entity
    {
        /// <summary>
        /// Id Группы
        /// </summary>
        [ForeignKey("ParentId")]
        public long ParentId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [Required]
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
