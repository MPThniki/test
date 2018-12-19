using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ConsoleApp1
{
    class CashOut
    {
        public struct Sale
        {
            int MONEY;
            int COUNT;
            int ID;
            int PRICE;

            

            string PRODUCT_NAME;
            
            ConsoleKeyInfo key;

            public void StartJob()
            {
                Money.MoneyAccount moneyAccount = new Money.MoneyAccount();
                Console.WriteLine("Касса открыта!");
                try
                {
                    using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\Products.pro", FileMode.Open, FileAccess.Read))
                    {

                    }
                }
                catch
                {
                    for (int i = 10; i >= 0; i--)
                    {
                        Console.Clear();
                        Console.WriteLine("Нет закупленных товаров!\nОбратитесь к работнику склада");
                        Console.WriteLine("Программа закроется через "+i+" сек");
                        Thread.Sleep(1000);
                    }
                    Environment.Exit(0);
                }
                conti:
                using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\Products.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(stream))
                {
                    while (FP.PeekChar() != -1)
                    {
                        ID = FP.ReadInt32();
                        PRODUCT_NAME = FP.ReadString();
                        COUNT = FP.ReadInt32();
                        PRICE = FP.ReadInt32();
                    }
                }
                int[] ids = new int[ID];
                string[] names = new string[ID];
                int[] counts = new int[ID];
                int[] prices = new int[ID];
                int z = 0;
                using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\Products.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(stream))
                {
                    while(FP.PeekChar() != -1)
                    {
                        ids[z] = FP.ReadInt32();
                        names[z] = FP.ReadString();
                        counts[z] = FP.ReadInt32();
                        prices[z] = FP.ReadInt32();
                        z++;
                    }
                }

                int cursor = 0;

                do
                {
                    Console.Clear();
                    Console.WriteLine($"   { "ID", 5} {"Наименование",10} {"кол-во",10} {"цена",8}");
                    for (int i = 0; i < ids.Length; i++)
                    {
                        if (cursor == i)
                        {
                            Console.Write(">> ");
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                        Console.WriteLine($"{ids[i],5} {names[i], 10} {counts[i],10} {prices[i],10}");
                    }
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        cursor += 1;
                        if (cursor >= ids.Length)
                            cursor = 0;
                    }
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        cursor -= 1;
                        if (cursor < 0)
                            cursor = ids.Length - 1;
                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                } while (key.Key != ConsoleKey.Enter);
                do
                {
                    Console.Clear();
                    Console.WriteLine("Продажа продукта: " + names[cursor]);
                    Console.WriteLine("Количество продукта имеется на складе: " + counts[cursor]);
                    Console.WriteLine("Цена продукта: " + prices[cursor]);
                    Console.WriteLine("Enter - продать\tBackspace - вернуться\tEscape - выход из программы");
                    key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.Enter:
                            SaleProduct(ref ids[cursor], ref names[cursor], ref counts[cursor], ref prices[cursor]);
                            break;
                        case ConsoleKey.Backspace:
                            break;
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;

                    }
                } while (key.Key != ConsoleKey.Enter);

                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Products.pro", FileMode.Create, FileAccess.Write))
                using (BinaryWriter FP = new BinaryWriter(Stream))
                {
                    for (int i = 0; i < ids.Length; i++)
                    {
                        if(counts[i] != 0)
                        {
                            FP.Write(ids[i]);
                            FP.Write(names[i]);
                            FP.Write(counts[i]);
                            FP.Write(prices[i]);
                        }
                        
                    }
                }
                string[] Menu = new string[2] { "Продолжить работу с кассой", "Сдать кассу" };
                cursor = 0;
                do
                {
                    Console.Clear();
                    for (int i = 0; i < 2; i++)
                    {
                        if (cursor == i)
                        {
                            Console.WriteLine(">> " + Menu[i]);
                        }
                        else
                        {
                            Console.WriteLine("   " + Menu[i]);
                        }
                    }

                    key = Console.ReadKey();

                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        cursor += 1;
                        if (cursor >= Menu.Length)
                            cursor = 0;
                    }
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        cursor -= 1;
                        if (cursor < 0)
                            cursor = Menu.Length - 1;
                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }

                   
                } while (key.Key != ConsoleKey.Enter);

                switch (cursor)
                {
                    case 0:
                        goto conti;
                    case 1:
                        moneyAccount.Enroll( MONEY);
                        Environment.Exit(0);
                        break;
                }

                goto conti;
            }
            
            public void SaleProduct(ref int id, ref string name, ref int count, ref int price)
            {
                start:
                int cou;
                Console.Clear();
                Console.WriteLine("Продажа продукта: " + name);
                Console.WriteLine("Количество продукта имеется на складе: " + count);
                Console.WriteLine("Цена продукта: " + price);
                Console.Write("Количество: ");
                cou = Convert.ToInt32(Console.ReadLine());
                if(count - cou >= 0)
                {
                    count -= cou;
                    MONEY += cou * (PRICE / 100) * 94;
                }
                else
                {
                    Console.WriteLine("Количество продаваемого товара превышает количество закупленного");
                    Thread.Sleep(5000);
                    goto start;
                }

            }
        }
    }
}
