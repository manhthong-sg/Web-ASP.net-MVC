using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using tett.Models;

namespace tett.Controllers
{
    public class NhanVienController : Controller
    {
        testDBEntities db = new testDBEntities();
        // GET: NhanVien

        //Show khách hàng
        public ActionResult KhachHangList()
        {
            var results = (from move in db.Users
                           where move.MaQuyen == 2
                           select move).ToList();

            var A = results;
            foreach (var item in A)
            {
                if (item.NgaySinh.Equals("")) { continue; }
                else { 
                item.NgaySinh.Value.ToString("dd/mm/yyyy");
                }
            }
            return View(A);
        }

        //Yeu Cau List
        //Show yêu cầu
        public ActionResult RequestList()
        {
            var results = db.YeuCaus;
            return View(results);
        }
        //Chi tiết Yêu cầu
        public ActionResult DetailsRequest(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YeuCau yeuCau = db.YeuCaus.Find(id);
            if (yeuCau == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaUser = new SelectList(db.Users, "MaUser", "TenUser", yeuCau.MaUser);
            return View(db.YeuCaus.Where(yc => yc.MaYC == id).FirstOrDefault());
        }
        [HttpPost, ActionName("DetailsRequest")]
        [ValidateAntiForgeryToken]
        public ActionResult DetailsRequest([Bind(Include = "MaYC,MaUser,TenLoaiXe,SoCho,SoLuong,GhiChu,TinhTrang")] YeuCau yeuCau)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(yeuCau).State = EntityState.Modified;
                    //yeuCau.TinhTrang = true;
                    db.SaveChanges();
                    return RedirectToAction("RequestList");
                }
                return View(yeuCau);
            }
            catch
            {
                return Content("Không thể sửa!");
            }

        }
       

        public ActionResult HopDongList()
        {
            var results = db.HopDongs;
            return View(results);
        }

        public ActionResult DetailsHopDong(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HopDong yeuCau = db.HopDongs.Find(id);
            if (yeuCau == null)
            {
                return HttpNotFound();
            }
           
            return View(db.HopDongs.Where(yc => yc.SoHD == id).FirstOrDefault());
        }

        public ActionResult DetailUserByNV(int id)
        {
            var a = db.Users;
            return View(db.Users.Where(acc => acc.MaUser == id).FirstOrDefault());
        }
        public ActionResult DetailCateCar(string id)
        {
            var b = db.Xes;
            return View(db.Xes.Where(xe => xe.MaXe.Trim()==id).FirstOrDefault());
        }
        //public ActionResult HoanThanhHD(int id)
        //{
        //    var cthd= db.ChiTietHopDongs.Where(c=>c.SoHD=id).Fir
        //    return View();
        //}
    }
        
}
