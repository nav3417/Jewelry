using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using JewelryDB.Jewlry;
using JewelryDB.Location;
using JewelryDB.OrderStatus;
using JewelryDB.userMgt;

namespace JewelryDB
{
  public  class Context:DbContext
    {
        public Context():base("Con")
        {

        }
        public DbSet<Jewelry> Jewelries { get; set; }
        public DbSet<JewelryCategory> Categories { get; set; }
        public DbSet<JewelryColor> Colors { get; set; }
        public DbSet<JewelryImages> Images { get; set; }
        public DbSet<JewelryType> Types { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Messages { get; set;}

    }
}
