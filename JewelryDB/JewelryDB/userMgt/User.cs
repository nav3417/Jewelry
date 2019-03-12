using JewelryDB.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryDB.userMgt
{
   public class User
    {
        public int Id { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public virtual City City { get; set; }

        public string Email { get; set;}

        public string ImageUrl { get; set; }

        public Nullable<DateTime> BirthDate { get; set; }

        public string FullName { get; set; }

        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public virtual Role Role { get; set; }
        public int Amount { get; set; }

        public bool IsInRole(int id)
        {
            return Role != null && Role.Id == id;
        }
        //List<Comment> Comments { get; set;}
        //public User()
        //{
        //    Comments = new List<Comment>();
        //}

    }
}
