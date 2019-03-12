using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelryUI.Models
{
    public class ProductAddModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public int TypeId { get; set; }

        public string ReleaseDate { get; set; }
        public string Color { get; set; }
    }
}