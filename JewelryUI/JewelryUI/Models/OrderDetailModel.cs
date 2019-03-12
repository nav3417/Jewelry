using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JewelryDB.Jewlry;

namespace JewelryUI.Models
{
    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string jewelry { get; set; }
    }
}