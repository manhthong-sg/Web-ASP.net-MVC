using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tett.Models
{
    public class HopDongModel
    {
        testDBEntities db = new testDBEntities();
        public int SoHD { get; set; }
        public int MaUser { get; set; }
        public string TenUser { get; set; }
        public string NoiDung { get; set; }
        public Nullable<System.DateTime> Ngay { get; set; }
        public Nullable<System.DateTime> NgayNhan { get; set; }
        public Nullable<System.DateTime> NgayTra { get; set; }
        public double TraTruoc { get; set; }
        public string HTTT { get; set; }
        public double GiaT { get; set; }
        public int SoLuong = 1;
        public string TinhTrang{get; set;}
        public string MaXe { get; set; }
        public string TenXe { get; set; }
        public string DieuKhoan { get; set; }
       
    }
}