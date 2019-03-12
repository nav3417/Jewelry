using JewelryDB.userMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryDB.Jewlry;
using System.Data.Entity;
using JewelryUI.Models;
using System.Net;
using JewelryDB;
using JewelryDB.OrderStatus;

namespace JewelryUI.Controllers
{
    public class AdminController : Controller
    {
        Context con = new Context();
        JewelryHandler Jhandler = new JewelryHandler();
        // GET: Admin
        public ActionResult Home()
        {
           
            int c = new UserHandler().GetUsers().Count;
            ViewBag.users = c - 1;
            ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
            ViewBag.types = ModelHelper.ToSelectItemList(new JewelryHandler().GetTypes());
            ViewBag.colors = ModelHelper.ToSelectItemList(new JewelryHandler().GetColors());
            ViewBag.jewelries = ModelHelper.ToProductSummaryList(new JewelryHandler().GetJewelries());

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
        public ActionResult AddProduct()
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
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Jewelry jewelry = con.Jewelries.Find(id);
            ProductDetailModel prodct = new ProductDetailModel();
            prodct.Name = jewelry.Name;
            prodct.Price = jewelry.Price;
            prodct.Category = jewelry.Category.Name;
            prodct.Color = jewelry.Color.Name;
            prodct.Description = jewelry.Description;
            prodct.ImageUrl = (jewelry.Images.Count > 0) ? jewelry.Images.First().Url : null;
            //ViewBag.edit=prodct;

            List<SelectListItem> list = ModelHelper.ToSelectItemList(Jhandler.Getcategories());
            foreach (SelectListItem i in list)
            {
                if(i.Text==prodct.Category)
                {
                    i.Selected = true;
                }
            }
            List<SelectListItem> list1 = ModelHelper.ToSelectItemList(Jhandler.GetColors());
            foreach (SelectListItem item in list1)
            {
                if (item.Text == prodct.Color)
                {
                    item.Selected = true;
                }
            }
            List<SelectListItem> list2 = ModelHelper.ToSelectItemList(Jhandler.GetTypes());
            foreach (SelectListItem item in list2)
            {
                if (item.Text == prodct.type)
                {
                    item.Selected = true;
                }
            }


            ViewBag.Category = list;
            ViewBag.Types = list2;
            ViewBag.Color = list1;
            UserModel currentUser = (UserModel)Session[WebUtil.CURRENT_USER];
            if (currentUser != null)
            {
                User user = new UserHandler().GetUserById(currentUser.Id);
                if (user.IsInRole(WebUtil.ADMIN_ROLE))
                {
                    return View(prodct);
                }
            }
            return RedirectToAction("index", "home");
        }
        [HttpPost]
        public ActionResult Edit(ProductDetailModel data, FormCollection dat)
        {
            JewelryCategory category = new JewelryCategory();
            category.Id =Convert.ToInt32(dat["Category"]);
            JewelryColor color = new JewelryColor();
            color.Id = Convert.ToInt32(dat["color"]);
            JewelryType type = new JewelryType();
            type.Id = Convert.ToInt32(dat["Type"]);
            JewelryImages images = new JewelryImages();
            //images.Id = Convert.ToInt32(data.ImageUrl);

            Jewelry jewelry = new Jewelry
            {
                Id = data.Id,
                Name = data.Name,
                Description=data.Description,
                Price=data.Price,
                Category = new JewelryCategory { Id = category.Id },
                Color = new JewelryColor { Id = color.Id },
                Type = new JewelryType { Id = type.Id },
            };
            new JewelryHandler().update(jewelry);

            return RedirectToAction("home", "admin");

        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            OrderDetail od = new OrderHandler().getdetail(id);
            if (od == null)
            {
                Jewelry jewelri = (from j in con.Jewelries
                                   .Include(D => D.Images)
                                   .Include(a => a.Type)
                                   .Include(a => a.Category)
                                   .Include(a => a.Color)
                                   where (j.Id == id)
                                   select j).FirstOrDefault();
                //Jewelry jewelry = con.Jewelries.Find(id);
                con.Entry(jewelri.Category).State = EntityState.Unchanged;
                con.Entry(jewelri.Color).State = EntityState.Unchanged;
                con.Entry(jewelri.Type).State = EntityState.Unchanged;
                con.Jewelries.Remove(jewelri);
                con.SaveChanges();
                return RedirectToAction("home", "admin");
            }
            return View();
        }
        [HttpPost]
        //public ActionResult Delete(int id)
        //{

        //    return View();
        //}
        [HttpGet]
        public ActionResult Detail()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Detail(int id)
        {

            return View();
        }

    }
}