using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FileExchanger.Domain.Models;
using FileExchanger.Security;
using FileExchanger.Domain.DB;
using FileExchanger.Domain.Models.People;
using FileExchanger.Service.Publications;
using FileExchanger.ViewModels.Publication.Additing;
using FileExchanger.Domain.Models.Dictionaries;

namespace FileExchanger.Infrastructure.Guarantors
{
    /// <summary>
    /// Заполнение данных для DbContext
    /// </summary>
    public class SeedDataGuarantor
    {
        private readonly IServiceProvider _serviceProvider;
        public SeedDataGuarantor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Проверка данных
        /// </summary>
        public async Task EnsureAsync()
        {
            var context = _serviceProvider.GetService<FileExchangerDbContext>();
            var userManager = _serviceProvider.GetService<UserManager<User>>();
            var roleManager = _serviceProvider.GetService<RoleManager<IdentityRole<Guid>>>();

            await AssertRoleExistenceAsync(roleManager, context);

            context.SaveChanges();

            var adminUser = userManager.FindByNameAsync(SecurityConstants.AdminUserName).Result;
            if (adminUser == null)
            {
                adminUser = new User
                {
                    Email = SecurityConstants.AdminEmail,
                    UserName = SecurityConstants.AdminUserName,
                    Employee = new Employee
                               {
                                   FirstName = SecurityConstants.AdminFirstName,
                                   LastName = SecurityConstants.AdminSurName
                               },
                };
                await userManager.CreateAsync(adminUser, SecurityConstants.AdminPassword);
            }
            
            if (!userManager.IsInRoleAsync(adminUser, SecurityConstants.AdminRole).Result)
                userManager.AddToRoleAsync(adminUser, SecurityConstants.AdminRole).Wait();

            var user = userManager.FindByNameAsync("mail@mail.ru").Result;
            if (user == null)
            {
                user = new User
                {
                    Email = "mail@mail.ru",
                    UserName = "mail@mail.ru",
                    Employee = new Employee
                    {
                        FirstName = "Буков",
                        LastName = "Александр"
                    },
                };
                await userManager.CreateAsync(user, "111111");

                context.PublicationTypeNames.AddRange(new List<PublicationTypeName>()
                {
                    new PublicationTypeName()
                    {
                       Id = 1,
                       Name = "Книга"
                    },
                   new PublicationTypeName()
                    {
                        Id = 2,
                        Name = "Журнал"
                    },
                    new PublicationTypeName()
                    {
                        Id = 3,
                        Name = "Публикация"
                    },
                    new PublicationTypeName()
                    {
                        Id = 4,
                        Name = "Методическое пособие"
                    },
                });

                context.Thematics.AddRange(new List<Thematic>()
                {
                  new Thematic()
                  {
                      Name = "Наука и техника",
                  },
                  new Thematic()
                  {
                      Name = "Спорт",
                  },
                  new Thematic()
                  {
                      Name = "Художественная литература",
                  },
                  new Thematic()
                  {
                      Name = "Другое",
                  },
                });

                var dataLoader = new DataLoader();
                var publications = dataLoader.GeneratePublications(5);
                var pubService = _serviceProvider.GetService<IPublicable>();
                foreach (NewPublication nPub in publications)
                {
                    pubService.AddPublication(nPub, user.Id);
                }

                context.Languages.AddRange(new List<Language>() 
                { 
                    new Language()
                    {
                        Id = 1,
                        Alias = "ru_RU",
                        Name = "Русский",
                    },
                    new Language()
                    {
                        Id = 2,
                        Alias = "en_EN",
                        Name = "Английский",
                    },

                });
                context.SaveChanges();
            }
            
            context.SaveChanges();
        }
        private static async Task AssertRoleExistenceAsync(RoleManager<IdentityRole<Guid>> roleManager, FileExchangerDbContext context)
        {
            var roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid> { Name = SecurityConstants.AdminRole, NormalizedName = "ADMIN" },
                new IdentityRole<Guid> { Name = SecurityConstants.СustomerRole, NormalizedName = "CUSTOMER" }
            };

            foreach (var role in roles)
            {
                var roleExit = await roleManager.RoleExistsAsync(role.Name);
                if (!roleExit)
                {
                    context.Roles.Add(role);
                    context.SaveChanges();
                }
            }

        }

    }
}
