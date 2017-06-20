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
            bool serverRunning = true;
            CheckDictionary();
            AcceptClient();
            client.RecieveUserName();
            DisplayUserConnection();
            AddDictionary();
            while (serverRunning == true)
            {
                TestServerUsers();
                message = client.Recieve(storedMessages);
                Respond(message);              
            }
        }

        public void TestServerUsers()
        {
            if (users.Count == 0)
            {
                serverRunning = false;
                Console.WriteLine("No users in chatroom. Server shutting down.");
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }

        public void AddDictionary()
        {
            users.Add(client.userName, client.userId);
        }

        public void DisplayUserConnection()
        {
            Console.Write("Connected: " + client.userName + "\n");
        }

        public void DisplayUserExit()
        {         
            users.Remove(client.userName);
            if (!users.ContainsKey(client.userName))
            {
                Console.Write("Disconnected: " + client.userName + "\n");
            }
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
                if (users.ContainsKey(client.userName))
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
