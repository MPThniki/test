using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ConsoleApp1
{
    class Depot
    {
        public struct Product
        {
            int ID;
            string NAME;
            int PRICE;
            int COUNT;
            int cursor;
            ConsoleKeyInfo key;

            public void Products()
            {
            start:
                try
                {
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Products.pro", FileMode.Open, FileAccess.Read))
                    using (BinaryReader FP = new BinaryReader(Stream))
                    {
                        while (FP.PeekChar() != -1)
                        {
                            ID = FP.ReadInt32();
                            NAME = FP.ReadString();
                            COUNT = FP.ReadInt32();
                            PRICE = FP.ReadInt32();
                        }
                    }
                }
                catch
                {
                    FileStream FP = new FileStream("B:\\TEMPFORMPT\\Products.pro", FileMode.Create);
                    FP.Close();
                    goto start;
                }
                int[] ids = new int[ID];
                string[] names = new string[ID];
                int[] counts = new int[ID];
                int[] prices = new int[ID];
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Products.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(Stream))
                {
                    int i = 0;
                    while (FP.PeekChar() != -1)
                    {
                        ids[i] = FP.ReadInt32();
                        names[i] = FP.ReadString();
                        counts[i] = FP.ReadInt32();
                        prices[i] = FP.ReadInt32();
                        i++;
                    }
                }
                do
                {
                    Console.Clear();
                    for (int i = 0; i < ids.Length; i++)
                    {
                        Console.WriteLine(ids[i] + "\t" + names[i] + "\t" + counts[i]);

                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                    Console.WriteLine("F1 - Закупить товар\tEsc - выйти из программы");
                    key = Console.ReadKey();
                } while (key.Key != ConsoleKey.F1);
                if (key.Key == ConsoleKey.F1)
                {
                    ViewProductBuy();
                }
            }

            public void AddProduct()
            {
                start:
                try
                {
                    using(FileStream Stream = new FileStream("B:\\TEMPFORMPT\\BuyProduct.pro", FileMode.Open, FileAccess.Read))
                    using(BinaryReader FP = new BinaryReader(Stream))
                    {
                        while (FP.PeekChar() != -1)
                        {
                            ID = FP.ReadInt32();
                            NAME = FP.ReadString();
                            PRICE = FP.ReadInt32();
                        }
                    }
                }
                catch
                {
                    FileStream FP = new FileStream("B:\\TEMPFORMPT\\BuyProduct.pro", FileMode.Create);
                    FP.Close();
                    goto start;
                }
                Console.Write("Название товара: ");
                NAME = Console.ReadLine();
                Console.Write("Цена: ");
                write:
                try
                {
                    PRICE = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    goto write;
                }
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\BuyProduct.pro", FileMode.Append, FileAccess.Write))
                using (BinaryWriter FP = new BinaryWriter(Stream))
                {
                    FP.Write(ID + 1);
                    FP.Write(NAME);
                    FP.Write(PRICE);
                }
            }

            public void ViewProductBuy()
            {
                try
                {
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\BuyProduct.pro", FileMode.Open, FileAccess.Read))
                    using (BinaryReader FP = new BinaryReader(Stream)) { }
                }
                catch
                {
                    Console.WriteLine("\nОШИБКА!\nПродукты не найдены!");
                    AddProduct();
                    goto ViewProductBuy;
                }
                Money.MoneyAccount moneyAccount = new Money.MoneyAccount();
            ViewProductBuy:
                
                    int count = 0;
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\BuyProduct.pro", FileMode.Open, FileAccess.Read))
                    using (BinaryReader FP = new BinaryReader(Stream))
                    {
                        while (FP.PeekChar() != -1)
                        {
                            ID = FP.ReadInt32();
                            NAME = FP.ReadString();
                            PRICE = FP.ReadInt32();
                            count++;
                        }
                    }
                    string[] ProductName = new string[count + 2];
                    int[] ProductPrice = new int[count];
                    int ind = 0;
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\BuyProduct.pro", FileMode.Open, FileAccess.Read))
                    using (BinaryReader FP = new BinaryReader(Stream))
                    {
                        while (FP.PeekChar() != -1)
                        {
                            
                            ID = FP.ReadInt32();
                            NAME = FP.ReadString();
                            PRICE = FP.ReadInt32();
                            ProductName[ind] = NAME;
                            ProductPrice[ind] = PRICE;
                            ind++;
                        }
                    }
                    ProductName[count] = "Добавить продукт";
                    ProductName[count+1] = "Вернуться к просмотру";
                    cursor = 0;
                    do
                    {
                        Console.Clear();

                        for (int i = 0; i < ProductName.Length; i++)
                        {
                            if (cursor == i)
                            {
                                Console.Write(">> ");
                            }
                            else
                            {
                                Console.Write("   ");
                            }
                            if (i == ProductName.Length - 1||i == ProductName.Length - 2)
                                Console.WriteLine(ProductName[i]);
                            else
                                Console.WriteLine(ProductName[i] + "   " + ProductPrice[i]);
                        }
                        key = Console.ReadKey();
                        if (key.Key == ConsoleKey.DownArrow)
                        {
                            cursor += 1;
                            if (cursor >= ProductName.Length)
                                cursor = 0;
                        }
                        if (key.Key == ConsoleKey.UpArrow)
                        {
                            cursor -= 1;
                            if (cursor < 0)
                                cursor = ProductName.Length - 1;
                        }
                        if (key.Key == ConsoleKey.Escape)
                        {
                            Environment.Exit(0);
                        }
                    } while (key.Key != ConsoleKey.Enter);
                    Console.Clear();
                    if (cursor == ProductName.Length - 2)
                    {
                        AddProduct();
                        goto ViewProductBuy;
                    } else
                    {
                        if (cursor == ProductName.Length - 1)
                        {
                            Products();
                            goto ViewProductBuy;
                        }
                        Buy(cursor);
                        goto ViewProductBuy;
                    }

                        
                    
                   }

            public void Buy(int cursor)
            {
                Money.MoneyAccount moneyAccount = new Money.MoneyAccount();
                using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\BuyProduct.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(Stream))
                {
                    while (FP.PeekChar() != -1)
                    {

                        ID = FP.ReadInt32();
                        NAME = FP.ReadString();
                        PRICE = FP.ReadInt32();
                        if (ID == cursor + 1)
                            break;
                    }
                }

                cursor = 0;
                
                string[] product = new string[2] {"Закупить","Вернуться"};

                do
                {
                    Console.Clear();
                    Console.WriteLine(ID + "  " + NAME + "\t" + PRICE);
                    for (int i = 0; i < product.Length; i++)
                    {
                        if (cursor == i)
                            Console.WriteLine(">> " + product[i]);
                        else
                            Console.WriteLine("   " + product[i]);
                    }
                    key = Console.ReadKey();
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        cursor += 1;
                        if (cursor >= product.Length)
                            cursor = 0;
                    }
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        cursor -= 1;
                        if (cursor < 0)
                            cursor = product.Length - 1;
                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                } while (key.Key != ConsoleKey.Enter);
                Console.Clear();
                if(cursor == 1)
                    ViewProductBuy();
                using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\MoneyAccount.pro", FileMode.Open, FileAccess.Read))
                using (BinaryReader FP = new BinaryReader(stream))
                {
                    moneyAccount.SumMoney = FP.ReadInt32();
                }
            rep:
                Console.Clear();
                int lastKey = 1;
                string StCount = "1";
                int Count = 1;
                int cost = Count * PRICE;
                Console.WriteLine("Количество закупаемого продукта: " + StCount);
                Console.WriteLine("Общая стоимость: " + cost);
                Console.WriteLine("Денег на счету: " + moneyAccount.SumMoney);
                Console.WriteLine("Останется после покупки: " + (moneyAccount.SumMoney - cost));
                Console.WriteLine("Q - выйти\tENTER - продолжить");
                do
                {

                    key = Console.ReadKey(true);
                    Console.Clear();
                    Console.Write("Количество закупаемого продукта: ");

                    if (key.KeyChar >= '0' && key.KeyChar <= '9')
                    {
                        lastKey++;
                        if (StCount == "0")
                            StCount = StCount.Substring(0, 0);
                        StCount += key.KeyChar;

                    }
                    if (key.Key == ConsoleKey.Backspace)
                    {
                        if (lastKey > 0)
                        {
                            lastKey--;
                        }
                        if (lastKey != 0)
                            StCount = StCount.Substring(0, lastKey);
                        else
                            StCount = "0";


                    }
                    if (key.KeyChar == 'Q' || key.KeyChar == 'q')
                    {
                        goto exit;
                    }


                    Console.WriteLine(StCount);

                    Count = Convert.ToInt32 (StCount);
                    cost = Count * PRICE;

                    Console.WriteLine("Общая стоимость: " + cost);
                    Console.WriteLine("Денег на счету: " + moneyAccount.SumMoney);
                    Console.WriteLine("Останется после покупки: " + (moneyAccount.SumMoney - cost));
                    Console.WriteLine("Q - выйти\tENTER - продолжить");

                } while (key.Key != ConsoleKey.Enter);
                if ((moneyAccount.SumMoney - cost) > 0)
                {
                    Console.WriteLine("Товар закуплен!");
                    using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\MoneyAccount.pro", FileMode.Create, FileAccess.Write))
                    using (BinaryWriter FP = new BinaryWriter(stream))
                    {
                        FP.Write(moneyAccount.SumMoney - cost);
                    }

                    int id = 0;
                    string name = "";
                    int coun = 0;
                    int price = 0;

                    using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\Products.pro", FileMode.Open, FileAccess.Read))
                    using (BinaryReader FP = new BinaryReader(stream))
                    {
                        while(FP.PeekChar() != -1)
                        {
                            id = FP.ReadInt32();
                            name = FP.ReadString();
                            coun = FP.ReadInt32();
                            price = FP.ReadInt32();
                        }
                    }
                    using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Products.pro", FileMode.Append, FileAccess.Write))
                    using (BinaryWriter FP = new BinaryWriter(Stream))
                    {
                        FP.Write(id + 1); 
                        FP.Write(NAME);
                        FP.Write(Count);
                        FP.Write(PRICE/10*13);
                    }
                    Thread.Sleep(1000);
                    ViewProductBuy();

                }
                else
                {
                    Console.WriteLine("Покупка невозможна!\nНедостаточно средств для произведения операции.");
                    Thread.Sleep(1000);
                    goto rep;
                }

            exit:;
            }

        }
    }
}
