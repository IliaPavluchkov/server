using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SQL
{
    class TableManipulations
    {

        public List<string> makeTable(List<string> str,int sizeMess)
        {
            int i = 0, counter = 0, x = 0, y = 1, lengh;
            string clearStr = "";
            string buff;
            while (true)
            {
                buff = str[x].ToString();
                buff += "*";
                lengh = buff.Length;
                while (true)
                {

                    if (buff[i + 1] == '*')
                    {

                    }
                    if (buff[i] == '|')
                    {

                        clearStr += buff[i];
                        y++;
                    }
                    else
                    {
                        if (buff[i + 1] == '*')
                        {
                            for (; counter < 26; counter++)
                            {
                                clearStr += ' ';
                            }
                            counter = 0;
                            break;
                        }
                        else
                        if (buff[i + 1] == '|')
                        {
                            if (y == 1)
                            {
                                clearStr += buff[i];
                                counter = 0;
                                i++;
                                continue;
                            }
                            clearStr += buff[i];
                            for (; counter < 26; counter++)
                            {
                                clearStr += ' ';
                            }
                            counter = 0;
                        }
                        else
                        {
                            clearStr += buff[i];
                            counter++;
                        }
                    }


                    i++;
                }
                i = 0;
                counter = 0;
                y = 1;
                buff = "";
                str[x] = clearStr;
                clearStr = "";
                if (x == sizeMess - 1)
                    break;
                x++;
            }
            return str;
        }

        public string[] makeStrForExpeert(int scaleSize,int sizeMart)
        {
            string[] matr = new string[sizeMart];
            for(int i = 0; i < sizeMart; i++)
            {
                for (int j = 0; j < sizeMart; j++)
                {
                    if (i == j && j==sizeMart-1)
                    {
                        matr[i] += "-\n";
                        break;
                    }
                    else
                    if (i == j)
                    {
                        matr[i] += "-|";
                        continue;
                    }
                    else
                    if (j == sizeMart - 1)
                    {
                        matr[i] += "1/" + scaleSize + "\n";
                        break;
                    }
                    matr[i] += "1/" + scaleSize + "|";
                }
                Console.WriteLine(matr[i]);
            }
            return matr;
        }

        public void sort(TcpClient tcpClient)
        {
            Posts post = new Posts(tcpClient);
            SQLclass sql = new SQLclass();

            List<string> str2=sql.getLoginsAndId();
            List<string> ready = new List<string>();
            List<string> name = new List<string>();
            int i = 0;

            while (i != str2.Count())
            {
                string[] words = str2[i].Split('|');
                name.Add(words[1]);
                i++;
            }
            string mes;
            name.Sort();
            Thread.Sleep(50);
            post.sendMes(str2.Count().ToString());
            Thread.Sleep(40);
            post.sendMes("|id|Логин                    |Пароль                   |ФИО                      |Зарплата                 |Должность                |Телефон                  |\n");
            for (i = 0; i < str2.Count(); i++)
            {
                Thread.Sleep(40);
                mes= sql.getInfoUserWhereLogin(name[i]) + "|" + sql.getInfoUser(sql.getInfoUserWhereLogin(name[i]))[0];
                ready.Add(mes);
                post.sendMes(this.makeTable(ready, 1)[0]+"\n");
                ready.Clear();

            }
        }
    }
}
