using FileExchanger.Domain.Models.Dictionaries;
using FileExchanger.Domain.Models.Dictionaries.Codes;
using FileExchanger.ViewModels.Publication.Additing;
using FileExchanger.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileExchanger.Domain.DB
{
    public class DataLoader
    {
        public List<NewUserViewModel> GenerateUsers()
        {
            List<NewUserViewModel> users = new List<NewUserViewModel>();
            for (int i = 0; i < 2; i++)
            {
                users.Add(new NewUserViewModel()
                {
                    EmailAddress = i.ToString() + "@mail.ru",
                    FirstName = "A" + i,
                    LastName = "B" + i,
                    Password = i + 100000.ToString(),
                    ConfirmPassword = i + 100000.ToString(),
                });
            }
            return users;
        }

        public List<NewPublication> GeneratePublications(int n)
        {
            List<NewPublication> pubs = new List<NewPublication>();
            for (int i = 0; i < n; i++)
            {

                pubs.Add(new NewPublication
                {
                    Author = "Author " + randomString(6),
                    Name = "Book about " + randomString(5),
                    OtherAuthors = "Other Authors " + (4 * i).ToString(),
                    PublicationLanguage = i % 2,
                    PublicationTypeName = i,
                    PublishedBy = "PublishedBy " + i.ToString(),
                    PublishingYear = DateTime.Today.ToString("yyyy"),
                    ResourseLink = "Resourse.Link." + i.ToString() + ".com",
                    Thematics = i % 4,
                });
            }

            return pubs;
        }
        private string randomString(int lenght)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[lenght];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}
