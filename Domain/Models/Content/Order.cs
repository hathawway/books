using FileExchanger.Domain.Models.Common;
using FileExchanger.Domain.Models.People;
using System;

namespace FileExchanger.Domain.Models.Content
{
    /// <summary>
    /// Заказ на приобретение публикации
    /// </summary>
    public class Order : Entity
    {
        /// <summary>
        /// Дата приобретения
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Итоговая сумма (пока 0)
        /// </summary>
        public double Summary { get; set; }

        /// <summary>
        /// Аккаунт приобретающего публикацию
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// Сама публикация
        /// </summary>
        public Publication Publication { get; set; }
    }
}
