using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryDB.Jewlry;
using JewelryDB.Location;
using JewelryDB.OrderStatus;
using JewelryDB.userMgt;
using JewelryUI.Models;
using JewelryDB;
using System.Data.Entity;

namespace JewelryUI.Controllers
{
    public class OrderController : Controller
    {
        LocationHandler lhandler = new LocationHandler();
        Context con = new Context();
        // GET: Order
        [HttpGet]
        public ActionResult Index()
        {
            UserModel currentUser = (UserModel)Session[WebUtil.CURRENT_USER];
                ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
                ViewBag.types = ModelHelper.ToSelectItemList(new JewelryHandler().GetTypes());
                ViewBag.colors = ModelHelper.ToSelectItemList(new JewelryHandler().GetColors());
                ViewBag.cities = ModelHelper.ToSelectItemList(lhandler.GetCities());
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection data)
        {
            UserModel currentUser = (UserModel)Session[WebUtil.CURRENT_USER];
            if (data != null)
            {
                ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];
                Order order = new Order();
                order.OrderHolder = data["name"];
                order.status = new Status { Id = 1 };
                if (currentUser != null)
                {
                        order.User =new User {Id=currentUser.Id };   
                }
                //order.TotalAmount=
                order.Cellno = data["cellno"];
                order.Orderdate = DateTime.Now;
                order.city = new City { Id = Convert.ToInt32(data["city"]) };
                
                List<OrderDetail> detail = new List<OrderDetail>();
                if (cart != null && cart.NumberOfItems > 0)
                {
                    order.TotalAmount = 0;
                    foreach (var citems in cart.Items)
                    {
                        detail.Add(new OrderDetail { jewelry = new Jewelry { Id = citems.Id }, Quantity = citems.Quantity });
                        int c = citems.Amount;
                        order.TotalAmount = c + order.TotalAmount;
                    }
                }
                User user = new User();
                if (currentUser != null)
                {
                    user.Id = currentUser.Id;
                }
                new OrderHandler().Add(order, detail,user);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult order()
        {
            ViewBag.a1 = new OrderHandler().GetstatusOrder(1).Count;
            ViewBag.a2 = new OrderHandler().GetstatusOrder(2).Count;
            ViewBag.a3 = new OrderHandler().GetstatusOrder(3).Count;
            ViewBag.a4 = new OrderHandler().GetstatusOrder(6).Count;
            ViewBag.status = ModelHelper.ToSelectItemList(new OrderHandler().getStatuses());
            List<OrderModel> order = new List<OrderModel>();
            order = ModelHelper.ToOrderSummaryList(new OrderHandler().GetOrders());
            ViewBag.order = order;
            UserModel currentUser = (UserModel)Session[WebUtil.CURRENT_USER];
            if (currentUser != null)
            {
                User user = new UserHandler().GetUserById(currentUser.Id);
                if (user.IsInRole(WebUtil.ADMIN_ROLE))
                {
                    return View();
                }
            }
            return RedirectToAction("index", "home");
        }
        [HttpGet]
        public ActionResult detail(int id)
        {

            Order o =new OrderHandler().GetOrder(id);
            OrderModel om = new OrderModel();
            om.Id = o.Id;
            om.cellno = o.Cellno;
            om.City = o.city.Name;
            om.orderdate = o.Orderdate;
            om.OrderHolder = o.OrderHolder;
            om.totalAmount = o.TotalAmount;
            om.status = o.status.Name;
            foreach (var od in o.Detail)
            {
                OrderDetailModel odm = new OrderDetailModel();
                odm.Id = od.Id;
                odm.Quantity = od.Quantity;
                odm.jewelry = od.jewelry.Name;
                om.detail.Add(odm);
            }

            ViewBag.order = om;

            UserModel currentUser = (UserModel)Session[WebUtil.CURRENT_USER];
            if (currentUser != null)
            {
                User user = new UserHandler().GetUserById(currentUser.Id);
                if (user.IsInRole(WebUtil.ADMIN_ROLE))
                {
                    return PartialView("~/Views/Shared/_OrderDetailView.cshtml", om);
                }
            }
            return PartialView("~/Views/Shared/_orderdetailuser.cshtml", om);


        }
        public ActionResult detailonstatus(int id)
        {
         List<OrderModel> order = ModelHelper.ToOrderSummaryList(new OrderHandler().getorderbystatus(id));
            ViewBag.status = ModelHelper.ToSelectItemList(new OrderHandler().getStatuses());
            ViewBag.order = order;
            return View();
        }
        [HttpGet]
        public ActionResult Status(int id)
        {
           // Order order = (Order)Session[WebUtil.status];
            Order ordr = new Order();
           // ordr= new OrderHandler().GetOrder(id);
            TempData["order"] = new OrderHandler().GetOrder(id);
            ViewBag.status = ModelHelper.ToSelectItemList(new OrderHandler().getStatuses());
            return View();
        }
        [HttpPost]
        public ActionResult Status(FormCollection data)
        {
            Status stats = new Status();
            Order ordr = TempData["order"] as Order;
            stats.Id =Convert.ToInt32(data["status"]);
           // stats= new OrderHandler().GetSinglestatus(id);
            ordr.status = new Status { Id = stats.Id};
            //new OrderHandler().Remove(ordr.Id);
            new OrderHandler().Add(ordr);
            UserModel currentUser = (UserModel)Session[WebUtil.CURRENT_USER];
            if (currentUser != null)
            {
                User user = new UserHandler().GetUserById(currentUser.Id);
                if (user.IsInRole(WebUtil.ADMIN_ROLE))
                {
                    return RedirectToAction("order", "order");
                }
            }
            return RedirectToAction("index", "home");
        }
        public ActionResult delete(int id)
        {
            //Order order = new OrderHandler().GetOrder(id);

            OrderHandler oh = new OrderHandler();
            Context con = new Context();
            using (con)
            {
                //var ent = con.Entry(order);
         Order order= (from m in con.Orders
                        .Include(d => d.Detail)
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Category))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Color))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Images))
                        .Include(d => d.Detail.Select(j => j.jewelry).Select(c => c.Type))
                        .Include(d => d.city)
                        .Include(d => d.status)
                 where (m.Id == id)
                 select m).FirstOrDefault();
                if (order != null)
                {
                    con.Entry(order.status).State = EntityState.Unchanged;
                    con.Entry(order.city).State = EntityState.Unchanged;
                    foreach (var i in order.Detail)
                    {
                        con.Entry(i.jewelry.Category).State = EntityState.Unchanged;
                        con.Entry(i.jewelry.Color).State = EntityState.Unchanged;
                        con.Entry(i.jewelry.Type).State = EntityState.Unchanged;
                    }
                    con.Orders.Remove(order);
                    con.SaveChanges();
                }
              
            }
            return RedirectToAction("order", "order");
        }
    }
}