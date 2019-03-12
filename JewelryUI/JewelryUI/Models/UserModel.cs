using JewelryDB.userMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelryUI.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string LoginId { get; set; }

        public string Password { get; set; }
        public string ImgURL { get; set; }

        public string FullName { get; set; }
        public int Amount { get; set; }
    }
}