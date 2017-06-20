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
        public string userName;

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
            try
            {
                byte[] message = Encoding.ASCII.GetBytes(Message);
                stream.Write(message, 0, message.Count());
            }
            catch
            {
                DisplayUserDisconnected();
            }
        }

        public string Recieve(Queue<string> storedMessages)
        {
            string recievedMessageString= "";
            try
            {
                byte[] recievedMessage = new byte[256];              
                stream.Read(recievedMessage, 0, recievedMessage.Length);
                recievedMessageString = Encoding.ASCII.GetString(recievedMessage).Replace("/0",string.Empty);
                storedMessages.Enqueue(recievedMessageString);
                Console.WriteLine(recievedMessageString);                
            }
            catch
            {
                DisplayUserDisconnected();               
            }
            return recievedMessageString;
        }

        public void DisplayUserDisconnected()
        {
            Console.WriteLine(Client.Client.userName + " has left the room.");
        }
    }
}
