using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
            Task.Run((() => //Printer response på en seperat tråd så konsollen stadig er klar til Msg();
            {
                while (true)
                {
                    PrintResponse(sr, sw);
                }
            }));
            while (true)
            {
                Msg(sr, sw, clientSocket);
            }
            //Stop(ns, clientSocket);

        }

        public async void PrintResponse(StreamReader sr, StreamWriter sw)
        {
            string serverAnswer = sr.ReadLine();
            Console.WriteLine("Response: " + serverAnswer);
        }

        public void Msg(StreamReader sr, StreamWriter sw, TcpClient client)
        {
            string message = Console.ReadLine(); //Sends original message
            sw.WriteLine(ClientName + " : " + message); //Writes message to server

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
