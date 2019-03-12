using JewelryUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryDB.userMgt;
using JewelryDB.OrderStatus;

namespace JewelryUI.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        public ActionResult Index()
        {
            UserModel currentUser = (UserModel)Session[WebUtil.CURRENT_USER];
            List<OrderModel> Order = ModelHelper.ToOrderSummaryList(new OrderHandler().GetUsersOrder(currentUser.Id));
            ViewBag.order = Order;
            return View();
        }
    }
}