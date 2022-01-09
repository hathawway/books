using FileExchanger.ViewModels.User.Account;
using System;

namespace FileExchanger.Service.Account
{
    public interface IAccounting
    {
        /// <summary>
        /// Выдача информации по пользователю
        /// </summary>
        /// <returns></returns>
        AccountViewModel GetAccountInfo(Guid userId);

        /// <summary>
        /// Обновление информации пользователя
        /// </summary>
        /// <param name=""></param>
        void UpdateAccountInfo(AccountViewModel userInfoChanges, Guid userId);
    }
}
