using JewelryDB.Jewlry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Context con = new Context();
            Console.WriteLine("Id\t Name");
            foreach (var i in con.statuses)
            {
                Console.WriteLine(i.Id+"\t"+i.Name);
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}