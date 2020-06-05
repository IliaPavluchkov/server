using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Numerics;
using System.Collections.Specialized;

namespace SQL
{
    class SQLclass
    {
        private static string connectionStr = "server=localhost;user=root;database=peple;password=Ip25102000";
        private static MySqlConnection conn = new MySqlConnection(connectionStr);

        public void startSQL()
        {
            conn.Open();

        }
        public void closeSQl()
        {
            conn.Close();
        }
        public List<string> requestLogPasSQL()
        {
            string request = "SELECT Логин, Пароль From workers";

            MySqlCommand command = new MySqlCommand(request, conn);
            MySqlDataReader readrer = command.ExecuteReader();

            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();

            while (readrer.Read())
            {
                str.Add(readrer[0].ToString() + " " + readrer[1].ToString());
                //Console.WriteLine( readrer[0].ToString()+" "+ readrer[1].ToString());
            }
            readrer.Close();
            return str;
        }
        public List<string> getInfoUser(string idUser)
        {
            string request = "SELECT Логин, Пароль, Фио, ЗП, Должность, Телефон From workers WHERE id = " + idUser;

            MySqlCommand command = new MySqlCommand(request, conn);
            MySqlDataReader readrer = command.ExecuteReader();

            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();

            while (readrer.Read())
            {
                str.Add(readrer[0].ToString() + "|" + readrer[1].ToString() + "|" + readrer[2].ToString() + "|" + readrer[3].ToString() + "|" + readrer[4].ToString() + "| " + readrer[5].ToString());
                
            }
            readrer.Close();
            
            return str;
        }

        public List<string> getAllInfoUser()
        {
            string request = "SELECT id , Логин, Пароль, Фио, ЗП, Должность, Телефон From workers";

            MySqlCommand command = new MySqlCommand(request, conn);
            MySqlDataReader readrer = command.ExecuteReader();

            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();

            while (readrer.Read())
            {
                str.Add(readrer[0].ToString() + "|" + readrer[1].ToString() + "|" + readrer[2].ToString() + "|" + readrer[3].ToString() + "|" + readrer[4].ToString() + "| " + readrer[5].ToString() + "| " + readrer[6].ToString());

            }
            readrer.Close();

            return str;
        }

        public string getInfoUserWhereLogin(string login)
        {
            string request = "SELECT id From workers WHERE Логин = '"+login+"'";

            MySqlCommand command = new MySqlCommand(request, conn);
            MySqlDataReader readrer = command.ExecuteReader();

            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();

            while (readrer.Read())
            {
                str.Add(readrer[0].ToString());
            }
            readrer.Close();
            return str[0];
        }

        public void sendToSQL(string message)
        {
            string request = "INSERT INTO workers (id, Логин, Пароль, Фио, ЗП, Должность, Телефон) VALUES (" + message+")";
            MySqlCommand command = new MySqlCommand(request, conn);
            command.ExecuteNonQuery();
        }

        public string getLastId()
        {
            string id;
            string request = "SELECT id From workers";

            MySqlCommand command = new MySqlCommand(request, conn);
            MySqlDataReader readrer = command.ExecuteReader();

            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();

            while (readrer.Read())
            {
                str.Add(readrer[0].ToString());
                //Console.WriteLine( readrer[0].ToString()+" "+ readrer[1].ToString());
            }
            readrer.Close();
            id = str[str.Count - 1];
            return id;
        }

        public List<string> getLoginsAndId()
        {
            string request = "SELECT id , Логин From workers";

            MySqlCommand command = new MySqlCommand(request, conn);
            MySqlDataReader readrer = command.ExecuteReader();

            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();

            while (readrer.Read())
            {
                str.Add(readrer[0].ToString() + "|" + readrer[1].ToString());

            }
            readrer.Close();

            return str;
        }

        public void deleteUser(string id)
        {
            string request = "DELETE FROM workers WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(request, conn);
            command.ExecuteNonQuery();
        }

        public List<string> returnAllId()
        {
            string request = "SELECT id From workers";

            MySqlCommand command = new MySqlCommand(request, conn);
            MySqlDataReader readrer = command.ExecuteReader();

            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();

            while (readrer.Read())
            {
                str.Add(readrer[0].ToString());

            }
            readrer.Close();

            return str;
        }

        public void redactionInfoUser(string message,string id)
        {
            string request = "UPDATE workers Set "+message+" WHERE id = "+id;
            MySqlCommand command = new MySqlCommand(request, conn);
            command.ExecuteNonQuery();
        }

        public List<string> getUserInformationAboutYourself(string id)
        {
            string request = "SELECT information From infotable WHERE id = "+id;

            MySqlCommand command = new MySqlCommand(request, conn);
            MySqlDataReader readrer = command.ExecuteReader();

            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();
        
            while (readrer.Read())
            {
                str.Add(readrer[0].ToString());

            }
            readrer.Close();
            if (str.Count() == 0)
            {
                str.Add("\n");
            }
            return str;
        }

        public void updateInformationAboutYourself(string message, string id)
        {
            string buff= "information ='"+message+"'";
            string request = "UPDATE infotable Set " + buff + " WHERE id = " + id;
            MySqlCommand command = new MySqlCommand(request, conn);
            command.ExecuteNonQuery();
        }
        public void setUserInformationAboutYourself(string message)
        {
            string request = "INSERT INTO infotable (id, information) VALUES (" + message + ")";
            MySqlCommand command = new MySqlCommand(request, conn);
            command.ExecuteNonQuery();
        }
        public int returnSizeBD()
        {
            string request = "SELECT id From workers";

            MySqlCommand command = new MySqlCommand(request, conn);
            MySqlDataReader readrer = command.ExecuteReader();

            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();

            while (readrer.Read())
            {
                str.Add(readrer[0].ToString());
            }
            readrer.Close();
            return str.Count;
        }

        public List<string> getFinance()
        {
            string request = "SELECT money From finans";

            MySqlCommand command = new MySqlCommand(request, conn);
            MySqlDataReader readrer = command.ExecuteReader();

            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();

            while (readrer.Read())
            {
                str.Add(readrer[0].ToString());
            }
            readrer.Close();
            return str;
        }

        public void updateFinance(string month,string money)
        {
            money = "money = '" + money + "'";
            string request = "UPDATE finans Set " + money + " WHERE id = " + month;
            MySqlCommand command = new MySqlCommand(request, conn);
            command.ExecuteNonQuery();
        }

    }
}
