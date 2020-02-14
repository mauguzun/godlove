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
        Dictionary<string , int> proxyAttemps =  new  Dictionary<string ,int>();
   

        public override void Print()
        {
            Console.WriteLine("I should Pin");
        }

        public override void Start()
        {


            this.ThreadCount = 9;

            string[] paths = Directory.GetFiles("Account");
            List<Account> acounts = Account.GetAccounts();
            acounts.Reverse();
            Console.WriteLine("start " + paths.Count());

            Pinterest.Pinterest pin = null;
            Parallel.ForEach(acounts, new ParallelOptions() { MaxDegreeOfParallelism = this.ThreadCount }, (acc) =>
            {
                Console.WriteLine("try " + acc.Email);
                var proxy = new GetProxy.ProxyReader();
                string current = proxy.GetList().OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                try
                {
                    Console.WriteLine(current);
                    drivers.InitDriver(false, current);
                    pin = new Pinterest.Pinterest(drivers.Driver);


                    pin.Email = acc.Email;
                    pin.UserName = acc.UserName;
                    pin.UserName = acc.UserName;
                    pin.MakeLogin(acc.Email, acc.Password);
                    Console.WriteLine("start login check ");
                    if (pin.CheckLogin())
                    {
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
                                pin.SaveCookie(CookieManager.Filename(acc.Email, current));

                            }

                            if (this.allreadyTried[pin.Email] > this.Attemps)
                            {
                                Console.WriteLine("more then 15");
                                break;
                            }

                            //  pin.Follow();
                            pin.MakePost();
                            if (proxyAttemps.Keys.Contains(current))
                            {
                                if(proxyAttemps[current] > 10)
                                {
                                    RemoveOneProxy(current);
                                }
                                proxyAttemps[current] += 1;
                            }
                            else
                            {
                                proxyAttemps[current] = 1;
                            }

                          
                            
                        }
                    }
                    else
                    {
                        if (pin.Driver.FindElementsById("__PWS_ROOT__").Count() != 0 )
                        {
                            RemoveOneProxy(current);
                        }

                        drivers.SuperQuit();
                    }
                }
                catch (Exception ex)
                {
                   
                        RemoveOneProxy(current);
                  
                    drivers.SuperQuit();
                }
                finally
                {
                    drivers.SuperQuit();

                }
            });
        }

        private static void RemoveOneProxy(string current)
        {
            var list = new ProxyReader().GetList();
            if(list != null)
            { 
                list.Remove(current);
                File.WriteAllLines(@"C:\my_work_files\pinterest\proxy.txt", list);
            }
          
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

