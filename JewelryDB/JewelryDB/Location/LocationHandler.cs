using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryDB.Location
{
   public class LocationHandler
    {
        public List<City> GetCities()
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Cities select m).ToList();
            }
        }
        public City GetCityById(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Cities where(m.Id==id) select m).FirstOrDefault();
            }
        }

    }
}
