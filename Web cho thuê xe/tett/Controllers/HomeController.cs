using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tett.Models;

namespace tett.Controllers
{
    public class HomeController : Controller
    {
        testDBEntities db = new testDBEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchCar(string str)
        {
            var links = from l in db.Xes select l;
            if (!string.IsNullOrEmpty(str))
            {
                links = links.Where(s => s.TenXe.Contains(str));
            }
            return View(links);
        }
        public ActionResult KH()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

    }
}