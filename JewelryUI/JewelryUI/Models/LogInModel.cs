using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelryUI.Models
{
    public class LogInModel
    {
        public int Id { get; set; }

        public string LoginId { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}