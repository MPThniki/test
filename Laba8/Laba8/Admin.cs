using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
/* 
    Налоги
    Усно - 6% для всей вырочки
     ПФР - 22%
     ФФОМС - 5.1%
     ФСС - 2.9%
     н\сл - 0.2 %
     Сами работники 
     13 % - пенсия 
     
     */

namespace ConsoleApp1
{
    class Admin
    {
        public struct Users
        {
            public int ID;
            public string Login;
            public string Password;
            public byte Rules;
            public bool Exists;

            public void AddUSer()
            {
                try
                {
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Users.pro", FileMode.Open, FileAccess.Read))
                    using (BinaryReader FP = new BinaryReader(Stream))
                    {

                    }
                }
                catch
                {
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Users.pro", FileMode.Create))
                    {

                    }
                    
                }
                Console.Clear();
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Users.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(Stream))
                {
                    while (FP.PeekChar() != -1)
                    {
                        ID = FP.ReadInt32();
                        Login = FP.ReadString();
                        Password = FP.ReadString();
                        Rules = FP.ReadByte();
                        Exists = FP.ReadBoolean();
                        //Console.WriteLine(ID + Login + Password + Login + Rules + Exists);
                    }
                }
                Console.Write("Login: ");
                Login = Console.ReadLine();
                Console.Write("Password: ");
                Password = Console.ReadLine();
                Console.WriteLine("1 - Администратор \n2 - Склад \n3 - Касса \n4 - Кадры \n5 - Бухгалтер");
                Rules = Convert.ToByte(Console.ReadLine());
                Exists = true;
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Users.pro", FileMode.Append, FileAccess.Write))
                using (BinaryWriter FP = new BinaryWriter(Stream))
                {
                    FP.Write(ID+1);
                    FP.Write(Login);
                    FP.Write(Password);
                    FP.Write(Rules);
                    FP.Write(Exists);
                }
            }
            public void ViewUsers()
            {
                check:
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Users.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(Stream))
                {
                    while (FP.PeekChar() != -1)
                    {
                        ID = FP.ReadInt32();
                        Login = FP.ReadString();
                        Password = FP.ReadString();
                        Rules = FP.ReadByte();
                        Exists = FP.ReadBoolean();
                        // Console.WriteLine(ID + Login + Password + Login + Rules + Exists);
                    }
                }
                string[] MenuAdmin = new string[ID + 1];
                bool[] ExMenuAd = new bool[ID + 1];
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Users.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(Stream))
                {
                    int i = 0;
                    while (FP.PeekChar() != -1)
                    {
                        ID = FP.ReadInt32();
                        Login = FP.ReadString();
                        Password = FP.ReadString();
                        Rules = FP.ReadByte();
                        Exists = FP.ReadBoolean();
                        //Console.WriteLine(ID + Login + Password + Rules + Exists);
                        MenuAdmin[i] = Login;
                        ExMenuAd[i] = Exists;
                        i++;
                    }
                }
                MenuAdmin[ID] = "Создать нового пользователя";
                ConsoleKeyInfo key;
                int cursor = 0;
                do
                {
                    Console.Clear();

                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" - пользователи существующие в программе \t");
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("  ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine(" - пользователи, которые находятся в корзине");

                    for (int i = 0; i < MenuAdmin.Length; i++)
                    {
                        if (cursor == i)
                        {
                            Console.Write(">> ");
                        }
                        else
                        {
                            Console.Write("   ");
                        }

                        if (ExMenuAd[i])
                            Console.BackgroundColor = ConsoleColor.Green;
                        else if(i == MenuAdmin.Length - 1)
                            Console.BackgroundColor = ConsoleColor.Black;
                        else
                            Console.BackgroundColor = ConsoleColor.Red;

                        Console.WriteLine(MenuAdmin[i]);

                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    key = Console.ReadKey();
                    if(key.Key == ConsoleKey.DownArrow)
                    {
                        cursor += 1;
                        if (cursor >= MenuAdmin.Length)
                            cursor = 0;
                    }
                    if(key.Key == ConsoleKey.UpArrow)
                    {
                        cursor -= 1;
                        if (cursor < 0)
                            cursor = MenuAdmin.Length - 1;
                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                } while (key.Key != ConsoleKey.Enter);
                if (cursor == MenuAdmin.Length - 1)
                {
                    AddUSer();
                    goto check;
                } else
                {
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Users.pro", FileMode.Open, FileAccess.Read))
                    using (BinaryReader FP = new BinaryReader(Stream))
                    {
                        for(int i = 0; i <= cursor; i++)
                        {
                            ID = FP.ReadInt32();
                            Login = FP.ReadString();
                            Password = FP.ReadString();
                            Rules = FP.ReadByte();
                            Exists = FP.ReadBoolean();
                            //Console.WriteLine(ID + Login + Password + Rules + Exists);
                            //Console.ReadKey();
                        }
                    }
                    cursor = 0;
                    string[] Edit = new string[5] { "Изменить логин","Изменить пароль","Изменить права доступа","","Вернуться"};
                    if (Exists == true)
                        Edit[3] = "Удалить";
                    else
                        Edit[3] = "Восстановить";
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("ID \tLogin \tPass \tRules \tExists");
                        Console.WriteLine(ID + "\t" + Login + "\t" + Password + "\t" + Rules + "\t" + Exists);
                        for (int i = 0; i < Edit.Length; i++)
                        {
                            if (cursor == i)
                            {
                                Console.WriteLine(">> " + Edit[i]);
                            }
                            else
                            {
                                Console.WriteLine("   " + Edit[i]);
                            }
                        }
                        key = Console.ReadKey();
                        if(key.Key == ConsoleKey.DownArrow)
                        {
                            cursor += 1;
                            if (cursor >= Edit.Length)
                                cursor = 0;
                        }
                        if (key.Key == ConsoleKey.UpArrow)
                        {
                            cursor -= 1;
                            if (cursor < 0)
                                cursor = Edit.Length - 1;
                        }
                        if (key.Key == ConsoleKey.Escape)
                        {
                            Environment.Exit(0);
                        }
                    } while (key.Key != ConsoleKey.Enter);
                    Change(ID, MenuAdmin.Length, cursor);
                    goto check;
                }

            }

