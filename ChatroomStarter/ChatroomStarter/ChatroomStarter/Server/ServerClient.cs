using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerClient
    {
        NetworkStream stream;
        TcpClient client;       
        public string UserId;
        public string UserName;    
        
 
        public ServerClient(NetworkStream Stream, TcpClient Client)
        {           
            UserId = Guid.NewGuid().ToString();               
            stream = Stream;
            client = Client;        
        }

        public void ReceiveUserName()
        {
            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            string UserName = Recieve();           
        }
        public void Send(string Message)
        {
           
        }

        public string Recieve()
        {
            try
            {
                byte[] recievedMessage = new byte[256];              
                stream.Read(recievedMessage, 0, recievedMessage.Length);
                string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);
                Console.WriteLine(recievedMessageString);
                return recievedMessageString;
            }
            catch
            {
                Console.WriteLine("A user has left the room.");
                Console.ReadLine();
                Environment.Exit(0);
                return Console.ReadLine();
            }
        }

    }
}
