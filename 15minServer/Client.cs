using System.IO;
using System.Net.Sockets;

namespace _15minServer
{
    class DataProcessing
    {
        private string path = @"E:\\FileWriter.txt";
        private readonly IParser _parser;

        public DataProcessing(TcpClient Client)
        {
            _parser = new Utf8Parser();
            var data = new byte[1024];
            // Читаем из потока клиента до тех пор, пока от него поступают данные
            while (Client.GetStream().Read(data, 0, data.Length) != 0)
            {
                var request = _parser.Parse(data);

                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.Write("");
                    }
                }
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(request);
                }

            }
        }
    }
}
