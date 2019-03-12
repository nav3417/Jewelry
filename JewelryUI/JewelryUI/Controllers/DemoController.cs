using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryDB.Jewlry;
using JewelryUI.Models;
using JewelryDB.Location;
using JewelryDB;

namespace JewelryUI.Controllers
{
    public class DemoController : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            Context con = new Context();
            LocationHandler lhandler = new LocationHandler();
            List<City> Cities = lhandler.GetCities();
            ViewBag.Cities = Cities;
            ViewBag.jewelry = ModelHelper.ToProductSummaryList(new JewelryHandler().GetJewelries());
            return View();
        }
    }
}