using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryDB.userMgt;

namespace JewelryDB.OrderStatus
{
  public  class OrderHandler
    {

      public  List<Order> GetOrders()
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Orders
                        .Include(d => d.Detail)
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Category))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Color))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Images))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Type))
                        .Include("city")
                        .Include("status")
                        .Include(d=>d.User)
                        select m).ToList();
            }
        }

        public List<Order> GetstatusOrder(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Orders
                        .Include(d => d.Detail)
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Category))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Color))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Images))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Type))
                        .Include("city")
                        .Include("status")
                        .Include(u=>u.User)
                        where(m.status.Id==id)
                        select m).ToList();
            }
        }
         public List<Order> GetUsersOrder(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Orders
                        .Include(d => d.Detail)
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Category))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Color))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Images))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Type))
                        .Include("city")
                        .Include("status")
                        .Include(u => u.User)
                        where (m.User.Id==id)
                        select m).ToList();
            }
        }
        public Order GetOrder(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Orders
                        .Include(d=>d.Detail)
                        .Include(d=>d.Detail.Select(j=>j.jewelry).Select(c=>c.Category))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Color))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Images))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Type))
                        .Include(d=>d.city)
                        .Include(d=>d.status)
                        .Include(u => u.User)
                        where (m.Id == id) select m).FirstOrDefault();
            }
        }

        public List<Order> getorderbystatus(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Orders
                         .Include(d => d.Detail)
                         .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Category))
                         .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Color))
                         .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Images))
                         .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Type))
                         .Include("city")
                         .Include("status")
                         .Include(u => u.User)
                        where (m.status.Id == id)
                        select m).ToList();
            }
        }
        public List<Status> getStatuses()
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.statuses select m).ToList();
            }
        }
        public Status GetSinglestatus(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.statuses where m.Id == id select m).FirstOrDefault();
            }
        }
        public OrderDetail getdetail(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.OrderDetails
                        .Include(Q=>Q.jewelry)
                        where(m.jewelry.Id==id)
                        select m).FirstOrDefault();
            }
        }

        public void Add(Order order,List<OrderDetail> detail,User user)
        {
            Context con = new Context();
            using (con)
            {
                con.Entry(order.city).State = EntityState.Unchanged;
                con.Entry(order.status).State = EntityState.Unchanged;
                if (user.Id>0)
                {
                    con.Entry(order.User).State = EntityState.Unchanged;
                }
                foreach (var i in detail)
                {
                    con.Entry(i.jewelry).State = EntityState.Unchanged;
                    order.Detail.Add(i);
                }
                con.Orders.Add(order);
                con.SaveChanges();
            }
        }
        public void Add(Order order)
        {
            Context con = new Context();
            using (con)
            {
                Order j = con.Orders.Find(order.Id);
                if(j!=null)
                {
                    // con.Entry(j).CurrentValues.SetValues(order);
                    //foreach (var i in order.Detail)
                    //{
                    //    OrderDetail k = con.OrderDetails.Find(i.Id);
                    //    con.Entry(k).CurrentValues.SetValues(i);
                    //}
                    //con.Entry(order.city).State = EntityState.Unchanged;
                    //con.Entry(order.status).State = EntityState.Unchanged;
                    //foreach (var i in order.Detail)
                    //{
                    //    con.Entry(i.jewelry).State = EntityState.Unchanged;
                    //    order.Detail.Add(i);
                    //}
                    j.status = new Status() { Id = order.status.Id};
                    con.Entry(j.status).State = EntityState.Unchanged;
                    //con.Orders.Add(order);
                    con.SaveChanges();
                }
            }
        }

    }
}
