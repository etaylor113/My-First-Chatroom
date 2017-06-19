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
        public string userId;
        public string UserName;    
        
 
        public ServerClient(NetworkStream Stream, TcpClient Client)
        {           
            userId = Guid.NewGuid().ToString();               
            stream = Stream;
            client = Client;        
        }

        //public void ReceiveUserName()
        //{
        //    byte[] recievedMessage = new byte[256];
        //    stream.Read(recievedMessage, 0, recievedMessage.Length);
        //    string UserName = Recieve();           
        //}
        public void Send(string Message)
        {
           byte [] message = Encoding.ASCII.GetBytes(Message);
           stream.Write(message, 0, message.Count());
        }

        public string Recieve(Queue<string> storedMessages)
        {
            try
            {
                byte[] recievedMessage = new byte[256];              
                stream.Read(recievedMessage, 0, recievedMessage.Length);
                string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);
                storedMessages.Enqueue(recievedMessageString);
                Console.WriteLine(recievedMessageString);
                return recievedMessageString;
            }
            catch
            {
                Console.WriteLine("A user has left the room.");
                Environment.Exit(0);
                return Console.ReadLine();
            }
        }

    }
}
