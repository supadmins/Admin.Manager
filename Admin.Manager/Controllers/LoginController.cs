using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Manager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EditPassWord()
        {
            return View();
        }

        public ActionResult EditPassWordDemo()
        {
            return View();
        }
    }
}