﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
   
    public class Server
    {
        public static ServerClient client;       
        TcpListener server;
        public string userName;
        public List<byte> nameList = new List<byte>();
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
            bool userLoop = true;
         
            while (userLoop == true)
            {
                AcceptClient();
                AddDictionary();
                DisplayUserConnection();
                message = client.Recieve();
                Respond(message);               
            }          
        }

        public void AddDictionary()
        {
            userName = client.Recieve();           
            users.Add(userName, client.UserId);
        }

        public void DisplayUserConnection()
        {
            Console.Write("Connected: " + userName + "\n");
        }      
      
        private void AcceptClient()
        {
            TcpClient clientSocket = default(TcpClient);
            clientSocket = server.AcceptTcpClient();                      
            Console.WriteLine("Connected");        
            NetworkStream stream = clientSocket.GetStream();
            client = new ServerClient(stream, clientSocket);            
        }
        private void Respond(string body)
        {
             client.Send(body);
        }
    }
}