            public void Change(int id, int length,int cursor)
            {
                
                using (FileStream Stream1 = new FileStream("B:\\TEMPFORMPT\\Users.temp", FileMode.Create)) { }
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Users.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(Stream))
                {
                    while(FP.PeekChar() != -1)
                    {
                        ID = FP.ReadInt32();
                        Login = FP.ReadString();
                        Password = FP.ReadString();
                        Rules = FP.ReadByte();
                        Exists = FP.ReadBoolean();
                        if(ID == id)
                            switch (cursor)
                            {
                                case 0:
                                    Console.Clear();
                                    Console.Write("Новый логин: ");
                                    string NewLogin = Console.ReadLine();
                                    WriteToFile("B:\\TEMPFORMPT\\Users.temp", ID, NewLogin, Password, Rules, Exists);
                                    break;
                                case 1:
                                    Console.Clear();
                                    Console.Write("Новый пароль: ");
                                    string NewPassword = Console.ReadLine();
                                    WriteToFile("B:\\TEMPFORMPT\\Users.temp", ID, Login, NewPassword, Rules, Exists);
                                    break;
                                case 2:
                                    Console.Clear();
                                    Console.WriteLine("1 - Администратор \n2 - Склад \n3 - Касса \n4 - Кадры \n5 - Бухгалтер");
                                    Console.Write("Новые права доступа: ");
                                    byte NewRules = Convert.ToByte(Console.ReadLine());
                                    WriteToFile("B:\\TEMPFORMPT\\Users.temp", ID, Login, Password, NewRules, Exists);
                                    break;  
                                case 3:
                                    Console.Clear();
                                    if(Exists)
                                        Console.Write("Пользователь помещен в корзину.");
                                    else
                                        Console.Write("Пользователь восстановлен.");
                                    WriteToFile("B:\\TEMPFORMPT\\Users.temp", ID, Login, Password, Rules, !Exists);
                                    Thread.Sleep(2000);
                                    break;
                                case 4:
                                    WriteToFile("B:\\TEMPFORMPT\\Users.temp", ID, Login, Password, Rules, Exists);
                                    break;
                            }
                        else
                            WriteToFile("B:\\TEMPFORMPT\\Users.temp", ID, Login, Password, Rules, Exists);
                    }
                }
                File.Delete("B:\\TEMPFORMPT\\Users.pro");
                using (FileStream Stream1 = new FileStream("B:\\TEMPFORMPT\\Users.pro", FileMode.Create, FileAccess.Write)) { }
                WriteInUPro();
                File.Delete("B:\\TEMPFORMPT\\Users.temp");
            }

            public void WriteInUPro()
            {
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Users.temp", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(Stream))
                {
                    while (FP.PeekChar() != -1)
                    {
                        ID = FP.ReadInt32();
                        Login = FP.ReadString();
                        Password = FP.ReadString();
                        Rules = FP.ReadByte();
                        Exists = FP.ReadBoolean();
                        WriteToFile("B:\\TEMPFORMPT\\Users.pro", ID, Login, Password, Rules, Exists);
                    }
                }
            }

            public void WriteToFile(string url, int id, string login, string password, byte rules, bool exists)
            {
                using (FileStream Stream1 = new FileStream(url, FileMode.Append, FileAccess.Write))
                using (BinaryWriter FW = new BinaryWriter(Stream1))
                {
                    FW.Write(id);
                    FW.Write(login);
                    FW.Write(password);
                    FW.Write(rules);
                    FW.Write(exists);
                }
            }
        }
    }
}
