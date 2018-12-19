using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Money
    {
        public struct MoneyAccount
        {
            public double SumMoney;
            public string MsgHistory; 

            void Error()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ОШИБКА! Поле не может содержать буквы, спецсимволы или быть отрицательной!");
                Console.ForegroundColor = ConsoleColor.Black;
            }

            public void CreatFile()
            {
                
                using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\MoneyAccount.pro", FileMode.Create, FileAccess.Write))
                using (BinaryWriter FP = new BinaryWriter(stream))
                {
                    WriteCap:
                    Console.Write("Введите начальную сумму капитала: ");
                    try
                    {
                        SumMoney = Convert.ToInt32(Console.ReadLine());
                        if (SumMoney < 0)
                        {
                            Error();
                            goto WriteCap;
                        }
                             
                    }
                    catch
                    {
                        Error();
                        goto WriteCap;
                    }
                    FP.Write(SumMoney);
                }
                MsgHistory = "Зачисление " + SumMoney;
                using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\MoneyAccountHistory.pro", FileMode.Create, FileAccess.Write))
                using (BinaryWriter FP = new BinaryWriter(stream))
                {
                    FP.Write(MsgHistory);
                }
            }

            public void Enroll(int Money)
            {
                using (BinaryReader FP = new BinaryReader(new FileStream("B:\\TEMPFORMPT\\MoneyAccount.pro", FileMode.Open, FileAccess.Read)))
                {
                    SumMoney = FP.ReadInt32();
                }
                SumMoney += Money;
                File.Delete("B:\\TEMPFORMPT\\MoneyAccount.pro");
                using (FileStream stream = new FileStream("B:\\TEMPFORMPT\\MoneyAccount.pro", FileMode.Create, FileAccess.Write))
                using (BinaryWriter FP = new BinaryWriter(stream))
                {
                    FP.Write(SumMoney);
                }
            }
        }
    }
}
