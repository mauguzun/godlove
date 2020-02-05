using AddMeFast;
using GetProxy;
using GodLoveMe.Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GodLoveMe.Strategy
{
    class DoLoginAndPin : AllStart
    {
        string usedProxy;
        int usedCount;
        public override void Print()
        {
            Console.WriteLine("I should Pin");
        }

        public override void Start()
        {




            string[] paths = Directory.GetFiles("Account");
            List<Account> acounts = Account.GetAccounts();
             acounts.Reverse();
            Console.WriteLine("start " + paths.Count());


            Parallel.ForEach(acounts, new ParallelOptions() { MaxDegreeOfParallelism = this.ThreadCount }, (acc) =>
            {
                Console.WriteLine("try " + acc.Email);
                var proxy = new GetProxy.ProxyReader();
                string current = proxy.GetList()[0];

                try
                {
                    Console.WriteLine(current);
                    drivers.InitDriver(false, current);
                    Pinterest.Pinterest pin = new Pinterest.Pinterest(drivers.Driver);


                    pin.Email = acc.Email;
                    pin.UserName = acc.UserName;
                    pin.UserName = acc.UserName;
                    pin.MakeLogin(acc.Email, acc.Password);

                    if (pin.CheckLogin())
                    {
                        pin.SaveCookie(CookieManager.Filename(acc.Email, current));
                        if (pin.ValidName() == false)
                        {
                            pin.FillName();
                        }

                        while (true)
                        {
                            if (this.allreadyTried.Keys.ToList().Contains(pin.Email))
                            {
                                this.allreadyTried[pin.Email] += 1;
                            }
                            else
                            {
                                this.allreadyTried[pin.Email] = 1;
                            }

                            if (this.allreadyTried[pin.Email] > this.Attemps)
                            {
                                Console.WriteLine("more then 15");
                                break;
                            }

                            //  pin.Follow();
                            pin.MakePost();
                            if (current == usedProxy)
                            {
                                this.usedCount++;
                            }

                            usedProxy = current;
                            if (this.usedCount > 4)
                            {
                                RemoveOneProxy(current);
                                this.usedCount = 0;
                            }
                        }
                    }
                    else
                        {
                            if (pin.Driver.FindElementById("__PWS_ROOT__") == null)
                            {
                                RemoveOneProxy(current);
                            }

                            drivers.SuperQuit();
                        }
                    }
                catch { }
                finally
                {
                    drivers.SuperQuit();

                } 
            });
        }

        private static void RemoveOneProxy( string current)
        {
            var list = new ProxyReader().GetList();
            list.Remove(current);
            File.WriteAllLines(@"C:\my_work_files\pinterest\proxy.txt", list);
        }

        private void DeleteBadAccount(string path)
        {
            try
            {
                Console.WriteLine("delete bad account");
                File.Delete(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine("on delete not valid proxy ");
            }

            drivers.SuperQuit();
        }
    }
}

