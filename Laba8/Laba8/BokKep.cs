using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ConsoleApp1
{
    class BokKep
    {
        public struct Bokkep
        {
            public int ID;
            public string FIO;
            public string DEP;
            public int SALARY;
            public double SumMoney;
            public double SumSALARY;

            public void Issue(int id)
            {
                using (BinaryReader FP = new BinaryReader(new FileStream("B:\\TEMPFORMPT\\MoneyAccount.pro", FileMode.Open, FileAccess.Read)))
                {
                    SumMoney = FP.ReadDouble();
                }
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Working.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(Stream))
                {
                    while (id == ID)
                    {
                        ID = FP.ReadInt32();
                        FIO = FP.ReadString();
                        DEP = FP.ReadString();
                        SALARY = FP.ReadInt32();
                    }
                }
                Console.Clear();
                Console.WriteLine("Процесс выдачи заработной платы...");
                SumSALARY = SALARY + (SALARY / 100) * 22 + (SALARY / 100) * 5.1 + (SALARY / 100) * 0.2 + (SALARY / 100) * 2.9;
                if (SumSALARY < SumMoney)
                {
                    Console.WriteLine("ФИО: " + FIO);
                    Console.WriteLine("Зарплата = " + SALARY);
                    Console.WriteLine("Зарплата + налоги = " + SumSALARY);
                    SumMoney -= SumSALARY;
                }
                using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\MoneyAccount.pro", FileMode.Create, FileAccess.Write))
                using (BinaryWriter FP = new BinaryWriter(stream))
                {
                    FP.Write(SumMoney);
                }
                Thread.Sleep(2000);
            }

            public void View()
            {
                start:
                try
                {
                    using (BinaryReader FP = new BinaryReader(new FileStream("B:\\TEMPFORMPT\\MoneyAccount.pro", FileMode.Open, FileAccess.Read)))
                    {
                        SumMoney = FP.ReadDouble();
                    }
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
                catch
                {
                    for (int i = 10; i > 0; i--)
                    {
                        Console.Clear();
                        Console.WriteLine("Завершение работы программы!\nE: Файлы для работы программы не найдены");
                        Thread.Sleep(1000);
                    }
                    Environment.Exit(0);
                }

                string[] FIOs = new string[ID];
                int[] SALARYs = new int[ID];
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
                        FIOs[ind] = FIO;
                        SALARYs[ind] = SALARY;
                        ind++;
                    }
                }

                ConsoleKeyInfo key;
                int cursor = 0;

                do
                {
                    Console.Clear();
                    Console.WriteLine("Выберите работника для выдачи заработной платы");
                    for (int i = 0; i < FIOs.Length; i++)
                    {
                        if (cursor == i)
                        {
                            Console.Write(">> ");
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                        Console.WriteLine(FIOs[i] + "    " + SALARYs[i]);
                    }
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        cursor += 1;
                        if (cursor >= FIOs.Length)
                            cursor = 0;
                    }
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        cursor -= 1;
                        if (cursor < 0)
                            cursor = FIOs.Length - 1;
                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                } while (key.Key != ConsoleKey.Enter);
                Issue(cursor);
                goto start;
            }
        } 
    }
}
