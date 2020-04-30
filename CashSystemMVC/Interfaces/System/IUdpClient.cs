using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CashSystemMVC.Interfaces.System
{
    public interface IUdpClient
    {
        void SendMessageToAtm(int port, string message);
    }

    public class UpdClient : IUdpClient
    {
        public void SendMessageToAtm(int port, string message)
        {
            var udpClient = new UdpClient(11000);
            try
            {
                udpClient.Connect("192.168.0.19", port);

                // Sends a message to the host to which you have connected.
                var sendBytes = Encoding.ASCII.GetBytes(message);

                udpClient.Send(sendBytes, sendBytes.Length);

                udpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}