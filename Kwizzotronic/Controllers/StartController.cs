using Kwizzotronic.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Kwizzotronic.Controllers
{
    public class StartController : BaseController
    {
        [HttpGet]
        public ActionResult SelectLanguage()
        {
            return View();
        }


        public ActionResult SelectEnglish()
        {
            SetCulture("en");
            return View("SelectMode");
        }

        public ActionResult SelectCroatian()
        {
            SetCulture("");
            return View("SelectMode");
        }

        public ActionResult GoToAuth()
        {
            return RedirectToAction("Index", "Auth");
        }

        [HttpGet]
        public ActionResult SelectMode()
        {
            return View();
        }

        private void SetCulture(string language)
        {
            HttpCookie cookie = new HttpCookie("language");
            cookie.Expires.AddDays(10);
            cookie["name"] = language;
            Response.Cookies.Add(cookie);
            var cultureInfo = CultureInfo.CreateSpecificCulture(language);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }

    }
}