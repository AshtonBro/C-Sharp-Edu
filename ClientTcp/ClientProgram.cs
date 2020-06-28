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
            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpSocket.Bind(tcpEndPoint);
            tcpSocket.Listen(5);

            Console.Write("Введите сообщение: ");
            var message = Console.ReadLine(); // ввели сообщение

            var data = Encoding.UTF8.GetBytes(message); // получили и закодировали данные
            tcpSocket.Connect(tcpEndPoint); // открыть сокет, сделать подключение для этого сокета
            tcpSocket.Send(data); // Отправляем наш массив байт

            var buffer = new byte[256]; // Получаем ответ
            var sizeData = 0; 
            var answer = new StringBuilder(); // Ответ сервера

            do 
            {
                sizeData = tcpSocket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, sizeData));
            }
            while (tcpSocket.Available > 0);
        }
    }
}
