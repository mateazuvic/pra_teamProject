using Kwizzotronic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kwizzotronic.Controllers
{
    public class AuthController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            var creator = Repository.GetCreatorWithEmailAndPassword(email, password);
            if (creator != null)
            {
                Repository.creator = creator;
                return RedirectToAction("Index", "User");
            }

            ViewBag.ErrorMessage = Resources.Web_sitemap.wrongEmailOrPassword;
            return View();
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(string FirstName, string LastName, string Email, string Password)
        {
            var creator = Repository.SaveCreator(new Creator(firstName: FirstName, lastName: LastName, email: Email, password: Password));
            if (creator != null)
            {
                Repository.creator = creator;
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.ErrorMessage = Resources.Web_sitemap.userExists;
                return View();
            }

        }

    }
}