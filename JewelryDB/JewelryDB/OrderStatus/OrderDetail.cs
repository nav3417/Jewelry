﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryDB.Jewlry;

namespace JewelryDB.OrderStatus
{
  public  class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public virtual Jewelry jewelry { get; set; }
    }
}
