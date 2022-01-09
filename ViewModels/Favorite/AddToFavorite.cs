using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileExchanger.ViewModels.Favorite
{
    public class AddToFavorite
    {
        /// <summary>
        /// Номер публикации, которую необходимо добавить в избранное
        /// </summary>
        public long PublicationId { get; set; }
    }
}
