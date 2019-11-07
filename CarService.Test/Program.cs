using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new Model1Container1();
            var clientUtil = new ClientUtils(ctx);
            var chestie = clientUtil.GetClientsList();
            Console.Write(chestie);
        }
    }
}
