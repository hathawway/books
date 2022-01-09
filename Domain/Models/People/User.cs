using FileExchanger.Domain.Models.Content;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FileExchanger.Domain.Models.People
{
    public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// Ссылка на таблицу с информацией о данном пользователе
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Корзина
        /// </summary>
        public IEnumerable<Publication> Cart;

        /// <summary>
        /// Добавленное из корзины
        /// </summary>
        public ICollection<Publication> Acquired;
    }
}
