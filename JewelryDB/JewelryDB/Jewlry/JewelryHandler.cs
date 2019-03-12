using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryDB.Jewlry
{
   public class JewelryHandler
    {
        public List<Jewelry> GetJewelries()
        {
            Context con = new Context();
            using(con)
            {
                return (from m in con.Jewelries
                        .Include("Category")
                        .Include("Type")
                        .Include("Images")
                        .Include("Color")
                        select m).ToList();
            }
        }
      
        public Jewelry GetJewelryById(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Jewelries
                        .Include("Category")
                        .Include("Type")
                        .Include("Images")
                        .Include("Color")
                        where(m.Id==id)
                        select m).FirstOrDefault();
            }
        }
        public List<Jewelry> GetJewelryByCateryId(JewelryCategory Category)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Jewelries
                         .Include("Category")
                        .Include("Type")
                        .Include("Images")
                        .Include("Color")
                        where(m.Category.Id==Category.Id)
                        select m).ToList();
            }
        }
        public List<Jewelry> GetJewelryByTypeId(JewelryType Type)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Jewelries
                         .Include("Category")
                        .Include("Type")
                        .Include("Images")
                        .Include("Color")
                        where (m.Type.Id == Type.Id)
                        select m).ToList();
            }
        }
        public int Getcategybyname(string name)
        {
            Context con = new Context();
            return con.Categories.FirstOrDefault(x => x.Name.Equals(name)).Id;
        }
        public List<Jewelry> GeJewelrytByColorId(JewelryColor Color)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Jewelries
                         .Include("Category")
                        .Include("Type")
                        .Include("Images")
                        .Include("Color")
                        where (m.Color.Id == Color.Id)
                        select m).ToList();
            }
        }
        public List<Jewelry> GetLatestJewelry()
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Jewelries
                        .Include("Category")
                        .Include("Type")
                        .Include("Images")
                        .Include("Color")
                        orderby m.ReleaseDate descending
                       select m).ToList();
            }
        }
        public List<JewelryCategory> Getcategories()
        {
            Context con = new Context();
            using(con)
            {
                return (from m in con.Categories
                        select m).ToList();
            }
        }
        public JewelryCategory Getcategory(JewelryCategory Category)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Categories
                        where(m.Id==Category.Id)
                        select m).FirstOrDefault();
            }
        }
        public List<JewelryType> GetTypes()
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Types
                        select m).ToList();
            }
        }
        public JewelryType GetType(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Types
                        where(m.Id==id)
                        select m).FirstOrDefault();
            }
        }
        public int gettypeid(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Types
                        where (m.Id == id)
                        select m.Id).FirstOrDefault();
            }
        }
        public int getColoreid(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Colors
                        where (m.Id == id)
                        select m.Id).FirstOrDefault();
            }
        }
        public int getcategotyid(int id)
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Categories
                        where (m.Id == id)
                        select m.Id).FirstOrDefault();
            }
        }
        public List<JewelryColor> GetColors()
        {
            Context con = new Context();
            using (con)
            {
                return (from m in con.Colors select m).ToList();
            }
        }
        public void Add(Jewelry jewelry)
        {
            Context con = new Context();
            using (con)
            {
                con.Entry(jewelry.Category).State= EntityState.Unchanged;
                con.Entry(jewelry.Color).State = EntityState.Unchanged;
                con.Entry(jewelry.Type).State = EntityState.Unchanged;
                con.Jewelries.Add(jewelry);
                con.SaveChanges();
            }
        }
      
        public void update(Jewelry jewelry)
        {
            Context con = new Context();
            Jewelry j = con.Jewelries.Find(jewelry.Id);
            if(j!=null)
            {
                j.Description = jewelry.Description;
                j.Name = jewelry.Name;
                j.Price = jewelry.Price;
                j.ReleaseDate = jewelry.ReleaseDate;
                j.Type = new JewelryType { Id = jewelry.Type.Id };
                j.Color = new JewelryColor { Id = jewelry.Color.Id };
                j.Category = new JewelryCategory { Id = jewelry.Category.Id };
                con.Entry(j.Color).State = EntityState.Unchanged;
                con.Entry(j.Category).State = EntityState.Unchanged;
                con.Entry(j.Type).State = EntityState.Unchanged;
                con.Entry(j).State = EntityState.Modified;
                con.SaveChanges();
            }
        }
        public void Remove(Jewelry jewelry)
        {
            Context con = new Context();
            using (con)
            {

                con.Jewelries.Remove(jewelry);
                con.SaveChanges();
            }
        }


    }
}
