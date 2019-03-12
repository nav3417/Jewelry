using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JewelryDB.userMgt;

namespace JewelryDB.Jewlry
{
   public class SecondHandJewelry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public Nullable<DateTime> ReleaseDate { get; set; }
        public virtual JewelryCategory Category { get; set; }
        public virtual JewelryType Type { get; set; }
        public virtual ICollection<JewelryImages> Images { get; set; }
        public virtual JewelryColor Color { get; set; }
        public SecondHandJewelry()
        {
            Images = new List<JewelryImages>();
        }
        public virtual User User { get; set; }
    }
}
