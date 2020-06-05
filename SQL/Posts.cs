using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.ComponentModel;
using System.Threading;
namespace SQL
{
    class Posts: ServerConfig
    {
        public TcpClient client;
        private NetworkStream stream = null;
        public Posts(TcpClient tcpClient)
        {
            client = tcpClient;
            stream = client.GetStream();
        }
        ~Posts()
        {
            if (stream != null)
                stream.Close();
            Console.WriteLine("Поток закрыт");
        }
        public void sendMes(string message)
        {
            byte[] data = new byte[64];
            message = message.Substring(message.IndexOf(':') + 1);
            data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        public string recvMes()
        {
            byte[] data = new byte[64];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            int curr;
            Thread.Sleep(75);
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (stream.DataAvailable);
            string message = builder.ToString();
            return message;
        }
        public bool thisIdDontUse(int id)
        {
            int i = 0;
            int size = DataBanck.listUser.Count;
            while (i != size)
            {
                if (DataBanck.listUser[i] == id)
                {
                    return false;
                }
            }
            return true;
        }

        public TcpClient returnTcp()
        {
            return client;
        }
        public void deliteLocalId(int localId )
        {
            List<int> buf = new List<int>();
            int i = 0;
            int size = DataBanck.listUser.Count;
            if (size == 0) return;
            size--;
            while (i!=size)
            {
                if (DataBanck.listUser[i] != localId) { buf.Add(DataBanck.listUser[i]); }
            }
            DataBanck.listUser = buf;
        }
        public void clearStream()
        {
            stream.Flush();
        }
    }
}
