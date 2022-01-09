using FileExchanger.ViewModels.Publication.Additing;
using Microsoft.AspNetCore.Mvc;
using FileExchanger.Service.Publications;
using FileExchanger.Security.Extensions;
using Microsoft.AspNetCore.Authorization;
using FileExchanger.Service.BasicData;

namespace FileExchanger.Controllers
{
    public class PublicationController : Controller
    {

        private readonly IPublicable _publicationService;
        private readonly IDictionaire _dictionaireService;

        public PublicationController(
            IPublicable publicationService, IDictionaire dictionaireService)
        {
            _publicationService = publicationService;
            _dictionaireService = dictionaireService;
        }

        #region Публикации (добавление, выдача)
       
        /// <summary>
        /// Добавление ресурса в базу
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public IActionResult AddResourceAsync(NewPublication resource)
        {
            var user = this.GetAuthorizedUser();
            _publicationService.AddPublication(resource, user.Id);
            return RedirectToAction("MyPosted", "Account");
        }

        /// <summary>
        /// Страница добавления ресурса
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            ViewData["Thematics"] = _dictionaireService.GetThematics();
            ViewData["PublicationTypeName"] = _dictionaireService.GetPublicationTypeNames();
            ViewData["Langs"] = _dictionaireService.GetLanguages();
            return View();
        }
        #endregion

        /// <summary>
        /// Даёт полную информацию по публикации publicationId
        /// </summary>
        /// <param name="publicationId"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Publication(long publicationId)
        {
            return View(_publicationService.GetPublication(publicationId));
        }
    }
}
