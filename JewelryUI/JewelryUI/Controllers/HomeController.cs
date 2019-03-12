using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryUI.Models;
using JewelryDB.Jewlry;
using JewelryDB.OrderStatus;
using JewelryDB.userMgt;
using Telerik.ReportViewer.Mvc;

namespace JewelryUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (new JewelryHandler().GetJewelries().Count > 0)
            {
                ViewBag.jewelries = ModelHelper.ToProductSummaryList(new JewelryHandler().GetJewelries());
            }
            ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
            ViewBag.types = ModelHelper.ToSelectItemList(new JewelryHandler().GetTypes());
            ViewBag.colors = ModelHelper.ToSelectItemList(new JewelryHandler().GetColors());
            UserModel currentUser = (UserModel)Session[WebUtil.CURRENT_USER];
            if(currentUser != null)
            {
                List<OrderModel> Order = ModelHelper.ToOrderSummaryList(new OrderHandler().GetUsersOrder(currentUser.Id));
                ViewBag.order = Order;
                int count = ModelHelper.ToOrderSummaryList(new OrderHandler().GetUsersOrder(currentUser.Id)).Count;
                ViewBag.count = count;
            }
            return View();
        }
        public ActionResult CustomReport()
        {
            var user =ModelHelper.tomodel(new UserHandler().GetUsers().FirstOrDefault());
            return View(user);
        }
        public ActionResult Jewelry()
        {

            //Telerik.Reporting.Report report1 = new Telerik.Reporting.Report();
            //report1.DocumentName = "Jewelry.trdp";
            //Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            //reportParameter1.Name = "Name";
            //reportParameter1.Text = "Naveed Khalid";
            //reportParameter1.Type = Telerik.Reporting.ReportParameterType.Integer;
            //reportParameter1.AllowBlank = false;
            //reportParameter1.AllowNull = false;
            //reportParameter1.Value = "=10";
            //reportParameter1.Visible = true;
            //report1.ReportParameters.Add(reportParameter1);
            return View();
        }
      


    }
}