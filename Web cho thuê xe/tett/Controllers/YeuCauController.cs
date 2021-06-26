using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tett.Models;

namespace tett.Controllers
{
    public class YeuCauController : Controller
    {
        testDBEntities db = new testDBEntities();
        // GET: YeuCau
        public ActionResult GuiYeuCau()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GuiYeuCau(int id)
        {
            if (ModelState.IsValid)
            {
               
                db.Configuration.ValidateOnSaveEnabled = false;
                User u = db.Users.Where(s => s.MaUser == id).FirstOrDefault();


                //_acc.MaQuyen = 2;
                //db.Users.Add(_acc);
                YeuCau yc_kh = new YeuCau();
                yc_kh.MaUser = u.MaUser;
                
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
               
            }
            return View();
        }
    }
}