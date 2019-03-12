using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JewelryUI.Models
{
    public class DDList
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SelectListItem> Values { get; set; }
        public string Caption { get; set; }
    }
}