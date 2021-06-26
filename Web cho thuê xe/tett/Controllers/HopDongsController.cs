using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tett.Models;

namespace tett.Controllers
{
    public class HopDongsController : Controller
    {
        testDBEntities db = new testDBEntities();
        //public int sttHD = db.HopDongs.Last().SoHD;
        public ActionResult HopDongUser(string maUser)
        {
            List<HopDong> lstHD = new List<HopDong>();
            var HDs = db.HopDongs.ToList();
            foreach (HopDong x in HDs)
            {
                HopDong hd = new HopDong();
                if (x.MaUser.ToString() == maUser)
                {
                    hd.SoHD = x.SoHD;
                    hd.Ngay = x.Ngay;
                    hd.MaUser = x.MaUser;
                    hd.NoiDung = x.NoiDung;
                    hd.DonGia = x.DonGia;
                    hd.HTTT = x.HTTT;
                    hd.GiaT = x.GiaT;
                    hd.DieuKhoan = x.DieuKhoan;
                    //hd.ChiTietHopDong.MaXe = x.ChiTietHopDong.MaXe;
                    lstHD.Add(hd);
                }
            }
            return View(lstHD);
        }
        // GET: HopDongs
        public ActionResult HopDongDetails(int id)
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

        //create hop dong
        public ActionResult CreateHopDong(string maXe, string tenXe)
        {
            Session["IDXe"] = maXe;
            Session["TenXe"] = tenXe;

            Session["GiaXe"] = db.Xes.Where(s => s.MaXe == maXe).FirstOrDefault().GiaXe;
            Session["DieuKhoan"] = "Phải abc xyz";
            Session["HTTT"] = "Thanh toán trực tiếp lúc nhận xe";
            Session["GiaT"] = 0;
            Session["GiaTT"] = 0;
            return View();
        }
        [HttpPost]
        public ActionResult CreateHopDong(HopDongModel _hd)
        {
            var hopdong = db.HopDongs.ToList().Last();
            int sttHD = hopdong.SoHD;
            if (ModelState.IsValid)
            {

                db.Configuration.ValidateOnSaveEnabled = false;

                //db.Users.Add(_acc);
                //add vào hợp đồng
                HopDong hd = new HopDong();

                //int so_ngay_thue = _hd.NgayTra - _hd.NgayNhan;
                int soNgay = 0;
                if (_hd.NgayTra != null && _hd.NgayNhan != null)
                {
                    DateTime ngayM = (DateTime)_hd.NgayNhan;
                    DateTime ngayT = (DateTime)_hd.NgayTra;
                    TimeSpan Time = ngayT - ngayM;
                    soNgay = Time.Days;
                }

                //double gia_1_xe= db.Xes.Where(x => x.MaXe == Session["IDXe"].ToString()).FirstOrDefault().GiaXe;

                hd.Ngay = DateTime.Now.Date;
                hd.TinhTrang = "Doi thanh toan";
                hd.MaUser = (int)@Session["AccountID"];
                hd.NoiDung = "Kiểm tra kỹ xe trước khi nhận. Thanh toán tiền thuê xe đúng hạn.Chịu toàn bộ chi phí bảo dưỡng xe theo định kỳ. Phải tự sửa chữa nếu có xảy ra hỏng hóc nhỏ.";
                hd.DonGia = double.Parse(Session["GiaXe"].ToString()) * soNgay * 0.5;

                hd.HTTT = "Thanh toán trực tiếp lúc nhận xe";
                //decimal? giaX = gia_1_xe * soNgay;
                hd.GiaT = decimal.Parse(Session["GiaXe"].ToString()) * soNgay;
                //hd.GiaT = (decimal)_hd.GiaT;
                //hd.DonGia = decimal.Parse(Session["GiaXe"].ToString()) * soNgay;
                hd.DieuKhoan = "VUI LÒNG THANH TOÁN 50% SỐ TIỀN THUÊ KHI ĐẾN NHẬN XE. 50% CÒN LẠI THANH TOÁN LÚC TRẢ XE. NẾU QUÁ HẠN CHÚNG TÔI SẼ LẤY THÊM 5% TỔNG HOÁ ĐƠN/ 1 NGÀY";
                //if (Session["GiaT"].ToString() == "0")
                //{

                //    Session["GiaT"] = decimal.Parse(Session["GiaXe"].ToString()) * soNgay;
                //    Session["GiaTT"] = double.Parse(Session["GiaXe"].ToString()) * soNgay*0.5;
                //    return View();
                //}

                db.HopDongs.Add(hd);
                db.SaveChanges();
                //add vào chi tiết hợp đồng

                ChiTietHopDong cthd = new ChiTietHopDong();

                cthd.SoHD = sttHD + 1;
                cthd.MaXe = _hd.MaXe;
                cthd.soLuong = _hd.SoLuong;
                cthd.TraTruoc = (decimal)_hd.TraTruoc;
                cthd.NgayNhan = _hd.NgayNhan;
                cthd.NgayTra = _hd.NgayTra;
                db.ChiTietHopDongs.Add(cthd);
                db.SaveChanges();


                //add vào bảng xe ra
                XeRa xera = new XeRa();
                xera.NgayRa = (DateTime)_hd.NgayNhan;
                xera.MaXe = Session["IDXe"].ToString();
                xera.GhiChu = "";
                db.XeRas.Add(xera);


                Xe xe = db.Xes.Where(s => s.MaXe == _hd.MaXe).First();
                xe.TinhTrang = true;

                //add vào bảng xe vào
                XeVao xevao = new XeVao();
                xevao.NgayVao = (DateTime)_hd.NgayTra;
                xevao.MaXe = Session["IDXe"].ToString();
                xevao.GhiChu = "";
                db.XeVaos.Add(xevao);



                db.SaveChanges();
                return RedirectToAction("KH", "Home");
            }
            return View();
        }
        public ActionResult HoanTatHD(string maXe, int maHD)
        {
            Xe xe=db.Xes.Where(x => x.MaXe == maXe).FirstOrDefault();
            xe.TinhTrang = false;
            HopDong hd=db.HopDongs.Where(x => x.SoHD == maHD).FirstOrDefault();
            hd.TinhTrang = "Da hoan tat";
            db.SaveChanges();
            return RedirectToAction("HopDongList", "NhanVien");
        }
        public ActionResult XacNhanDaThanhToan(string maXe, int maHD)
        {
            
            HopDong hd = db.HopDongs.Where(x => x.SoHD == maHD).FirstOrDefault();
            hd.TinhTrang = "Co hieu luc";
            db.SaveChanges();
            return RedirectToAction("HopDongList", "NhanVien");
        }
        public ActionResult HuyHD(string maXe, int maHD)
        {
            Xe xe = db.Xes.Where(x => x.MaXe == maXe).FirstOrDefault();
            xe.TinhTrang = false;
            HopDong hd = db.HopDongs.Where(x => x.SoHD == maHD).FirstOrDefault();
            ChiTietHopDong cthd=db.ChiTietHopDongs.Where(x => x.SoHD == maHD).FirstOrDefault();
            hd.TinhTrang = "Da huy";
            //db.HopDongs.Remove(hd);
            db.SaveChanges();
            return RedirectToAction("HopDongList", "NhanVien");
        }
        //public ActionResult ThongBaoHDThanhCong(string tienTT, string tongTien, string ngayNhan) 
        //{
        //    Session["tienTT"] = tienTT;
        //    Session["tongTien"] = tongTien;
        //    Session["ngayNhan"] = ngay;


        //    return View();
        //}
        //public ActionResult CreateHopDong()
        //{
        //    ViewBag.MaLoaiXe = new SelectList(db.HopDongs, "SoHD", "MaUser");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult CreateHopDong([Bind(Include = "MaXe,MaLoaiXe,BienSo,TenXe,MoTa,GiaXe,TinhTrang,HinhAnh")] HopDong hd)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var check_ID = db.Xes.Where(s => s.MaXe == xe.MaXe.Trim()).FirstOrDefault();
        //        if (check_ID == null)
        //        {
        //            db.Configuration.ValidateOnSaveEnabled = false;
        //            db.Xes.Add(xe);
        //            db.SaveChanges();
        //            return RedirectToAction("CarList", "Admin");
        //        }
        //        else
        //        {
        //            ViewBag.ErrorDangKy = "Đã tồn tại mã xe này";
        //            return View(xe);
        //        }
        //    }

        //    ViewBag.MaLoaiXe = new SelectList(db.LoaiXes, "MaLoaiXe", "TenLoaiXe", xe.MaLoaiXe);
        //    return View(hd);
        //}
    }
}