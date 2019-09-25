using Katalog_v_2.Models;
using Katalog_v_2.Models.Interface;
using Katalog_v_2.Service.BDService;
using Katalog_v_2.Service.FileService;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace Katalog_v_2.Controllers
{
    public class UserController : Controller
    {
        private readonly IUser _service;

        public UserController()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["BD"]) == true)
            {
                _service = new UserService();
            }
            else
            {
                _service = new UserFileService();
                
            }
        }

        public ActionResult Autorization()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autorization(User new_user)
        {
             if (new_user.Login != "" && new_user.Password != "")
            {
                if (_service.Authorization(new_user))
                {
                    FormsAuthentication.SetAuthCookie(new_user.Login, true);
                    return RedirectToAction("Main", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }
            return RedirectToAction("Autorization_copy", "User");
        }

        public ActionResult Autorization_copy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autorization_copy(User new_user)
        {
            if (new_user.Login != "" && new_user.Password != "")
            {
                if (_service.Authorization(new_user))
                {
                    FormsAuthentication.SetAuthCookie(new_user.Login, true);
                    return RedirectToAction("Procedures", "Procedure");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }
            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(User user)
        {
            if (user.Login != "" && user.Password != "")
            {
                if (_service.Registrations(user))
                {
                    FormsAuthentication.SetAuthCookie(user.Login, true);
                    return RedirectToAction("Main", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }
            return RedirectToAction("Registration", "User");
        }
    }
}