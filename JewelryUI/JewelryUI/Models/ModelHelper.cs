using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JewelryDB.Jewlry;
using JewelryDB.OrderStatus;
using JewelryDB.userMgt;

namespace JewelryUI.Models
{
    public class ModelHelper
    {
        public static List<SelectListItem> ToSelectItemList(dynamic values)
        {
            List<SelectListItem> tempList = new List<SelectListItem>();
            if (values != null)
            {
                tempList = new List<SelectListItem>();
                foreach (var v in values)
                {
                    tempList.Add(new SelectListItem { Text = v.Name, Value = Convert.ToString(v.Id) });
                }
                tempList.TrimExcess();
            }
            return tempList;
        }
        public static List<ProductSummaryModel> ToProductSummaryList(IEnumerable<Jewelry> jewelries)
        {
            List<ProductSummaryModel> tempList = new List<ProductSummaryModel>();
            if (jewelries != null)
            {
                foreach (var m in jewelries)
                {
                    tempList.Add(ToProductSummary(m));
                }
                tempList.TrimExcess();
            }
            return tempList;
        }
        public static ProductSummaryModel ToProductSummary(Jewelry jewelry)
        {
            return new ProductSummaryModel
            {
                Id = jewelry.Id,
                Name = jewelry.Name,
                Price = jewelry.Price,
                Category=jewelry.Category.Name,
                ImageUrl = (jewelry.Images.Count > 0) ? jewelry.Images.First().Url : null
            };
        }
        public static ProductDetailModel ProductSummary(Jewelry jewelry)
        {
            return new ProductDetailModel
            {
                Id = jewelry.Id,
                Name = jewelry.Name,
                Price = jewelry.Price,
                ImageUrl = (jewelry.Images.Count > 0) ? jewelry.Images.First().Url : null,
                Category=jewelry.Category.Name,
                Color=jewelry.Color.Name,
                Description=jewelry.Description,
                ReleaseDate=jewelry.ReleaseDate,
                type=jewelry.Type.Name
            };
        }
        public static List<StatusModel>  tostatuslist(IEnumerable<Status> statu)
        {
            List<StatusModel> templist = new List<StatusModel>();
            if(statu!=null)
            {
                foreach (var i in statu)
                {
                    templist.Add(Tostatus(i));
                }
                templist.TrimExcess();
            }
            return templist;
        }
        public static StatusModel Tostatus(Status status)
        {
            return new StatusModel
            {
                Id = status.Id,
                Name=status.Name
               
            };
        }
        public static List<OrderModel> ToOrderSummaryList(IEnumerable<Order> orders)
        {
            List<OrderModel> tempList = new List<OrderModel>();
            if (orders != null)
            {
                foreach (var m in orders)
                {
                    tempList.Add(ToOrderSummary(m));
                }
                tempList.TrimExcess();
            }
            return tempList;
        }
        public static OrderModel ToOrderSummary(Order order)
        {
            return new OrderModel
            {
                Id = order.Id,
                OrderHolder = order.OrderHolder,
                City = order.city.Name,
                cellno = order.Cellno,
                totalAmount = order.TotalAmount,
                orderdate = order.Orderdate,
                status = order.status.Name,
                
            };
        }
        public static UserModel tomodel(User user)
        {
            return new UserModel
            {
              LoginId=user.LoginId,
              Password=user.Password,
              FullName=user.FullName,
              ImgURL=user.ImageUrl,
              Id=user.Id,
              Amount=user.Amount
            };
        }

    }
}