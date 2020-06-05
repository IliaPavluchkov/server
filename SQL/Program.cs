using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Net.Sockets;
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace SQL
{
    class Program
    {
        private const int port = 7770;
        static void Main(string[] args)
        {
            DataBanck.whatDo = 0;
            //Подключаем SQL бд
            SQLclass objSQL = new SQLclass();
            objSQL.startSQL();
            //стартуем подклчение
            ServerConfig server = new ServerConfig();
            server.startServer();
            objSQL.closeSQl();
            //___________________________________________________________________________________________________________--
            return;
        }
    }
}
