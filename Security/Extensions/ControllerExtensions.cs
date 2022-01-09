﻿using FileExchanger.Exceptions.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using FileExchanger.Domain.Models;
using FileExchanger.Domain.Models.People;

namespace FileExchanger.Security.Extensions
{
    /// <summary>
    /// Методы расширения безопасности, применяемые в контроллерах
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Возвращает ID успешно авторизованного пользователя <see cref="User"/>
        /// </summary>
        /// <param name="controller">Контроллер</param>
        /// <returns>ID успешно авторизованного пользователя <see cref="User"/></returns>
        private static Guid GetAuthorizedUserId(this ControllerBase controller)
        {
            var claims = controller.HttpContext.User.Claims;
            var claim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            return claim != null ? Guid.Parse(claim.Value) : Guid.Empty;
        }

        /// <summary>
        /// Возвращает успешно авторизованного пользователя <see cref="User"/>
        /// </summary>
        /// <param name="controller">Контроллер</param>
        /// <returns>Успешно авторизованный пользователь <see cref="User"/></returns>
        private static User TryGetAuthorizedUser(this ControllerBase controller)
        {
            var userId = controller.GetAuthorizedUserId();
            var userManager = controller.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
            return userManager.Users.Include(x => x.Employee).FirstOrDefault(x => x.Id == userId);
        }

        /// <summary>
        /// Возвращает успешно авторизованного пользователя <see cref="User"/>
        /// </summary>
        /// <param name="controller">Контроллер</param>
        /// <exception cref="UnauthorizedException">Не удалось получить авторизованного пользователя</exception>
        /// <returns>Успешно авторизованный пользователь <see cref="User"/></returns>
        public static User GetAuthorizedUser(this ControllerBase controller)
        {
            return controller.TryGetAuthorizedUser() ?? throw new UnauthorizedException();
        }

        /// <summary>
        /// Возвращает ip адрес удаленного клиента
        /// </summary>
        /// <param name="controller">Контроллер</param>
        /// <returns>ip адрес клиента</returns>
        public static string GetUserRemoteIpAddress(this ControllerBase controller)
        {
            return controller.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
