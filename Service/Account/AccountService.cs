using FileExchanger.Domain.DB;
using FileExchanger.Domain.Models.People;
using FileExchanger.ViewModels.User.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace FileExchanger.Service.Account
{
    public class AccountService : IAccounting
    {

        private readonly FileExchangerDbContext _fileExchangerDbContext;
        public AccountService(FileExchangerDbContext fileExchangerDbContext)
        {
            _fileExchangerDbContext = fileExchangerDbContext ?? throw new ArgumentNullException(nameof(fileExchangerDbContext));
        }

        public AccountViewModel GetAccountInfo(Guid userId)
        {
            var userInfo = _fileExchangerDbContext.Users
                .Where(x => x.Id == userId)
                .Select(x => new AccountViewModel()
            {
                AditionalInfo = x.Employee.AditionalInfo ?? " ",
                FirstName = x.Employee.FirstName ?? " ",
                LastName = x.Employee.LastName ?? " ",
                MiddleName = x.Employee.MiddleName ?? " ",
            }).First();

            return userInfo;
        }

        public void UpdateAccountInfo(AccountViewModel userInfoChanges, Guid userId)
        {
            var user = _fileExchangerDbContext.Users
                .Include(x => x.Employee)
                .FirstOrDefault(x => x.Id == userId);
            user.Employee = new Employee()
            {
                FirstName = userInfoChanges.FirstName,
                AditionalInfo = userInfoChanges.AditionalInfo,
                LastName = userInfoChanges.LastName,
                MiddleName = userInfoChanges.MiddleName,
            };
            _fileExchangerDbContext.SaveChanges();
        }
    }
}
