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
        

        public Server()
        {
            server = new TcpListener(IPAddress.Parse("127.0.0.1"), 9999);
            server.Start();
        }

        public void Run()
        {
            string message;
            bool serverRunning = true;

            
            AcceptClient();
            client.RecieveUserName();
            DisplayUserConnection();
            client.AddDictionary();
            client.CheckDictionary();
            while (serverRunning == true)
            {                
                message = client.Recieve(storedMessages);
                client.TestServerUsers();
                Respond(message);              
            }
        }
     
        public void DisplayUserConnection()
        {
            Console.Write("Connected: " + client.userName + "\n");
        }

        public void DisplayUserExit()
        {         
            client.users.Remove(client.userName);
            if (!client.users.ContainsKey(client.userName))
            {
                Console.Write(client.userName + " has disconnected.\n");
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
            try
            {
                client.Send(body);
                storedMessages.Dequeue();
            }
            catch
            {
                client.TestServerUsers();
            }
                     
        }

    }
}
