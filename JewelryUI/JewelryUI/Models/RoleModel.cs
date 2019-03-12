using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelryUI.Models
{
    public class RoleModel
    {
        public int Id { get; set; }

        private int rol;

        public int Rol
        {
            get { return rol; }
            set { rol = 2; }
        }
    }
}