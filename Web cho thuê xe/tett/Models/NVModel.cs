using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tett.Models
{
    public class NVModel
    {
        testDBEntities db = new testDBEntities();
        public int MaUser { get; set; }
        public string TenUser { get; set; }
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


        public void List(int ma)
        {
            this.TenUser = TenUser;
            
        }
        public void ModelNV(User user)
        {
            var _user = db.Users.Find(user.MaQuyen);            
        }
    }
}