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
    public class AdminController : Controller
    {
        testDBEntities db = new testDBEntities();
        // GET: Admin

        //LIST ACC ------------------------
        public ActionResult AccountList()
        {
            var users = db.Users;
            return View(users);
        }


        //CREATE ACC
        public ActionResult CreateUserByAdmin()
        {
            ViewBag.MaQuyen = new SelectList(db.PhanQuyens, "MaQuyen", "TenQuyen");
            return View();
        }
        [HttpPost]
        public ActionResult CreateUserByAdmin( User user) 
        {
            ViewBag.MaQuyen = new SelectList(db.PhanQuyens, "MaQuyen", "TenQuyen", user.MaQuyen);
            if (ModelState.IsValid)
            {
                var check_ID = db.Users.Where(s => s.SDT == user.SDT.Trim()).FirstOrDefault();
                if (check_ID == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("AccountList", "Admin");
                }
                else
                {
                    ViewBag.ErrorDangKy = "SĐT này đã được đăng ký";
                    return View(user);
                }

            }
            return View(user);
        }

        //Admin có quyền quản lý nhưng không được thay đổi thông tin của User
        //DELETE ACC
        public ActionResult DeleteUserByAdmin(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(db.Users.Where(acc => acc.MaUser == id).FirstOrDefault());
        }
        [HttpPost, ActionName("DeleteUserByAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserByAdmin(int id, User _user)
        {
            try
            {
                _user = db.Users.Where(s => s.MaUser == id).FirstOrDefault();
                db.Users.Remove(_user);
                db.SaveChanges();
                return RedirectToAction("AccountList", "Admin");
            }
            catch
            {
                return Content("Không thể xoá!");
            }
        }

        //EDIT ACC
        public ActionResult EditUserByAdmin(int id)
        {
            ViewBag.MaQuyen = new SelectList(db.PhanQuyens, "MaQuyen", "TenQuyen");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(db.Users.Where(acc => acc.MaUser == id).FirstOrDefault());
        }
        [HttpPost, ActionName("EditUserByAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserByAdmin(int id, User _user)
        {
            ViewBag.MaQuyen = new SelectList(db.PhanQuyens, "MaQuyen", "TenQuyen", _user.MaQuyen);
            try
            {
                if (ModelState.IsValid)
                {
                    _user.SDT.ToString().Trim();
                    db.Entry(_user).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("AccountList");
                }
                return View();
            }
            catch
            {
                return Content("Không thể sửa!");
            }

        }




        //LIST NV --------------------
        public ActionResult StaffList()
        {
            var results = (from move in db.Users
                           where move.MaQuyen == 1
                           select move).ToList();
            
            var A = results;
            foreach (var item in A)
            {
                if (item.NgaySinh.Equals("")) { continue; }
                else
                {
                    item.NgaySinh.Value.ToString("dd/mm/yyyy");
                }
            }
            return View(A);
        }
        //Detail/Edit NV -- xong
        //DELETE NV -- Xong
        //CREATE NV -- xong

        //LIST LOAI NV

        //DELETE LOAI NV

        //EDIT LOAI NV





        //LIST CARS
        public ActionResult CarList()
        {
            var cars = db.Xes;
            return View(cars);
        }

        //CREATE CAR
        public ActionResult CreateCarByAdmin()
        {
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe");
            return View();
        }    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCarByAdmin([Bind(Include = "MaXe,MaLoaiXe,BienSo,TenXe,MoTa,GiaXe,TinhTrang,HinhAnh")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                var check_ID = db.Xes.Where(s => s.MaXe == xe.MaXe.Trim()).FirstOrDefault();
                if (check_ID == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Xes.Add(xe);
                    db.SaveChanges();
                    return RedirectToAction("CarList", "Admin");
                }
                else
                {
                    ViewBag.ErrorDangKy = "Đã tồn tại mã xe này";
                    return View(xe);
                }
            }

            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe", xe.MaLoaiXe);
            return View(xe);
        }


        //DELETE CAR
        public ActionResult DeleteCarByAdmin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            return View(xe);
        }
        [HttpPost, ActionName("DeleteCarByAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCarByAdmin(string id, Xe xe)
        {
            try
            {
             xe = db.Xes.Find(id);
            db.Xes.Remove(xe);
            db.SaveChanges();
            return RedirectToAction("CarList");
            }
            catch
            {
                return Content("Không thể xoá!");
            }
        }


        //EDIT CAR
        public ActionResult EditCarByAdmin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Xe xe = db.Xes.Find(id);
            if (xe == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe", xe.MaLoaiXe);
            return View(xe);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCarByAdmin([Bind(Include = "MaXe,MaLoaiXe,BienSo,TenXe,MoTa,GiaXe,TinhTrang,HinhAnh")] Xe xe)
        {
            try
            {
                if (ModelState.IsValid)
            {
                db.Entry(xe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CarList");
            }
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe", xe.MaLoaiXe);
            return View(xe);
        }
            catch
            {
                return Content("Không thể sửa!");
    }
}




        //LIST CATE XE ------------------------------
        public ActionResult CategoryCarList()
        {
            var cateXes = db.LoaiXes;
            return View(cateXes);
        }

        //CREATE CATE XE
        public ActionResult CreateCategoryCarByAdmin()
        {
            ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe");
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategoryCarByAdmin(LoaiXe cate)
        {
            if (ModelState.IsValid)
            {
                ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe");
                var check_ID = db.LoaiXes.Where(s => s.MaLoaiXe == cate.MaLoaiXe).FirstOrDefault();
                if (check_ID == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.LoaiXes.Add(cate);
                    db.SaveChanges();
                    return RedirectToAction("CategoryCarList", "Admin");
                }
                else
                {
                    ViewBag.ErrorDangKy = "Loại xe này đã tồn tại";
                    return View(cate);
                }

            }
            return View(cate);
        }

        //EDIT CATE XE
        public ActionResult EditCategoryCarByAdmin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiXe lx = db.LoaiXes.Find(id);
            if (lx == null)
            {
                return HttpNotFound();
            }
            return View(db.LoaiXes.Where(car=>car.MaLoaiXe == id).FirstOrDefault());
        }
        [HttpPost, ActionName("EditCategoryCarByAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategoryCarByAdmin(string id, LoaiXe _loaixe)
        {           
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(_loaixe).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("CategoryCarList");
                }
                return View();
            }
            catch
            {
                return Content("Không thể sửa!");
            }

        }

        //DELETE CATE XE
        public ActionResult DeleteCategoryCarByAdmin(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiXe lx = db.LoaiXes.Find(id);
            if (lx == null)
            {
                return HttpNotFound();
            }
            return View(db.LoaiXes.Where(l => l.MaLoaiXe == id).FirstOrDefault());
        }
        [HttpPost, ActionName("DeleteCategoryCarByAdmin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryCarByAdmin(string id, LoaiXe _loaiXe)
        {
            try
            {
                _loaiXe = db.LoaiXes.Where(s => s.MaLoaiXe == id).FirstOrDefault();
                db.LoaiXes.Remove(_loaiXe);
                db.SaveChanges();
                return RedirectToAction("CategoryCarList", "Admin");
            }
            catch
            {
                return Content("Không thể xoá!");
            }
        }




    }
}