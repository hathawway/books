using FileExchanger.Domain.Models.Dictionaries;
using FileExchanger.Domain.Models.People;
using FileExchanger.ViewModels.Common;
using FileExchanger.ViewModels.Publication;
using FileExchanger.ViewModels.Publication.Additing;
using System;
using System.Collections.Generic;

namespace FileExchanger.Service.Publications
{
    public interface IPublicable
    {
        /// <summary>
        /// Добавить публикацию в базу данных
        /// </summary>
        /// <param name="resource">данные о публикации</param>
        /// <param name="user">пользователь</param>
        void AddPublication(NewPublication resource, Guid userId);

        /// <summary>
        /// Публикации добавленные пользователем
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        IEnumerable<PublicationShortViewModel> MyPosted(Guid userId);

        /// <summary>
        /// Каталог всех публикаций
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IEnumerable<PublicationShortViewModel> Catalog(Request request, string querry, User user, long thematics);

        /// <summary>
        /// Публикации которые добавил пользователь из корзины
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        IEnumerable<PublicationShortViewModel> Acquired(Guid userId);

        /// <summary>
        /// Добавление публикации пользователю из корзины в добавленное
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Id"></param>
        void AddToAcquired(Guid userId, long Id);

        /// <summary>
        /// Удаление из купленного
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Id"></param>
        void RemoveFromAcquired(Guid userId, long Id);

        /// <summary>
        /// Избранное 
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        IEnumerable<PublicationShortViewModel> Cart(Guid userId);

        /// <summary>
        /// Добавление публикации в корзину пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Id"></param>
        void AddToCart(Guid userId, long Id);

        /// <summary>
        /// Удаляет из корзины пользователя с Guid = userId публикацию с Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Id"></param>
        void RemoveFromCart(Guid userId, long Id);


        /// <summary>
        /// Возвращает полную информация по публикации
        /// </summary>
        /// <param name="publicationId"></param>
        /// <returns></returns>
        PublicationViewModel GetPublication(long publicationId);
    }
}
