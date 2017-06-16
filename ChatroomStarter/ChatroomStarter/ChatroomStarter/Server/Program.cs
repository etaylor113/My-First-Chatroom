using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            bool serverBool = true;
            do
            {
                new Server().Run();
                Console.ReadLine();
            } while (serverBool == true);
        }
    }
}
