using IChipo.YJH.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Manager.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var bll = new yjh_user_Repository();
           var data= bll.Query(p => p.Id != null);
            ViewBag.UserName = data?.FirstOrDefault().UserName;
            return View(data);
        }

        public ActionResult BootPage() {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}