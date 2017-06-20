using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace Server
{
    public class ServerClient
    {
        NetworkStream stream;
        TcpClient client;       
        public string userId;
        public string userName;
        public Dictionary<string, string> users = new Dictionary<string, string>
        {

        };
        public ServerClient(NetworkStream Stream, TcpClient Client)
        {           
            userId = Guid.NewGuid().ToString();               
            stream = Stream;
            client = Client;               
        }
      

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

        public string RecieveUserName()
        {          
            byte[] recievedName = new byte[150];
            stream.Read(recievedName, 0, recievedName.Length);
            userName = Encoding.ASCII.GetString(recievedName).Replace("\0", string.Empty);
            return userName;
        }

        public string Recieve(Queue<string> storedMessages)
        {
            string recievedMessageString = "";
            try
            {
                byte[] recievedMessage = new byte[150];              
                stream.Read(recievedMessage, 0, recievedMessage.Length);
                recievedMessageString = Encoding.ASCII.GetString(recievedMessage).Replace("\0",string.Empty);
                storedMessages.Enqueue(recievedMessageString);
                Console.WriteLine(userName + ": " + recievedMessageString);                
            }
            catch
            {
                users.Remove(userName);
                DisplayUserDisconnected();
            }
            return recievedMessageString;
        }

        public void DisplayUserDisconnected()
        {            
            Console.WriteLine(userName + " has left the room.");
        }

        public void TestServerUsers()
        {
            if (users.Count == 0)
            {
                Console.WriteLine("No users in chatroom. Server shutting down.");
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }

        public void AddDictionary()
        {
            users.Add(userName, userId);
        }

        public bool CheckDictionary()
        {
            bool userCheck = false;
            foreach (KeyValuePair<string, string> name in users)
            {
                if (users.ContainsKey(userName))
                {
                    userCheck = true;
                }
                else
                {
                    userCheck = false;
                }
            }
            return userCheck;
        }
    }
}
