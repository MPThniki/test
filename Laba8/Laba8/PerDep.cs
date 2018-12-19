using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ConsoleApp1
{
    class PerDep
    {
        public struct Perdep
        {
            public int ID;
            public string FIO;
            public string DEP;
            public int SALARY;

            public void Add()
            {
            start:
                bool creat = true;
                Console.Clear();
                Console.WriteLine("Добавление работника...");
                try
                {
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Working.pro", FileMode.Open, FileAccess.Read))
                    using (BinaryReader FP = new BinaryReader(Stream))
                    {
                        while (FP.PeekChar() != -1)
                        {
                            ID = FP.ReadInt32();
                            FIO = FP.ReadString();
                            DEP = FP.ReadString();
                            SALARY = FP.ReadInt32();
                        }

                    }
                }
                catch (Exception)
                {
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Working.pro", FileMode.Create)) { }
                    creat = false;
                    goto start;
                }
                if(creat)
                ID++;
                Console.Write("ФИО работника: ");
                FIO = Console.ReadLine();
                Console.Write("Отдел: ");
                DEP = Console.ReadLine();
                writeSal:
                Console.Write("Заработная плата: ");
                try
                {
                    SALARY = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("E: Сумма введена неправильно!");
                    goto writeSal;
                }
                using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\Working.pro", FileMode.Append, FileAccess.Write))
                using (BinaryWriter FP = new BinaryWriter(stream))
                {
                    FP.Write(ID);
                    FP.Write(FIO);
                    FP.Write(DEP);
                    FP.Write(SALARY);
                }
                Console.Clear();
                Console.WriteLine("Работник создан...");
                Thread.Sleep(1000);
            }

            public void View()
            {
                
                try
                {
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Working.pro", FileMode.Open, FileAccess.Read))
                    {

                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("E: Файл со списком рабочих не найден");
                    Thread.Sleep(1000);
                    Add();
                }
            start:
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Working.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(Stream))
                {
                    while (FP.PeekChar() != -1)
                    {
                        ID = FP.ReadInt32();
                        FIO = FP.ReadString();
                        DEP = FP.ReadString();
                        SALARY = FP.ReadInt32();
                    }

                }
                if (ID == 0)
                    Add();
                string[,] working = new string[ID + 1,3];
                int Length = ID + 1;
                int ind = 0;
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Working.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(Stream))
                {
                    while (FP.PeekChar() != -1)
                    {
                        ID = FP.ReadInt32();
                        FIO = FP.ReadString();
                        DEP = FP.ReadString();
                        SALARY = FP.ReadInt32();
                        working[ind, 0] = FIO;
                        working[ind, 1] = DEP;
                        working[ind, 2] = Convert.ToString(SALARY);
                        ind++;
                    }

                }
                working[ID, 0] = "Добавить работника";
                working[ID, 1] = "";
                working[ID, 2] = "";
                ConsoleKeyInfo key;
                int cursor = 0;
                do
                {
                    Console.Clear();
                    for (int i = 0; i < Length; i++)
                    {
                        if (cursor == i)
                        {
                            Console.Write(">> ");
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                        Console.WriteLine($"{working[i,0],10} {working[i,1],10} {working[i,2],10}");
                    }
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        cursor += 1;
                        if (cursor >= Length)
                            cursor = 0;
                    }
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        cursor -= 1;
                        if (cursor < 0)
                            cursor = Length - 1;
                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                } while (key.Key != ConsoleKey.Enter);
                if(cursor == Length - 1)
                {
                    Add();
                    goto start;
                }
                else
                {
                    Edit(ref working, cursor, Length);
                    goto start;
                }
            }

            public void Edit(ref string[,] working, int id, int Length)
            {
                Console.Clear();
                Console.WriteLine($"{working[id, 0],10} {working[id, 1],10} {working[id, 2],10}");
                string[] menu = new string[3] {"Редактировать", "Удалить/восстановить", "Вернуться"};
                int cursor = 0;
                ConsoleKeyInfo key;
                do
                {
                    Console.Clear();
                    for (int i = 0; i < menu.Length; i++)
                    {
                        if (cursor == i)
                        {
                            Console.Write(">> ");
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                        Console.WriteLine(menu[i]);
                    }
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        cursor += 1;
                        if (cursor >= menu.Length)
                            cursor = 0;
                    }
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        cursor -= 1;
                        if (cursor < 0)
                            cursor = menu.Length - 1;
                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                } while (key.Key != ConsoleKey.Enter);
                if (cursor == 1)
                {
                    working[id, 0] = "-";
                }
                else if (cursor == 0)
                {
                    Console.Write("ФИО работника: ");
                    FIO = Console.ReadLine();
                    Console.Write("Отдел: ");
                    DEP = Console.ReadLine();
                writeSal:
                    Console.Write("Заработная плата: ");
                    try
                    {
                        SALARY = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("E: Сумма введена неправильно!");
                        goto writeSal;
                    }
                    working[id, 0] = FIO;
                    working[id, 1] = DEP;
                    working[id, 2] = Convert.ToString(SALARY);
                }
                else if (cursor == 2)
                    goto finish;
                ID = 1;
                File.Delete("B:\\TEMPFORMPT\\Working.pro");
                using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\Working.pro", FileMode.CreateNew, FileAccess.Write))
                using (BinaryWriter FP = new BinaryWriter(stream))
                {
                    for (int i = 0; i < Length - 1; i++)
                    {
                        if (working[i,0] != "-")
                        {
                            FP.Write(ID);
                            FP.Write(working[i, 0]);
                            FP.Write(working[i, 1]);
                            FP.Write(Convert.ToInt32(working[i,2]));
                            ID++;
                        }
                    }
                }
            finish:;
            }
        }
    }
}
