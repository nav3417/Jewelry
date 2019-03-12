using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryDB.Jewlry;
using JewelryUI.Models;
using JewelryDB.userMgt;

namespace JewelryUI.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult create()
        {

            JewelryHandler Jhandler = new JewelryHandler();
            ViewBag.Category = ModelHelper.ToSelectItemList(Jhandler.Getcategories());
            ViewBag.Types = ModelHelper.ToSelectItemList(Jhandler.GetTypes());
            ViewBag.Color = ModelHelper.ToSelectItemList(Jhandler.GetColors());
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
        [HttpPost]
        public ActionResult create(FormCollection  data)
        {
            Jewelry jewelry = new Jewelry();
            jewelry.Name = data["name"];
            jewelry.Price = Convert.ToInt32(data["price"]);
            if (string.IsNullOrEmpty(data["Releasedate"]))
            {
                jewelry.ReleaseDate = null;
            }
            else
            {
                string[] dParts = data["Releasedate"].Split('/');
                jewelry.ReleaseDate = new DateTime(Convert.ToInt32(dParts[2]), Convert.ToInt32(dParts[1]), Convert.ToInt32(dParts[0]));
            }
            jewelry.Category = new JewelryCategory { Id = Convert.ToInt32(data["category"]) };
            jewelry.Color = new JewelryColor { Id = Convert.ToInt32(data["Color"]) };
            jewelry.Type = new JewelryType { Id = Convert.ToInt32(data["types"]) };
            jewelry.Description = data["description"];
            long uno = DateTime.Now.Ticks;
            int counter = 0;
            foreach (string fcName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fcName];
                if (!string.IsNullOrEmpty(file.FileName))
                {
                    string url = "/Images/products/" + uno + "_" + ++counter + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    string path = Request.MapPath(url);
                    file.SaveAs(path);
                    jewelry.Images.Add(new JewelryImages { Url = url, Priority = counter });
                }
            }
            new JewelryHandler().Add(jewelry);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult CategoryList()
        {
            ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
            return View();
        }
        public ActionResult show(int id)
        {
            ProductDetailModel product = ModelHelper.ProductSummary(new JewelryHandler().GetJewelryById(id));
            ViewBag.produst = product;
            return View();
        }
        public ActionResult latest()
        {
            ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
            ViewBag.types = ModelHelper.ToSelectItemList(new JewelryHandler().GetTypes());
            ViewBag.colors = ModelHelper.ToSelectItemList(new JewelryHandler().GetColors());
            ViewBag.latest = ModelHelper.ToProductSummaryList(new JewelryHandler().GetLatestJewelry());
            return View();
        }
        public ActionResult bycategory(int id)
        {
            ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
            ViewBag.types = ModelHelper.ToSelectItemList(new JewelryHandler().GetTypes());
            ViewBag.colors = ModelHelper.ToSelectItemList(new JewelryHandler().GetColors());
            ViewBag.bycategory = ModelHelper.ToProductSummaryList(new JewelryHandler().GetJewelryByCateryId(new JewelryCategory { Id = id }));
            return View();
        }
        public ActionResult ByType(int id)
        {
            ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
            ViewBag.types = ModelHelper.ToSelectItemList(new JewelryHandler().GetTypes());
            ViewBag.colors = ModelHelper.ToSelectItemList(new JewelryHandler().GetColors());
            ViewBag.type = ModelHelper.ToProductSummaryList(new JewelryHandler().GetJewelryByTypeId(new JewelryType { Id = id }));
            return View();
        }
        public ActionResult byColor(int id)
        {
            ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
            ViewBag.types = ModelHelper.ToSelectItemList(new JewelryHandler().GetTypes());
            ViewBag.colors = ModelHelper.ToSelectItemList(new JewelryHandler().GetColors());
            ViewBag.color = ModelHelper.ToProductSummaryList(new JewelryHandler().GeJewelrytByColorId(new JewelryColor { Id = id }));
            return View();
        }
        public ActionResult productbyId(int id)
        {
            ViewBag.products = ModelHelper.ProductSummary(new JewelryHandler().GetJewelryById(id));
            return View();
        }
       
    }
}