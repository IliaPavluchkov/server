using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace SQL
{
    class ClassUser
    {
        private string message;
        private int localId = -1;
        private TcpClient client;
        private Posts post;
        private SQLclass objSQL = new SQLclass();
        private ClassAdmin objAdm;

        public ClassUser(TcpClient tcpClient)
        {
            client = tcpClient;
            post = new Posts(client);
            objAdm = new ClassAdmin(client);
        }
        public void registration()
        {
            message = objSQL.getLastId();
            int id = int.Parse(message);
            id++;
            message = id.ToString();
            
            message += ", " + post.recvMes();
            objSQL.sendToSQL(message);
            message = "'" + id.ToString() + "' , ''";
            objSQL.setUserInformationAboutYourself(message);
            Console.WriteLine("Зарегистрирован новый пользователь");
        }
        public void userForm()
        {
            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();
            while (true)
            {
                Thread.Sleep(75);
                str = objSQL.getInfoUser(localId.ToString());
                post.sendMes(str[0]);
                Thread.Sleep(40);
                post.sendMes(objSQL.getUserInformationAboutYourself("0")[0]);
                Thread.Sleep(40);
                post.sendMes(objSQL.getUserInformationAboutYourself(localId.ToString())[0]);


                message = post.recvMes();
                if (message == "end" && localId != -1)
                {
                    post.deliteLocalId(localId);
                    localId = -1;
                    break;
                }
                else if (message == "redaction")
                {
                    //Редактирование от лица пользователя
                    str = objSQL.getInfoUser(localId.ToString());
                    post.sendMes(str[0]);
                    message = post.recvMes();
                    if (message == "end") { continue; }
                    else
                    {
                        string[] buffer = message.Split(',');
                        string login = "Логин = '" + buffer[0] + "'";
                        string pass = "Пароль = '" + buffer[1] + "'";
                        string name = "Фио = '" + buffer[2] + "'";
                        string zp = "ЗП = '" + buffer[5] + "'";
                        string position = "Должность = '" + buffer[4] + "'";
                        string phoneNumber = "Телефон = '" + buffer[3] + "'";
                        objSQL.redactionInfoUser(login, localId.ToString());
                        objSQL.redactionInfoUser(pass, localId.ToString());
                        objSQL.redactionInfoUser(name, localId.ToString());
                        objSQL.redactionInfoUser(zp, localId.ToString());
                        objSQL.redactionInfoUser(position, localId.ToString());
                        objSQL.redactionInfoUser(phoneNumber, localId.ToString());
                    }
                    continue;
                }
                else if (message == "aboutYou")
                {
                    message=post.recvMes();
                    objSQL.updateInformationAboutYourself(message,localId.ToString());
                    continue;
                }
            }
        }
        public void avtorization()
        {


            List<List<string>> arr = new List<List<string>>();
            List<string> str = new List<string>();
            List<List<string>> arr1 = new List<List<string>>();
            List<string> str1 = new List<string>();

            try
            {
                while (true)
                {
                    message = post.recvMes();
                    DataBanck.whatDo = int.Parse(message);
                    if (DataBanck.whatDo == 1)
                    {
                     
                        Console.WriteLine(message);
                        message = post.recvMes();

                        if (message=="admin admin") 
                        {
                            if (post.thisIdDontUse(0) == false)
                            {
                                message = "P";
                                Thread.Sleep(30);
                                post.sendMes(message);
                                continue;
                            }
                            message = "admin";
                            Thread.Sleep(30);
                            post.sendMes(message);
                            Console.WriteLine("Произошол вход под админом");
                            DataBanck.listUser.Add(0);
                            localId = 0;

                            post.clearStream();
                                objAdm.startAdmin();

                            post.deliteLocalId(localId);
                            localId = -1;
                            continue;
                        }

                        str = objSQL.requestLogPasSQL();//запрос в бд логинов и паролей
                        str1 = objSQL.returnAllId();
                        int size = str.Count;
                        int pointer = 0;
                        while (pointer!=size)
                        {
                            if (message == str[pointer])
                            {
                                Console.WriteLine("Совпадение логина и пароля:"+ message);

                                int id = int.Parse(str1[pointer]);
                                if (post.thisIdDontUse(id) == false)
                                {
                                    message = "P";
                                    Thread.Sleep(30);
                                    post.sendMes(message);
                                    break;
                                }
                                DataBanck.listUser.Add(id);
                                
                                
                                    localId = id;
                                    message = id.ToString();
                                    Thread.Sleep(40);
                                    post.sendMes(message);

                                //Форма пользователя
                                userForm();

                                break;
                            }
                            pointer++;
                        }
                        if (pointer == size)
                        {
                            message = "error";
                            Thread.Sleep(30);
                            post.sendMes(message);
                        }
                        continue;
                    }
                    else if (DataBanck.whatDo == 2)
                    {
                        registration();
                    }
                    else if (DataBanck.whatDo == 3)
                    {
                        break;
                    }
                    else continue;


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (localId != -1)
                {
                    post.deliteLocalId(localId);
                    localId = -1;
                }
                if (client != null)
                    client.Close();
                Console.WriteLine("Соединение закрыто");
            }
        }
        public void setLocalId(int id) 
        {
            localId = id;
        }
    }
}
