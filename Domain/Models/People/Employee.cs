using FileExchanger.Domain.Models.Common;
using FileExchanger.Domain.Models.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FileExchanger.Domain.Models.People
{
    /// <summary>
    /// Информация о пользователе
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        public virtual Guid Guid { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        public string LastName { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string FirstName { get; set; }
       
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Дополнительная информация
        /// </summary>
        public string AditionalInfo { get; set; }

        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
