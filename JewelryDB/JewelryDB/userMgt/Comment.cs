using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryDB.userMgt
{
  public  class Comment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cellno { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public virtual User user { get; set; }
    }
}
