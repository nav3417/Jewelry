using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JewelryUI.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItems> Items { get; set; }
        public ShoppingCart()
        {
            Items =new List<ShoppingCartItems>();
        }
     public void Add(ShoppingCartItems newItems)
        {
            ShoppingCartItems foundItems = Items.Find(i => i.Id == newItems.Id);
            if(foundItems==null)
            {
                Items.Add(newItems);
                Items.TrimExcess();
            }
            else
            {
                foundItems.Quantity += newItems.Quantity;
            }
        }
        public void Remove(int id)
        {
            Items.RemoveAt(Items.FindIndex(i => i.Id==id));
        }

        public int NumberOfItems
        {
            get {
               return Items.Count();
            }
        }
    }
}