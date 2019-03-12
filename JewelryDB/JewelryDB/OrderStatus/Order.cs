using JewelryDB.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryDB.userMgt;

namespace JewelryDB.OrderStatus
{
  public  class Order
    {
        public Order()
        {
            Detail = new List<OrderDetail>();
        }

        public int Id { get; set; }
        public DateTime Orderdate { get; set; }
        public string OrderHolder { get; set; }
        public string Cellno { get; set; }
        public int TotalAmount { get; set; }
        public virtual List<OrderDetail> Detail { get; set; }
        public virtual City city { get; set; }
        public virtual Status status { get; set; }
        public virtual User User { get; set; }
    }
}
