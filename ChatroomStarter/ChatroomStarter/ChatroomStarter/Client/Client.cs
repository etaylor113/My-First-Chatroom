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
        static public string userName;
        public string messageString;
        public bool runUserMsg = true;

        public Client(string IP, int port)
        {
            
            clientSocket = new TcpClient();
            clientSocket.Connect(IPAddress.Parse(IP), port);
            stream = clientSocket.GetStream();
        }

        public void RunClient()
        {
            runUserMsg = true;
            
            while (runUserMsg == true)
            {
                messageString = Send();
                if (messageString == "/exit")
                {
                    Exit();
                }
                else if (messageString != "/exit")
                {
                    Recieve();
                }                
            }
        }

        public string SendUserName()
        {
            userName = UI.GetUserName();
            byte[] messageUser = Encoding.ASCII.GetBytes(userName);
            stream.Write(messageUser, 0, messageUser.Count());
            return userName;
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

        private void Exit()
        {
            string exitingUser = userName;

            runUserMsg = false;
        }
    }
}


