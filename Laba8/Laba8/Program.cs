using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Auth()
        {
            repeatAuth:
            Admin.Users User = new Admin.Users();
            Depot.Product product = new Depot.Product();
            CashOut.Sale job = new CashOut.Sale();
            PerDep.Perdep perdep = new PerDep.Perdep();
            BokKep.Bokkep bokkep = new BokKep.Bokkep();
            string Login, Password = "";
            ConsoleKeyInfo key;
            Console.Write("Login: ");
            Login = Console.ReadLine();
            Console.Write("Password: ");
            int lastKey = 0;
            string star;
            do
            {
                
                key = Console.ReadKey(true);
                Console.Clear();
                Console.WriteLine("Login: " + Login);
                Console.Write("Password: ");
                
                if (key.KeyChar >= '0' && key.KeyChar <= '9' || key.KeyChar >= 'A' && key.KeyChar <= 'Z' || key.KeyChar >= 'a' && key.KeyChar <= 'z' || key.KeyChar >= 'А' && key.KeyChar <= 'Я' || key.KeyChar >= 'а' && key.KeyChar <= 'я')
                {
                    lastKey++;
                    Password += key.KeyChar;

                }
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (lastKey > 0)
                    {
                        lastKey--;
                    }
                    Password = Password.Substring(0, lastKey);
                    
                    
                }
                for (int i = 0; i < Password.Length; i++)
                {
                    Console.Write("*");
                }

            } while (key.Key != ConsoleKey.Enter);
            bool ViewUsers = false;
            using (FileStream Stream = new FileStream("B:\\TEMPFORMPT\\Users.pro", FileMode.Open, FileAccess.Read))
            using (BinaryReader FP = new BinaryReader(Stream))
            {
                while (FP.PeekChar() != -1)
                {
                    User.ID = FP.ReadInt32();
                    User.Login = FP.ReadString();
                    User.Password = FP.ReadString();
                    User.Rules = FP.ReadByte();
                    User.Exists = FP.ReadBoolean();
                    if (User.Login == Login && User.Password == Password && User.Exists)
                    {   
                        ViewUsers = true;
                        break;
                    }
                }
            }
            if (ViewUsers)
            {
            switch (User.Rules)
            {
                case 1:
                    User.ViewUsers();
                    break;
                case 2:
                    //User.ViewUsers();
                    product.Products();
                    break;
                case 3:
                    job.StartJob();
                    break;
                case 4:
                    perdep.View();
                    break;
                case 5:
                    bokkep.View();
                    break;
            }
            }
            else
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Авторизация несовершилась.\nПопробуйте снова");
                    Console.WriteLine("Enter - продолжить\tEscape - выйти");
                    key = Console.ReadKey();
                    if(key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                } while (key.Key != ConsoleKey.Enter);
                Console.Clear();
                goto repeatAuth;
            }
        }

        static void Main(string[] args)
        {
           // Admin.Users User = new Admin.Users();
            Console.CursorVisible = false;
            //Money.MoneyAccount moneyAccount = new Money.MoneyAccount();
            //moneyAccount.CreatFile();
            //User.AddUSer();
            Auth();
            //
            //User.ViewUsers();
        }
    }
}
