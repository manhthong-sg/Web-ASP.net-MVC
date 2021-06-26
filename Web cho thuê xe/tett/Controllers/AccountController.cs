using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using tett.Models;

namespace tett.Controllers
{
    public class AccountController : Controller
    {
        testDBEntities db = new testDBEntities();
        // GET: Account
        public ActionResult Login()
        {
            User acc = new User();
            return View(acc);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountLogin user)
        {
            if (ModelState.IsValid)
            {
                var A = new AccountLogin();
                if(user.ID!=null)
                A.ID = user.ID.Trim();
                if (user.Password != null)
                    A.Password = user.Password.Trim();
                int checkAcc = A.IsValidAccount(A.ID, A.Password);
                var check = db.Users.Where(s=>s.SDT == user.ID.Trim() && s.PasswordUser==user.Password.Trim()).FirstOrDefault();
                if (checkAcc == 0)
                {
                    ViewBag.ErrorInfo = "Sai Thong Tin";
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                }
                else
                {
                    
                    db.Configuration.ValidateOnSaveEnabled=false;
                    Session["avatar"] = "avatar" + check.MaUser+".jpg";
                    Session["AccountID"] = check.MaUser;
                    Session["AccountName"] = check.TenUser;
                    Session["AccountMaQuyen"] = check.MaQuyen;
                    Session["Account"] = check;
                    var account = Session["Account"] as User;
                    ModelState.AddModelError("", "Đăng nhập thành công");


                    if (check.MaQuyen == 2)
                        return RedirectToAction("KH", "Home");
                    else if (check.MaQuyen == 1)
                        return RedirectToAction("RequestList", "NhanVien");
                    else
                        return RedirectToAction("CarList", "Admin");
                }
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User _acc)
        {
            if (ModelState.IsValid)
            {
                var check_ID= db.Users.Where(s => s.SDT.Equals(_acc.SDT)).FirstOrDefault();
                if (check_ID == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    _acc.MaQuyen = 2;
                    //db.Users.Add(_acc);
                    User _khach = new User();
                    _khach.TenUser = _acc.TenUser;
                    _khach.MaQuyen = _acc.MaQuyen;
                    _khach.SDT = _acc.SDT;
                    _khach.NgaySinh = DateTime.Parse("01 / 01 / 1001");
                    _khach.PasswordUser = _acc.PasswordUser;
                    db.Users.Add(_khach);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorDangKy = "ID này đã tồn tại";
                    return View();
                }
            }
            return View();
        }
    } }
    
