﻿using AddMeFast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodLoveMe.Start
{
    public class AllLogined : AllStart
    {
        public override void Print()
        {
            Console.WriteLine("I should Pin");
        }
        public override void Start()
        {



            string[] paths = Directory.GetFiles("Account");
            List<Account> acounts = Account.GetAccounts();

            Console.WriteLine("start " + paths.Count());


            Parallel.ForEach(paths, new ParallelOptions() { MaxDegreeOfParallelism = this.ThreadCount }, (path) =>
            {
                if (path.Contains(".xml"))
                {

                    try
                    {
                        string proxie = CookieManager.ProxyFromName(path);
                        Console.WriteLine(proxie);

                        Pinterest.Pinterest pin = new Pinterest.Pinterest(drivers.Driver);
                        string[] par = Path.GetFileNameWithoutExtension(path).Split(new string[] { "_p_" }, StringSplitOptions.RemoveEmptyEntries);

                        if (Follow)
                        {
                            drivers.InitDriver(true, par[1].Replace("_", ":"));

                        }
                       
                            drivers.InitDriver(false, par[1].Replace("_", ":"));
                       
                      
                        pin.Driver = drivers.Driver;
                        pin.Email = Path.GetFileNameWithoutExtension(par[0]);
                        pin.AccountPath = path;
                        pin.UserName = acounts.Where(x => x.Email == par[0]).FirstOrDefault().UserName;
                        pin.MakeLoginWithCookie(manager.Load(path));

                        if (pin.CheckLogin())
                        {
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
                                    break;
                                }

                                if (Follow)
                                {
                                    pin.Follow();
                                }
                                else
                                {
                                    pin.MakePost();
                                }
                                // 

                            }
                        }
                        else
                        {
                            DeleteBadAccount(path);
                        }
                    }
                    catch { }
                    finally
                    {
                        drivers.SuperQuit();

                    }

                }
            });
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
