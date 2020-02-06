using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AddMeFast;
using GetProxy;
using GodLoveMe.Start;
using GodLoveMe.Strategy;
using GodLoveMe.Utils;

namespace GodLoveMe
{
    class Program
    {
        static void Main(string[] args)
        {
            //1 zaloginem 


            Console.WriteLine("1 login ,2 pin ,3 follow,4 logined pin,5 logined follow,6 users, 7  all appedn from file  Check Proxy");

            //var p=    ProxySharp.Proxy.GetSingleProxy();
            //DriverInstance dr = new DriverInstance();
            //dr.InitDriver(true, p);
            int design = 1;
            string choosen = Console.ReadLine();

            Int32.TryParse(choosen, out design);


            AllStart strategy = new DoLogin();
            strategy.ThreadCount = 5;
            switch (design)
            {
                case 1:
                    strategy = new DoLogin();
                    break;
                case 2:
                    strategy = new DoLoginAndPin();
                    break;
                case 3:
                    strategy = new DoLoginAndFollow();
                    break;
                case 4:
                    strategy = new AllLogined();
                    strategy.Follow = false;
                    break;
                case 5:
                    strategy = new AllLogined();
                    strategy.Follow = true;
                    break;
                case 6:
                    strategy = new AppendUsers();
                    break;
                case 7:
                    strategy = new AppnedFfromFileLoginPassword();
                    break;




                default:
                    Thread other = new Thread(() =>
                    {
                        Proxy.Checker();
                    });
                    other.Start();
                    Console.ReadKey();
                    break;


            }




          
            strategy.Start();







        }
    }
}
