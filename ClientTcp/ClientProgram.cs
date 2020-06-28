using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region TCP
            //const string ip = "127.0.0.1";
            //const int port = 8080;

            //var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //Console.Write("Введите сообщение: ");
            //var message = Console.ReadLine(); // ввели сообщение

            //var data = Encoding.UTF8.GetBytes(message); // получили и закодировали данные

            //tcpSocket.Connect(tcpEndPoint); // открыть сокет, сделать подключение для этого сокета
            //tcpSocket.Send(data); // Отправляем наш массив байт

            //var buffer = new byte[256]; // Получаем ответ
            //var sizeData = 0; 
            //var answer = new StringBuilder(); // Ответ сервера

            //do 
            //{
            //    sizeData = tcpSocket.Receive(buffer);
            //    answer.Append(Encoding.UTF8.GetString(buffer, 0, sizeData));
            //}
            //while (tcpSocket.Available > 0); // получаем сообщение, раскодировали

            //Console.WriteLine(answer.ToString());

            //tcpSocket.Shutdown(SocketShutdown.Both);
            //tcpSocket.Close();

            //Console.ReadLine();
            #endregion

            const string ip = "127.0.0.1";
            const int port = 8082;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);

            while(true)
            {
                Console.Write("Введите сообщение: ");
                var message = Console.ReadLine();

                var serverUdpEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8081);
                udpSocket.SendTo(Encoding.UTF8.GetBytes(message), serverUdpEndPoint);

                var buffer = new byte[256];
                var sizeData = 0; 
                var data = new StringBuilder();

                EndPoint senderUdpEndPoint = new IPEndPoint(IPAddress.Parse(ip), 8081); // экземпляр адреса в который будем записывать данные (сохранить данные подключения, адрес клиента)

                do
                {
                    sizeData = udpSocket.ReceiveFrom(buffer, ref senderUdpEndPoint); // через реферальный аргумент передаём наш sender
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);

                Console.WriteLine(data);
            }
        }
    }
}
