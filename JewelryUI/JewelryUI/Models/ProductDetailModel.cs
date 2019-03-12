using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelryUI.Models
{
    public class ProductDetailModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Category { get; set; }

        public string type { get; set; }

        public Nullable<DateTime> ReleaseDate { get; set; }
        public string Color { get; set; }
    }
}