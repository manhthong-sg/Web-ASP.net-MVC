using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tett.Models;

namespace tett.Controllers
{
    public class CarsController : Controller
    {
        // GET: Cars
        testDBEntities db = new testDBEntities();
        // GET: CarsList
        public ActionResult Index()
        {
            List<Xe> cars = new List<Models.Xe>();
            var lstCars = db.Xes.ToList();
            foreach (Xe x in lstCars)
            {
                Xe car = new Xe();
                car.MaXe = x.MaXe;
                car.BienSo = x.BienSo;
                car.TenXe = x.TenXe;
                car.HinhAnh = x.HinhAnh;
                car.GiaXe = x.GiaXe;

                cars.Add(car);
            }
            return View(cars);
        }
        public ActionResult CarDetails(string id)
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
            //Xe car = new Xe();
            //car.MaXe = xe.MaXe;
            //car.TenXe = xe.TenXe;
            //car.GiaLoaiXe = xe.GiaLoaiXe;
            //car.HinhAnh = xe.HinhAnh;
            //car.MoTa = xe.MoTa;
            return View(xe);
        }

        //public ActionResult EditTinhTrangXe(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Xe xe = db.Xes.Find(id);
        //    if (xe == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.TinhTrang = new SelectList(db.Xes, "TrangThai", xe.TinhTrang);
        //    return View(xe);
        //}

        public ActionResult GioDatXe()
        {
            return View();
        }
    }
}