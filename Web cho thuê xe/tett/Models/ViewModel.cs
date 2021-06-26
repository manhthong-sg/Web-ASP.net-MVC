using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tett.Models
{
    public class ViewModel
    {
        testDBEntities db = new testDBEntities();
        public int MaUser { get; set; }
        public string TenUser { get; set; }

        [DataType(DataType.Date)]
        public Nullable<System.DateTime> NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string Mail { get; set; }
        public int MaQuyen { get; set; }
        public Nullable<int> CMND { get; set; }


        [Required(ErrorMessage = "Not a valid phone number")]
        [RegularExpression(@"[0-9]{10}|(admin)", ErrorMessage = "Not a valid phone number")]
        public string SDT { get; set; }
        public string NganHang { get; set; }
        public string SoTK { get; set; }


        [DisplayName("Nhập mật khẩu")]
        [Required(ErrorMessage = "Không để trống")]
        public string PasswordUser { get; set; }


        public void GanGT(User _acc)
        {
            var user = db.Users.Where(s => s.SDT.Equals(_acc.SDT)).FirstOrDefault();
            this.MaUser = user.MaUser;
            this.TenUser = user.TenUser.Trim();
            this.NgaySinh = user.NgaySinh;
            this.DiaChi = user.DiaChi.Trim();
            this.Mail = user.Mail.Trim();
            this.MaQuyen = user.MaQuyen;
            this.CMND = user.CMND;
            this.SDT = user.SDT.Trim();
            this.NganHang = user.NganHang.Trim();
            this.SoTK = user.SoTK.Trim();
            this.PasswordUser = user.PasswordUser.Trim();
        }
    }
}