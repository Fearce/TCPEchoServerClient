using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TCPEchoServer
{
    public class EchoService
    {
        private TcpClient connectionSocket;

        public EchoService(TcpClient connectionSocket)
        {
            this.connectionSocket = connectionSocket;

        }


        public void doIt(string message, string answer, StreamWriter sw, StreamReader sr)
        {
            try
            {
                while (message != null)
                {
                
                    if (message.ToUpper() == "STOP")
                    {
                        Console.WriteLine("Server stopped");
                        break;
                    }

                    if (message.ToUpper().Contains("HTML"))
                    {
                        Console.WriteLine(message);
                        string uri = message.Split(' ').Last();
                        var file = new StreamReader(@"C:\" + uri);
                        string line;
                        int count = 0;
                        while ((line = file.ReadLine()) != null)
                        {
                            sw.WriteLine(line);
                            count++;
                        }
                        file.Close();
                    }

                    else if (message.ToUpper().Contains("GET"))
                    {
                        Console.WriteLine(message);
                        //answer = message.Split(' ').Last();
                        answer = "HTTP/1.1 200 OK\r\nContent-Type: text/html\r\nConnection: close\r\nHello Client!";
                        sw.WriteLine(answer);
                        message = sr.ReadLine();
                    }

                    else
                    {
                        Console.WriteLine(message);
                        answer = message.ToUpper();
                        sw.WriteLine(answer);
                        message = sr.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Connection closed by client");
                TCPEchoServer1.ServerStopped = true;
            }
        }

    }
}
