using FileExchanger.Domain.DB;
using FileExchanger.Security.Extensions;
using FileExchanger.Service.Account;
using FileExchanger.Service.Publications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileExchanger.Controllers
{
    public class AccountController : Controller
    {
        private readonly IPublicable _publicationService;
        private readonly IAccounting _accountSerice;
        public AccountController(IPublicable publicationService,
            IAccounting accountSerice)
        {
            _accountSerice = accountSerice;
            _publicationService = publicationService;
        }

        /// <summary>
        /// Личный кабинет - вывод информации
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            ViewData["userInfo"] = _accountSerice.GetAccountInfo(this.GetAuthorizedUser().Id);
            return View();
        }


        /// <summary>
        /// Вывод корзины пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult Cart()
        {
            return View(_publicationService.Cart(this.GetAuthorizedUser().Id));
        }
        /// <summary>
        /// Добавление публикации с id в корзину
        /// Если добавляемый Id уже находится в корзине - то он оттуда удаляется
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddToCart(long publicationId)
        {
            _publicationService.AddToCart(this.GetAuthorizedUser().Id, publicationId);
            return RedirectToAction("Index", "Catalog");
        }
        /// <summary>
        /// Удаляет из корзины покупателя публикацию с publicationId
        /// </summary>
        /// <param name="publicationId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RemoveFromCart(long publicationId)
        {
            _publicationService.RemoveFromCart(this.GetAuthorizedUser().Id, publicationId);
            return RedirectToAction("Cart", "Account");
        }


        /// <summary>
        /// Те публикации которые пользовател добавил из корзины в купленное
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IActionResult MyAdded()
        {
            return View(_publicationService.Acquired(this.GetAuthorizedUser().Id));
        }
        /// <summary>
        /// Добавление публикации c id из корзины в купленное
        /// Если добавляемый Id уже находится в купленном - то он оттуда удаляется
        /// </summary>
        /// <param name="publicationId"></param>
        [HttpPost]
        public IActionResult AddToAcquired(long publicationId)
        {
            _publicationService.AddToAcquired(this.GetAuthorizedUser().Id, publicationId);
            return RedirectToAction("Cart", "Account");
        }
        [HttpPost]
        public IActionResult AddManyToAcquired(long[] publicationIds)
        {
            foreach (var publicationId in publicationIds)
            {
                _publicationService.AddToAcquired(this.GetAuthorizedUser().Id, publicationId);
            }

            return RedirectToAction("Cart", "Account");
        }

        /// <summary>
        /// Удаление публикации из купленного
        /// </summary>
        /// <param name="publicationId"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult RemoveFromAcquired(long publicationId)
        {
            _publicationService.RemoveFromAcquired(this.GetAuthorizedUser().Id, publicationId);
            return RedirectToAction("Index", "Account");
        }


        /// <summary>
        /// Те публикации которые пользователь сам добавил на сайт 
        /// </summary>
        /// <returns>Возвращаю для текущего пользователя его загруженные посты </returns>
        [HttpGet]
        [Authorize]
        public IActionResult MyPosted()
        {
            return View(_publicationService.MyPosted(this.GetAuthorizedUser().Id));
        }

       
        /// <summary>
        /// Вьюха для смены пароля
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

    }
}
