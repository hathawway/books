using FileExchanger.Domain.DB;
using FileExchanger.Domain.Models.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileExchanger.Service.BasicData
{
    public class DictinaryService : IDictionaire
    {
        private readonly FileExchangerDbContext _fileExchangerDbContext;
        public DictinaryService(FileExchangerDbContext fileExchangerDbContext)
        {
            _fileExchangerDbContext = fileExchangerDbContext ?? throw new ArgumentNullException(nameof(fileExchangerDbContext));
        }

        public IEnumerable<Language> GetLanguages()
        {
            var languages = _fileExchangerDbContext.Languages.Select(x => new Language()
            {
                Id = x.Id,
                Alias = x.Alias,
                Name = x.Name,
            });
            return languages;
        }

        public IEnumerable<PublicationTypeName> GetPublicationTypeNames()
        {
            return _fileExchangerDbContext.PublicationTypeNames.Select(x => new PublicationTypeName()
            {
                Id = x.Id,
                Name = x.Name,
            });
        }

        public IEnumerable<Thematic> GetThematics()
        {
            var Thematics = _fileExchangerDbContext.Thematics.Select(x => new Thematic()
            {
                Id = x.Id,
                Name = x.Name,
            });

            return Thematics;
        }
    }
}
