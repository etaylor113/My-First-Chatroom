using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class UI
    {       
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        public static string GetInput()
        {
            Console.WriteLine("Type a message:");
            string input = Console.ReadLine();                       
                return input;                      
        }

        public static string GetUserName()
        {
            Console.WriteLine("Please enter your username:");
            string userName = Console.ReadLine();
            Console.Clear();
            return userName;
        }        
    }
}
