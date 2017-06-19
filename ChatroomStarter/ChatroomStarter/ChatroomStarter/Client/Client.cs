using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Client
    {
        TcpClient clientSocket;
        NetworkStream stream;
        public string userName;
        public string messageString;
        public bool runUserMsg = true;

        public Client(string IP, int port)
        {
            userName = UI.GetUserName();
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse(IP), port);
            stream = clientSocket.GetStream();
        }

        public void runClient()
        {
            while (runUserMsg == true)
            {
                if(UI.GetInput() == "/leave")
                {
                    runUserMsg = false;
                }
                if(userName != null)
                {
                    Client client = new Client("127.0.0.1", 9999);
                    client.SendUserName();
                    client.runClient();
                    Console.ReadLine();
                }
                Send();
                Recieve();
            }          
        }

        public void SendUserName()
        {
            byte[] messageUser = Encoding.ASCII.GetBytes(userName);
            stream.Write(messageUser, 0, messageUser.Count());
        }

        public string Send()
        {
            string messageString = UI.GetInput();
            byte[] message = Encoding.ASCII.GetBytes(messageString);
            stream.Write(message, 0, message.Count());
            return messageString;
        }
        public void Recieve()
        {
            byte[] recievedMessage = new byte[256];
            stream.Read(recievedMessage, 0, recievedMessage.Length);
            UI.DisplayMessage(Encoding.ASCII.GetString(recievedMessage));
        }
    }
}


