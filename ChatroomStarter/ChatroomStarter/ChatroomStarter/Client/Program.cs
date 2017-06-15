using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool runUserMsg = true;
            while (runUserMsg == true)
            {              
                    Client client = new Client("127.0.0.1", 9999);
                    client.Send();
                    client.Recieve();
                    Console.ReadLine();
                
            }
        }
    }
}
