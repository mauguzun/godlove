using AddMeFast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodLoveMe.Start
{
    public class DoLoginAndFollow : AllStart
    {

        public override void Print()
        {
            Console.WriteLine("I should follow");
        }
        public override void Start()
        {

            DriverInstance drivers = new DriverInstance();
            CookieManager manager = new CookieManager();



            var accounts = Account.GetAccounts();
            accounts = accounts.Where(x => x.Followers > 12).ToList();


            Parallel.ForEach(accounts, new ParallelOptions() { MaxDegreeOfParallelism = this.ThreadCount }, (acc) =>
            {
                Console.WriteLine("try " + acc.Email);
                var proxy = new GetProxy.ProxyReader();
                string current = proxy.GetList()[0];

                try
                {

                    Console.WriteLine("follow start " + acc.Email);

                    drivers.InitDriver(true, current);
                    Pinterest.Pinterest pin = new Pinterest.Pinterest(drivers.Driver);


                    pin.Email = acc.Email;
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
                                break;
                            }

                            pin.Follow();

                        }
                    }
                    else
                    {
                        if (pin.Driver.FindElementById("__PWS_ROOT__") == null)
                        {
                            var list = proxy.GetList();
                            list.Remove(current);
                            File.WriteAllLines(@"C:\my_work_files\pinterest\proxy.txt", list);
                        }

                        drivers.SuperQuit();
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }

                finally
                {
                    drivers.SuperQuit();

                }


            });
        }
    }
}

