using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using JewelryDB.Jewlry;
using JewelryUI.Models;

namespace JewelryUI.API
{
    [RoutePrefix("api/Jewelry")]
    public class JewelryController : ApiController
    {
      public  IHttpActionResult GetAlljewelries()
        {
            return Ok ((from m in new JewelryHandler().GetJewelries()
                     select ToProductModel(m)).ToList());
            
        }
        public IHttpActionResult Get(int id)
        {
            return Ok((from m in new JewelryHandler().GetJewelries()
                       where(m.Id==id)
                       select ToProductModel(m)).ToList());

        }
        [Route("category/{name}")]
        public IHttpActionResult Get(string name)
        {
            int get = new JewelryHandler().Getcategybyname(name);
            return Ok((from m in new JewelryHandler().GetJewelries()
                       where(m.Category.Id==get)
                       select ToProductModel(m)).ToList());

        }
        private ProductDetailModel ToProductModel(Jewelry m)
        {
            return new ProductDetailModel
            {
                Id = m.Id,
                Name = m.Name,
                Price= m.Price,
                Description = m.Description,
                ImageUrl = (m.Images != null && m.Images.Count() > 0) ? m.Images.First().Url : null,
                Category=m.Category.Name,
                Color=m.Color.Name,
                type=m.Type.Name,
               // ReleaseDate = (m.ReleaseDate != null) ? m.ReleaseDate.Value.ToString("dd-MM-yyyy") : null
            };
        }
    }
}
