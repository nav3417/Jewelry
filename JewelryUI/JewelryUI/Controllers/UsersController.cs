using JewelryDB.userMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryUI.Models;
using JewelryDB.Location;
using JewelryDB.Jewlry;
using JewelryDB;
using JewelryDB.OrderStatus;

namespace JewelryUI.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        [HttpGet]
        public ActionResult Login()
        {
            HttpCookie cook = Request.Cookies["login"];
            if (cook != null)
            {
                cook.Expires.AddDays(1);
                Response.SetCookie(cook);
                User user = new UserHandler().GetUserByLogIn(cook.Values["login"], cook.Values["password"]);
                if (user != null)
                {
                    Session.Add(WebUtil.CURRENT_USER, ModelHelper.tomodel(user));
                    if(user.IsInRole(WebUtil.ADMIN_ROLE))
                    {
                        return RedirectToAction("home", "admin");
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                    
                }
                ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
                ViewBag.types = ModelHelper.ToSelectItemList(new JewelryHandler().GetTypes());
                ViewBag.colors = ModelHelper.ToSelectItemList(new JewelryHandler().GetColors());
            }
            return View();
         
        }
        [HttpPost]
        public ActionResult Login(LogInModel login)
        {
            User user = new UserHandler().GetUserByLogIn(login.LoginId, login.Password);
                if (user != null)
                {
                    if (Request.Browser.Cookies)
                    {
                        if (login.RememberMe)
                        {
                            HttpCookie cook = new HttpCookie("login");
                            cook.Expires.AddDays(1);
                            cook.Values.Add("login", user.LoginId);
                            cook.Values.Add("Password", user.Password);
                            Response.SetCookie(cook);
                        }
                    }
                    Session.Add(WebUtil.CURRENT_USER, ModelHelper.tomodel(user));
                //string crl = Request.QueryString["c"];
                //string act = Request.QueryString["a"];
                //if (!string.IsNullOrEmpty("crl") && string.IsNullOrEmpty("act"));
                //{
                //    return RedirectToAction(crl, act);
                //}
                if(user.IsInRole(WebUtil.ADMIN_ROLE))
                {
                    return RedirectToAction("home", "admin");

                }
                else
                {
                    return RedirectToAction("index", "home");
                }

            }
            return View();
        }
        [HttpGet]
        public ActionResult Signup()
        {
            ViewBag.categories = ModelHelper.ToSelectItemList(new JewelryHandler().Getcategories());
            ViewBag.types = ModelHelper.ToSelectItemList(new JewelryHandler().GetTypes());
            ViewBag.colors = ModelHelper.ToSelectItemList(new JewelryHandler().GetColors());
            LocationHandler lhandler = new LocationHandler();
            ViewBag.cities = ModelHelper.ToSelectItemList(lhandler.GetCities());
            if (TempData["alert"] != null)
            {
                ViewBag.alert = TempData["alert"];
            }
            return View();

        }
        [HttpPost]
        public ActionResult Signup(FormCollection data)
        {
            if (!ModelState.IsValid)
            {
                return View("Signup");
            }
            Context con = new Context();
            try
            {
                User var = new UserHandler().GetUserByLoginId(data["LoginId"]);
                if (var == null)
                {
                    User user = new User();
                    RoleModel rol = new RoleModel();
                    user.LoginId = data["LoginId"];
                    user.Password = data["Password"];
                    user.FullName = data["FullName"];
                    user.Phone = data["Cellno"];
                    user.Address = data["StreetAdress"];
                    user.Email = data["Email"];
                    user.BirthDate =Convert.ToDateTime(data["DOB"]);
                  //  user.ImageUrl = data["ImageURL"];
                    user.City = new City { Id = Convert.ToInt32(data["city"]) };
                    user.Role = new Role { Id = 2 };
                    user.SecurityQuestion = data["securityQuestion"];
                    user.SecurityAnswer = data["SecurityAnswer"];
                    long uno = DateTime.Now.Ticks;
                    foreach (string fcName in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[fcName];
                        if (!string.IsNullOrEmpty(file.FileName))
                        {
                            string url = "/Images/ProfilePic/" + uno + "_" + file.FileName.Substring(file.FileName.LastIndexOf("."));
                            string path = Request.MapPath(url);
                            file.SaveAs(path);
                            user.ImageUrl = url;
                            // user.ImageUrl.Add(new JewelryImages { Url = url, Priority = counter });
                        }
                    }

                    new UserHandler().AddUser(user);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    TempData["alert"]= var.LoginId;
                    return RedirectToAction("signup", "users");
                }
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
          
        }
        public ActionResult Logout()
        {
            HttpCookie myCookie = Request.Cookies["login"];
            if (myCookie != null)
            {
                myCookie.Expires = DateTime.Now;
                Response.SetCookie(myCookie);
            }
            Session.Abandon();
            return RedirectToAction("index","home");
        }

        public ActionResult checkuser()
        {
            UserModel usr = (UserModel)Session[WebUtil.CURRENT_USER];
            
            if (usr != null)
            {
                User user = new UserHandler().GetUserById(usr.Id);
                if (user.IsInRole(WebUtil.ADMIN_ROLE))
                {
                    return RedirectToAction("home", "admin");
                }
                else
                {
                    return RedirectToAction("index", "home");
                }

            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
        public ActionResult UserOrder(int id)
        {
            UserModel currentUser = (UserModel)Session[WebUtil.CURRENT_USER];
            User user = new UserHandler().GetUserById(currentUser.Id);
            if (user.IsInRole(WebUtil.ADMIN_ROLE))
            {
                ViewBag.admin = "admin";
            }
            ViewBag.order = ModelHelper.ToOrderSummaryList(new OrderHandler().GetUsersOrder(currentUser.Id));
            return View();
        }
        [HttpPost]
        public ActionResult feedback(FormCollection  data)
        {
            UserModel currentuser = (UserModel)Session[WebUtil.CURRENT_USER];
            Comment com = new Comment();
            com.Cellno = data["number"];
            com.Email = data["email"];
            com.Name = data["name"];
            com.Message = data["message"];
            if (currentuser != null)
            {
                com.user = new User { Id = currentuser.Id };
                new UserHandler().Addcommentwithuser(com);
            }

            if(currentuser==null)
            {
                new UserHandler().Addcomment(com);
            }
            return RedirectToAction("index","home");
        }
        [OutputCache(Duration =30)]
        public ActionResult addpost()
        {
            JewelryHandler Jhandler = new JewelryHandler();
            ViewBag.Category = ModelHelper.ToSelectItemList(Jhandler.Getcategories());
            ViewBag.Types = ModelHelper.ToSelectItemList(Jhandler.GetTypes());
            ViewBag.Color = ModelHelper.ToSelectItemList(Jhandler.GetColors());
            return View();
        }
      

    }
}