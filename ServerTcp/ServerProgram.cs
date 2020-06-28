using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region TCP
            //// задать адрес приложения Ip адрес и порт
            //const string ip = "127.0.0.1";
            //const int port = 8080;

            //var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //tcpSocket.Bind(tcpEndPoint); // связываем endPoint с Socket, мы говорим нашему socket, что необходимо слушать
            //tcpSocket.Listen(5);

            //while (true) // клиент пришел, создали листенера, данные обработали, отправили ответ и уничтожили. Далее обрабатываем следующего
            //{
            //    // обработчик на приём сообщения
            //    var listener = tcpSocket.Accept(); // создаётся новый сокет для подключения клиента
            //    var buffer = new byte[256]; // хранилище данных
            //    var sizeData = 0; // переменная в которую будем записывать реальное кол-во байт
            //    var data = new StringBuilder();

            //    do // проверяем условия что мы получили запрос
            //    {
            //        sizeData = listener.Receive(buffer);
            //        data.Append(Encoding.UTF8.GetString(buffer, 0, sizeData)); // кодируем и отправляем данные
            //    }
            //    while (listener.Available > 0);

            //    Console.WriteLine(data);

            //    listener.Send(Encoding.UTF8.GetBytes("Успех")); // принимаем и раскодируем данные

            //    listener.Shutdown(SocketShutdown.Both); // Отключаем подключение, двухсторонние отключение - закрываем у клиента и у сервера
            //    listener.Close(); // закрываем подключение

            //}
            #endregion

            Console.WriteLine("Сервер запущен...");
            const string ip = "127.0.0.1";
            const int port = 8081;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            udpSocket.Bind(udpEndPoint);

            while (true)
            {
                var buffer = new byte[256]; // хранилище данных
                var sizeData = 0; // переменная в которую будем записывать реальное кол-во байт
                var data = new StringBuilder();

                EndPoint senderUdpEndPoint = new IPEndPoint(IPAddress.Any, 0); // экземпляр адреса в который будем записывать данные (сохранить данные подключения, адрес клиента)

                do
                {
                    sizeData = udpSocket.ReceiveFrom(buffer, ref senderUdpEndPoint); // через реферальный аргумент передаём наш sender
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);

                udpSocket.SendTo(Encoding.UTF8.GetBytes("Сообщение получено"), senderUdpEndPoint);

                Console.WriteLine($"Сообщение получено: {data}");
            }

            //udpSocket.Shutdown(SocketShutdown.Both);
            //udpSocket.Close();
        }
    }
}
