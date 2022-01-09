using FileExchanger.Domain.DB;
using FileExchanger.Domain.Models.People;
using FileExchanger.Security.Extensions;
using FileExchanger.Service;
using FileExchanger.Service.BasicData;
using FileExchanger.Service.Publications;
using FileExchanger.ViewModels.Common;
using FileExchanger.ViewModels.Publication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FileExchanger.Controllers
{
    [Route("Catalog")]
    public class CatalogController : Controller
    {

        private readonly IPublicable _publicationService;
        private readonly IDictionaire _dictionaireService;

        public CatalogController(IPublicable publicationService, IDictionaire dictionaireService)
        {
            _publicationService = publicationService;
            _dictionaireService = dictionaireService;
        }

        [HttpGet]
        public ActionResult Index(string SortField, string query, long thematic, bool Acs = true)
        {
            var catalog = _publicationService.Catalog(
                new Request()
                {
                    Sort = new Sort
                    {
                        Acs = Acs,
                        Field = SortField
                    },
                },
                query,
                this.GetAuthorizedUser(), thematic);
            ViewData["Thematics"] = _dictionaireService.GetThematics();
            return View(catalog);
        }
    
        [HttpPost]
        public ActionResult AddToFavorite(long Id)
        {
            return RedirectToAction("Index", "Catalog");
        }    
    }
}
