using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelryUI.Models
{
    public class ProductSummaryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string ImageUrl { get; set; }
        public string Category { get; set; }
    }
}