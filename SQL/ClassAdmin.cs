using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.ComponentModel;
using System.Threading;
using System.Deployment.Internal;
using System.Net.NetworkInformation;

namespace SQL
{
	class ClassAdmin
	{
		private Posts post;
		private SQLclass objSQL = new SQLclass();
		private TableManipulations objT = new TableManipulations();
		private TcpClient client;
		private string message;
		public ClassAdmin(TcpClient tcpClient)
		{
			client = tcpClient;
			post = new Posts(client);
		}
		public List<string> serchForm()
		{
			List<List<string>> arr = new List<List<string>>();
			List<string> str = new List<string>();

			List<List<string>> arr1 = new List<List<string>>();
			List<string> str1 = new List<string>();

					str = objSQL.getLoginsAndId();
					int size = objSQL.returnSizeBD();

					int i = 0, j = 0, k = 0, bilLiSovpod = 0, maxSchet = 0;
					int[] schet = new int[size];

					for (i = 0; i < size; i++)
					{
						schet[i] = 0;
						str[i] += '\n';
						while (true)
						{
							if (str[i][j] == '\n')
							{
								if (maxSchet < schet[i])
									maxSchet = schet[i];

								j = 0; break;
							}

							if (str[i][j] >= 'А' && str[i][j] <= 'я')
							{
								if (str[i][j] == message[k] || str[i][j] == message[k] - 32 || str[i][j] == message[k] + 32)
								{
									schet[i]++;
									k++;
									bilLiSovpod = 1;
								}
								else
								if (maxSchet < schet[i])
								{
									maxSchet = schet[i];
									schet[i] = 0;
									
									k = 0;
								}
							}
							else
							if (str[i][j] >= 'A' && str[i][j] <= 'Z' || str[i][j] >= 'a' && str[i][j] <= 'z')
							{

								if (str[i][j] >= 'a' && str[i][j] <= 'z')
								{
									if (str[i][j] == message[k] || str[i][j] - 32 == message[k])
									{
										schet[i]++;
										k++;
										bilLiSovpod = 1;
									}
									else
										if (maxSchet < schet[i])
									{
										maxSchet = schet[i];
										schet[i] = 0;
										
										k = 0;
									}
								}
								else
								if (str[i][j] == message[k] || str[i][j] + 32 == message[k])
								{
									schet[i]++;
									k++;
									bilLiSovpod = 1;
								}
								else
									if (maxSchet < schet[i])
								{
									maxSchet = schet[i];
									schet[i] = 0;
									
									k = 0;
								}
							}
							else
							if (str[i][j] >= '0' && str[i][j] <= '9')
							{
								if (str[i][j] == message[k])
								{
									schet[i]++;
									k++;
									bilLiSovpod = 1;
								}
								else
									if (maxSchet < schet[i])
								{
									maxSchet = schet[i];
									schet[i] = 0;
									
									k = 0;
								}
							}

							j++;

						}

						schet[i] = maxSchet;
						maxSchet = 0;
						k = 0;

					}

					if (bilLiSovpod == 0)
					{
						str1.Add ( "Не было ни одного совпадения");
					return str1;
					}
					else
					{
						maxSchet = 0;

						for (i = 0; i < size; i++)
						{
							if (schet[i] > maxSchet)
							{
								maxSchet = schet[i];
							}
						}
						k = 0;
						int[] mas = new int[size];
						for (i = 0; i < size; i++)
						{
							if (schet[i] == 0) { continue; }
							if (schet[i] == maxSchet || schet[i] == maxSchet - 1)
							{

								str1.Add(str[i]);

								k++;
							}
							else mas[i] = 0;
						}
						i = 0;
						int size2 = k;
						k++;
						post.sendMes(k.ToString());
						k = 0;
						List<string> id = new List<string>();
						while (true)
						{
							if (k == size2) break;
							string[] words = str1[k].ToString().Split('|');
							id.Add(words[0]);
							str = objSQL.getInfoUser(id[k].ToString());
							str1[k] = id[k] + "|";
							str1[k] += str[0];
							k++;
							i++;
						}

					return str1;
					}

		}
		public void startAdmin()
		{

			List<List<string>> arr = new List<List<string>>();
			List<string> str = new List<string>();

				while (true)
				{
				post.clearStream();
				str = objSQL.getFinance();
				Thread.Sleep(20);
				
				for (int i = 0; i < str.Count; i++)
				{
					str[i] += '|';
					post.sendMes(str[i]);
				}

				Thread.Sleep(40);
				message = post.recvMes();
				if (message == "end")
				{
					return;
				}
				else if (message == "userTable")
				{
					int sort = 0;
					while (true)
					{
						if (sort != 1)
						{
							
						
						int sizeMess = objSQL.returnSizeBD();
						post.clearStream();
						Thread.Sleep(60);
						post.sendMes(sizeMess.ToString());
						str = objSQL.getAllInfoUser();

						str = objT.makeTable(str, sizeMess);

						message = "|id|Логин                    |Пароль                   |ФИО                      |Зарплата                 |Должность                |Телефон                  |\n";
						Thread.Sleep(100);
						post.sendMes(message);
						int x = 0;
						while (true)
						{
							str[x] += "\n";
							post.sendMes(str[x]);
							Thread.Sleep(40);
							if (x == sizeMess - 1)
							{
								break;
							}
							x++;
						}
						}
						sort = 0;
						//Ожидание новых комманд
						Thread.Sleep(40);

					message = post.recvMes();
					if (message == "end")
					{
						break;
					}
					else
					if (message == "serch")
					{
						while (true)
						{

							message = post.recvMes();
							if (message == "end")
							{
								break;
							}
							else
							{
								str = serchForm();
								if (str[0] == "Не было ни одного совпадения")
								{
									post.sendMes(str[0]);
									continue;
								}
								str = objT.makeTable(str, str.Count);
								message = "|id|Логин                    |Пароль                   |ФИО                      |Зарплата                 |Должность                |Телефон                  |\n";
								post.clearStream();
								Thread.Sleep(100);
								post.sendMes(message);
								int k = 0;
								while (true)
								{

									if (k == str.Count) break;
									post.sendMes(str[k]);
									k++;

								}

							}


						}




					}
					else //Сделать сортировку
					if (message == "sorting")
					{
							objT.sort(post.returnTcp());
							post.clearStream();
							sort = 1;
							continue;
					}
					else
					if (message == "goIn")
					{
							post.clearStream();
							message = post.recvMes();
							str = serchForm();
							post.clearStream();
							if (str[0] == "Не было ни одного совпадения")
							{
							Thread.Sleep(40);
							post.sendMes(str[0]);
							continue;
							}
							
							string[] words = str[0].Split('|');
						int id = Int32.Parse(words[0]);
						ClassUser obgUser = new ClassUser(client);
						obgUser.setLocalId(id);
						obgUser.userForm();
						continue;
					}
					else
					if (message == "delete")
					{
						message = post.recvMes();
						str = objSQL.returnAllId();
						int delete = 0;
						for (int i = 0; i < str.Count; i++)
						{
							if (message == str[i])
							{
								objSQL.deleteUser(message);
								message = "Пользователь удален";
								post.sendMes(message);
								delete = 1;
								break;
							}
						}
						if (delete == 0)
						{
							message = "Нету такого пользователя";
							post.sendMes(message);
						}
							
						continue;
					}
					else
					if (message == "alert")
					{
						message = post.recvMes();
						string buffId = message;
						string buff2;
						str = objSQL.getUserInformationAboutYourself(buffId);
						Thread.Sleep(40);
						post.sendMes(str[0]);
						message = post.recvMes();
						buff2 = post.recvMes();
						if (message == "end") { 
								continue; }
						else
						{
							objSQL.updateInformationAboutYourself(buff2, message);
						}
						continue;
					}
					}

					}
					else if (message == "providers")
					{
					while (true)
					{
						message = post.recvMes();
						if (message == "end")
						{
							break;
						}
						else if (message == "continue")
						{
							post.clearStream();
							Thread.Sleep(30);
							message = post.recvMes();
							post.clearStream();
							if (message == "end")
							{
								continue;
							}
							else
							{
								int numberOfExperts = int.Parse(message);
								int scaleSize = int.Parse(post.recvMes());
								message = post.recvMes();
								string lenghP = message;
								string[] providers = message.Split('\n');
								int sizeMart = providers.Length;
								string[] matr = objT.makeStrForExpeert(scaleSize, sizeMart);

								double[,] assessments = new double[numberOfExperts, sizeMart];

								Thread.Sleep(70);
								post.sendMes(numberOfExperts.ToString());
								post.clearStream();
								Thread.Sleep(70);
								post.sendMes(matr.Length.ToString());
								post.clearStream();



								for (int i = 0; i < numberOfExperts; i++)
								{
									Thread.Sleep(70);
									post.sendMes(lenghP);
									post.clearStream();
									Thread.Sleep(20);
									for (int j = 0; j < sizeMart; j++)
									{
										post.sendMes(matr[j]);

									}
									post.clearStream();
									message = post.recvMes();
									post.clearStream();
									if (message == "end")
									{
										break;
									}
									else
									{
										int k = 0;
										string[] buff = message.Split('\n');
										//строки
										for (int h = 0; h < sizeMart; h++)
										{
											string[] buff2 = buff[h].Split('|');
											//символы
											for (int l = 0; l < sizeMart; l++)
											{
												if (buff2[l] != "-")
												{
													string[] buff3 = buff2[l].Split('/');
													assessments[i, k] += double.Parse(buff3[0]) / scaleSize;
												}
											}
											k++;
										}
										continue;
									}

								}
								List<double> buf = new List<double>();
								double buf2 = 0;
								for (int x = 0; x < sizeMart; x++)
								{
									for (int x1 = 0; x1 < numberOfExperts; x1++)
									{
										assessments[x1, x] = assessments[x1, x] / (sizeMart * (sizeMart - 1));
										buf2 += assessments[x1, x];
									}
									buf.Add(buf2);
									Console.WriteLine(buf[x]);
									buf2 = 0;
								}
								Thread.Sleep(20);
								post.sendMes(sizeMart.ToString());
								Thread.Sleep(50);
								for (int x3 = 0; x3 < sizeMart; x3++)
								{
									providers[x3] += "=" + buf[x3]+"\n";
									post.sendMes(providers[x3]);
								}

							}
						}

					}

					}
					else if(message == "option Chart")
					{
						while (true)
						{
						str = objSQL.getFinance();
						for (int i = 0; i < str.Count; i++)
						{
							str[i] += '|';
							post.sendMes(str[i]);
						}
					
							message = post.recvMes();
							if (message == "end")
							{
							break;
							}
							else if (message == "set")
							{
							string month = post.recvMes();
							string money = post.recvMes();
							objSQL.updateFinance(month, money);
							continue;
							}
						}
					}

				}

			
		}
	}
}
