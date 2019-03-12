using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryUI.Models;
using JewelryDB.Jewlry;

namespace JewelryUI.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
       public int Add(ShoppingCartItems items)
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];
            if (cart == null) cart = new ShoppingCart();
            cart.Add(items);
            Session.Add(WebUtil.CART, cart);
            return cart.NumberOfItems;
        }
        public ActionResult Remove(int id)
        {
            ShoppingCart cart = (ShoppingCart)Session[WebUtil.CART];
            if (cart == null) cart = new ShoppingCart();
            cart.Remove(id);
            Session.Add(WebUtil.CART, cart);
            return RedirectToAction("ViewCart");
        }
        public ActionResult ViewCart()
        {
            ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
            ViewBag.types = ModelHelper.ToSelectItemList(new JewelryHandler().GetTypes());
            ViewBag.colors = ModelHelper.ToSelectItemList(new JewelryHandler().GetColors());
            return View();
        }
    }
}