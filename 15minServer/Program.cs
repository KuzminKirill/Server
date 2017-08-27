using System;
using System.Net.Sockets;
using System.Threading;

namespace _15minServer
{
    class Server
    {
        TcpListener Listener; // Объект, принимающий TCP-клиентов
        bool _flag = true;
        private Thread _action;

        //пустой конструктор
        public Server(int Port)
        {
            try
            {
                // Создаем "слушателя" для указанного порта
                Listener = new TcpListener(Port);
                Listener.Start();
            }
            catch (SocketException e)
            {
                throw e;
            }
        }

        ~Server()
        {
            Stop();
        }

        static void ClientThread(Object StateInfo)
        {
            // Просто создаем новый экземпляр класса DataProcessing и передаем ему приведенный к классу TcpClient объект StateInfo
            new DataProcessing((TcpClient)StateInfo);
        }

       

        public void Start(int Port)
        {
            try
            {
               if ((_action != null) && (!_action.IsAlive))
                    return;
               else _action = new Thread(Work);

                _action.Start();
            }

            catch (ThreadAbortException error)
            {
                Console.WriteLine("ThreadAborExeption: {0}", error);
            }
        }

        public void Stop()
        {
            _flag = false;
            if (_action != null)
                _action.Abort();
        }


        public void Work()
        {
            while (_flag)
            {
                // Принимаем нового клиента
                TcpClient Client = Listener.AcceptTcpClient();
                Thread Thread = new Thread(new ParameterizedThreadStart(ClientThread));
                Thread.Start(Client);
            }
            Stop();
        }

        static void Main(string[] args)
        {
            int port = 4556;
           Server serv =  new Server(port);
            serv.Start(port);
        }
    }
}
