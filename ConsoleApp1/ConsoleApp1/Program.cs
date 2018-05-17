using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        private static int HR;

        private static Object LCK;

        private static bool stop;

        static void Main(string[] args)
        {
            HR = 0;
            LCK = new Object();
            stop = false;

            Thread readConsole = new Thread(Console);
            Thread sendUDP = new Thread(UDP);

            readConsole.Start();
            sendUDP.Start();

            readConsole.Join();
            sendUDP.Join();
        }

        private static void Console()
        {
            int a = 0;
            while(!stop)
            {
                try
                {
                    System.Console.WriteLine("Insert a new value for the heart rate: ");
                    a = int.Parse(System.Console.ReadLine());

                    lock (LCK)
                    {
                        HR = a;
                    }
                }
                catch (Exception e)
                {
                    stop = true;
                }
            }
        }

        private static void UDP()
        {
            UdpClient u = new UdpClient(6000);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11111);
            int a = 0;
            while(!stop)
            {
                lock (LCK)
                {
                    a = HR;
                }
                byte[] msg = Encoding.ASCII.GetBytes(a.ToString());

                u.Send(msg, msg.Length, ipEndPoint);

                Thread.Sleep(50);
            }
        }
    }
}
