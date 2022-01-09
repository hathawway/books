using FileExchanger.Domain.DB;
using FileExchanger.Domain.Models.Content;
using FileExchanger.Domain.Models.Dictionaries;
using FileExchanger.Domain.Models.People;
using FileExchanger.ViewModels.Common;
using FileExchanger.ViewModels.Publication;
using FileExchanger.ViewModels.Publication.Additing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileExchanger.Service.Publications
{
    public class PublicationsService : IPublicable
    {
        private readonly FileExchangerDbContext _fileExchangerDbContext;
        
        public PublicationsService(FileExchangerDbContext fileExchangerDbContext)
        {
            _fileExchangerDbContext = fileExchangerDbContext ?? throw new ArgumentNullException(nameof(fileExchangerDbContext));
        }

        public IEnumerable<PublicationShortViewModel> MyPosted(Guid userId)
        {
            var publications = _fileExchangerDbContext.Publications
                .Where(p => p.Owner.Id == userId)
                .Select(x => new PublicationShortViewModel
                {
                    Id = x.Id,
                    Author = x.Author,
                    AddedTime = x.AddedDated,
                    Name = x.Name,
                    PublicationInfo = x.PublicationInfo.PublishedPlaceAndTime(),
                }).OrderByDescending(x => x.AddedTime);
            return publications;
        }

        public void AddPublication(NewPublication resource, Guid userId)
        {
            var publication = new Publication
            {
                Name = resource.Name,
                Author = resource.Author,
                OtherAuthors = resource.OtherAuthors,
                PublicationLanguage = _fileExchangerDbContext.Languages.FirstOrDefault(x => x.Id == resource.PublicationLanguage),
                AddedDated = DateTime.Today,
                PublicationInfo = new PublicationInfo
                {
                    PublicationTypeName = _fileExchangerDbContext.PublicationTypeNames.
                                            FirstOrDefault(x => x.Id == resource.PublicationTypeName),
                    PublishedBy = resource.PublishedBy,
                    ResourseLink = resource.ResourseLink,
                    PublishingYear = DateTime.ParseExact(resource.PublishingYear, "yyyy",
                                     System.Globalization.CultureInfo.InvariantCulture),
                },
                Owner = _fileExchangerDbContext.Users.FirstOrDefault(x => x.Id == userId),
            };
            
            publication.Thematics = _fileExchangerDbContext
                .Thematics
                .FirstOrDefault(x => x.Id == resource.Thematics);

            _fileExchangerDbContext.Publications.Add(publication);
            _fileExchangerDbContext.SaveChanges();
        }

        #region работа с корзиной
        public IEnumerable<PublicationShortViewModel> Cart(Guid userId)
        {
            var publications = _fileExchangerDbContext
                .Publications
                .Include(p => p.UsersCart)
                .Where(
                p => p.UsersCart.Contains(
                        _fileExchangerDbContext.Users.FirstOrDefault(u => u.Id == userId)
                    )
                )
                .Select(x => new PublicationShortViewModel()
                {
                    Id = x.Id,
                    AddedTime = x.AddedDated,
                    Amount = x.PublicationInfo.Pages,
                    Author = x.Author,
                    Name = x.Name,
                    PublicationInfo = x.PublicationInfo.PublishedBy,
                    PublicationYear = x.PublicationInfo.PublishingYear,
                    ResourseLink = x.PublicationInfo.ResourseLink
                });
            return publications;
        }
        public void AddToCart(Guid userId, long publishId)
        {
            var userWithCart = _fileExchangerDbContext
                .Users
                .Include(u => u.Cart)
                .FirstOrDefault(u => u.Id == userId);

            var publication = _fileExchangerDbContext
                .Publications
                .Where(x => x.Id == publishId).FirstOrDefault();

            var cart = userWithCart.Cart.ToList();
            cart.Add(publication);
            userWithCart.Cart = cart;
            _fileExchangerDbContext.SaveChanges();
        }
        public void RemoveFromCart(Guid userId, long publicationId)
        {
            var userWithCart = _fileExchangerDbContext
                   .Users
                   .Include(u => u.Cart)
                   .FirstOrDefault(u => u.Id == userId);

            var repeatedPublication = userWithCart.Cart.FirstOrDefault(x => x.Id == publicationId);

            if (repeatedPublication != null)
            {
                var cartIn = userWithCart.Cart.ToList();

                cartIn.Remove(repeatedPublication);
                userWithCart.Cart = cartIn;

                _fileExchangerDbContext.SaveChanges();
                return;
            }

        }
        #endregion

        #region Работа с купленным
        public IEnumerable<PublicationShortViewModel> Acquired(Guid userId)
        {
            var publications = _fileExchangerDbContext
                .Publications
                .Include(p => p.UsersAcquired)
                .Where(p => p.UsersAcquired.Contains(
                        _fileExchangerDbContext.Users.FirstOrDefault(u => u.Id == userId)
                    )
                )
                .Select(x => new PublicationShortViewModel
                {
                    Id = x.Id,
                    Author = x.Author,
                    AddedTime = x.AddedDated,
                    Name = x.Name,
                    PublicationInfo = x.PublicationInfo.PublishedPlaceAndTime(),
                }).OrderByDescending(x => x.AddedTime);

            return publications;
        }
        public void AddToAcquired(Guid userId, long publicationId)
        {
            var _user = _fileExchangerDbContext.Users
                .Include(u => u.Cart)
                .Include(u => u.Acquired)
                .Where(u => u.Id == userId)
                .FirstOrDefault();


            var publication = _user.Cart.Where(x => x.Id == publicationId).FirstOrDefault();

            var cart = _user.Cart.ToList();
            cart.Remove(publication);
            _user.Cart = cart;

            var acquired = _user.Acquired.ToList();
            acquired.Add(publication);
            _user.Acquired = acquired;
            _fileExchangerDbContext.SaveChanges();
        }
        public void RemoveFromAcquired(Guid userId, long publicationId)
        {
            var _user = _fileExchangerDbContext.Users
                .Include(u => u.Acquired)
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var publicationDuplicate = _user.Acquired.FirstOrDefault(x => x.Id == publicationId);

            if (publicationDuplicate != null)
            {
                _user.Acquired.Remove(publicationDuplicate);
                _fileExchangerDbContext.SaveChanges();
            }
        }
        #endregion

        #region Работа с каталогом
        public IEnumerable<PublicationShortViewModel> Catalog(Request request, string query, User user, long thematic)
        {
            var catalog = _fileExchangerDbContext.Publications.Include(x => x.PublicationInfo).Select(x => new PublicationShortViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Author = x.Author,
                AddedTime = x.AddedDated,
                PublicationInfo = x.PublicationInfo.PublishedPlaceAndTime(),
                IsInCurrentUserCart = x.UsersCart.Contains(user),
                IsInCurrentUserAccured = x.UsersAcquired.Contains(user),
                KeyWords = x.KeyWords(),
                Thematic = x.Thematics,
            });

            if (thematic != 0)
            {
                catalog = _fileExchangerDbContext.Publications.Include(x => x.PublicationInfo).Select(x => new PublicationShortViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Author = x.Author,
                    AddedTime = x.AddedDated,
                    PublicationInfo = x.PublicationInfo.PublishedPlaceAndTime(),
                    IsInCurrentUserCart = x.UsersCart.Contains(user),
                    IsInCurrentUserAccured = x.UsersAcquired.Contains(user),
                    KeyWords = x.KeyWords(),
                    Thematic = x.Thematics,
                }).Where(x => x.Thematic.Id == thematic);
            }

            switch (request.Sort.Field)
            {
                case "Author":
                    if (request.Sort.Acs)
                    {
                        catalog.OrderBy(x => x.Author);
                    }
                    else
                    {
                        catalog.OrderByDescending(x => x.Author);
                    }
                    break;
                case "Name":
                    if (request.Sort.Acs)
                    {
                        catalog.OrderBy(x => x.Name);
                    }
                    else
                    {
                        catalog.OrderByDescending(x => x.Name);
                    }
                    break;
                case "PublishingYear":
                    if (request.Sort.Acs)
                    {
                        catalog.OrderBy(x => x.PublicationYear);
                    }
                    else
                    {
                        catalog.OrderByDescending(x => x.PublicationYear);
                    }
                    break;
                default:
                    break;
            }

            if (query != null && query != "")
            {
                var q = (query.Split()).ToList();
                var catalogOut = new List<PublicationShortViewModel>();

                foreach(PublicationShortViewModel psvm in catalog.ToList())
                {
                    if(HasSameStrings(psvm.KeyWords, q))
                    {
                        catalogOut.Add(psvm);
                    }
                }
                return catalogOut;
            }

            return catalog.ToList();
        }
        public PublicationViewModel GetPublication(long publicationId)
        {
            var publication = _fileExchangerDbContext.Publications.Select(x => new PublicationViewModel()
            {
                Name = x.Name,
                Authors = x.Author,
                PublicationLanguage = x.PublicationLanguage.Name,
                PublicationType = x.PublicationInfo.PublicationTypeName.Name,
                PublishingYear = x.PublicationInfo.PublishingYear,
                ResourceLink = x.PublicationInfo.ResourseLink,
            }).FirstOrDefault();
            return publication;
        }
        #endregion

        public bool HasSameStrings(List<string> one, List<string> two)
        {
            foreach (string l in one )
            {
                foreach (string l2 in two)
                {
                    if (l == l2) {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
