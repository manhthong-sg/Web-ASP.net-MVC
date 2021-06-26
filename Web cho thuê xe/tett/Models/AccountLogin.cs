using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tett.Models
{
    public class AccountLogin
    {
        [Required(ErrorMessage = "Not a valid phone number")]
        [RegularExpression(@"[0-9]{10}|(admin)", ErrorMessage = "Not a valid phone number")]
        public string ID { get; set; }
        public string Password { get; set; }


        testDBEntities db = new testDBEntities();
        public void GanGT(User _acc)
        {
            var user = db.Users.Where(s => s.SDT.Equals(_acc.SDT)).FirstOrDefault();
            this.ID = user.SDT;
            this.Password = user.PasswordUser;
        }
        public int IsValidAccount(string id, string password)
        {
            var check = db.Users.Where(s => s.SDT.Equals(id.Trim()) && s.PasswordUser.Equals(password.Trim())).FirstOrDefault();

            if (check != null)
                return 1;
            return 0;
        }
    }
}