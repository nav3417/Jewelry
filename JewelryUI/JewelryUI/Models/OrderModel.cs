using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelryUI.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            detail = new List<OrderDetailModel>();
        }
        public int Id { get; set; }
        public DateTime orderdate { get; set; }
        public string OrderHolder { get; set; }
        public string City { get; set; }
        public string cellno { get; set; }
        public string status { get; set; }
        public int totalAmount { get; set; }
        public List<OrderDetailModel> detail { get; set; }
    }
}