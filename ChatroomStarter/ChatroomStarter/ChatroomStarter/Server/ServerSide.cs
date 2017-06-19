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
   
    public class Server
    {
        public static ServerClient client;       
        TcpListener server;
        public string userName;
        public bool userCheck;
        public bool serverRunning;

        public List<byte> nameList = new List<byte>();
        public Queue<string> storedMessages = new Queue<string>();
        public Dictionary<string, string> users = new Dictionary<string, string>
        {

        };
       
        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }     

        public void Run()
        {
            string message;                          
            while (serverRunning == true)
            {
                if (userCheck == false)
                {
                    AcceptClient();
                    AddDictionary();
                }                
                DisplayUserConnection();
                message = client.Recieve(storedMessages);
                Respond(message);
                TestServerUsers();
            }          
        }
        public void TestServerUsers()
        {
            if (users.Count == 0)
            {
                serverRunning = false;
            }
        }

        public void AddDictionary()
        {
            userName = client.Recieve(storedMessages);          
            users.Add(userName, client.userId);
        }

        public void DisplayUserConnection()
        {
            Console.Write("Connected: " + userName + "\n");
        }      
      
        private void AcceptClient()
        {
            TcpClient clientSocket = default(TcpClient);           
            clientSocket = server.AcceptTcpClient();                      
            Console.WriteLine(" ");        
            NetworkStream stream = clientSocket.GetStream();
            client = new ServerClient(stream, clientSocket);            
        }
        private void Respond(string body)
        {
             client.Send(body);
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
