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
                new Server().Run();
                Console.ReadLine();            
        }
    }
}


//              ---The SOLID design principles I used are as followed ---
// Single Responsibility Principle - My methods are very short do only one specific task.
// Open/ Closed Principle - I built out my program in a way that allows me to easily add new features.
