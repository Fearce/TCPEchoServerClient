using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPEchoClient
{
    class TCPClient1
    {
        private string ClientName;

        public async void Main()
        {
           
           Setup();

        }

        public TCPClient1(string clientName)
        {
            ClientName = clientName;
        }

        public void Setup()
        {
            //Console.ReadLine();
            TcpClient clientSocket = new TcpClient("127.0.0.1", 6789);
            Console.WriteLine(ClientName + " ready");

            Stream ns = clientSocket.GetStream();  //provides a Stream
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing
            while (true)
            {
                Msg(sr,sw);
            }

            //Stop(ns, clientSocket);

        }

        public void Msg(StreamReader sr, StreamWriter sw)
        {
            string message = Console.ReadLine();
            sw.WriteLine(ClientName + " : " + message);
            string serverAnswer = sr.ReadLine();

            Console.WriteLine("Response: " + serverAnswer);


        }

        public void Stop(Stream ns, TcpClient clientSocket)
        {
            Console.WriteLine("No more from server. Press Enter");
            Console.ReadLine();

            ns.Close();

            clientSocket.Close();
        }

    }
}
