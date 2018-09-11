using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPEchoServer
{
    class TCPEchoServer1
    {
        public TcpClient connectionSocket;

        public static bool ServerStopped;

        public void Main()
        {
            // IPAddress ip = new IPAddress("127.0.0.1");

            IPAddress ip = IPAddress.Parse("127.0.0.1");


            TcpListener serverSocket = new TcpListener(ip, 6789); //Only opens for 1 port (can only listen to one at a time?)
            //Alternatively but deprecated
            //TcpListener serverSocket = new TcpListener(6789);


            serverSocket.Start();
           

            while (true)
            {
                try
                {
                    if (ServerStopped)
                    {
                        ServerStopped = false;
                        Console.WriteLine("Client shut down, restarting");
                        serverSocket.Stop();
                        Main();
                    }
                    Console.WriteLine("Server started");

                    TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                    Task.Run(() =>
                    {
                        //Socket connectionSocket = serverSocket.AcceptSocket();
                        Console.WriteLine("Server activated");

                        Stream ns = connectionSocket.GetStream();
                        // Stream ns = new NetworkStream(connectionSocket);

                        StreamReader sr = new StreamReader(ns);
                        StreamWriter sw = new StreamWriter(ns);
                        sw.AutoFlush = true; // enable automatic flushing
                        string message = sr.ReadLine();
                        string answer = "";
                        EchoService echoSvc = new EchoService(connectionSocket);
                        //Task.Factory.StartNew(echoSvc.doIt(message, answer, sw, sr));
                        echoSvc.doIt(message, answer, sw, sr);
                    });
                    
                }
                catch (Exception IOException)
                {
                    Console.WriteLine("Client shut down, restarting");
                    serverSocket.Stop();
                    Main();
                }

                
            }

        }
    }
}
