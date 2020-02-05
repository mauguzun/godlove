using AddMeFast;
using GodLoveMe.Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodLoveMe.Strategy
{
    public class DoLogin : AllStart
    {

        private int repeatAccountInOneProxy = 0;
        public override void Print()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {

            var accounts = Account.GetAccounts();

            List<string> logined = new List<string>();
            foreach(var path in Directory.GetFiles("Account"))
            {
                if (path.Contains("xml"))
                {
                    string[] par = Path.GetFileNameWithoutExtension(path).Split(new string[] { "_p_" }, StringSplitOptions.RemoveEmptyEntries);
                    logined.Add(par[0]);
                }
            }


            Parallel.ForEach(accounts, new ParallelOptions() { MaxDegreeOfParallelism = ThreadCount }, (acc) =>
             {

                 if (logined.Contains(acc.Email))
                 {
                     Console.WriteLine("already logined");
                     return;
                 }
                
                 Console.WriteLine("try " + acc.Email);
                 var proxy = new GetProxy.ProxyReader();
                 string current = null;
                 try
                 {
                      current = proxy.GetList()[0];

                     drivers.InitDriver(false, proxy.GetList()[0]);
                     Pinterest.Pinterest pin = new Pinterest.Pinterest(drivers.Driver);

                     pin.MakeLogin(acc.Email, acc.Password);
                     if (pin.CheckLogin())
                     {
                        pin.SaveCookie(CookieManager.Filename(acc.Email, current));
                        if( pin.ValidName() == false)
                         {
                             pin.FillName();
                         }
                         repeatAccountInOneProxy++;
                         if(repeatAccountInOneProxy > 7)
                         {
                             var list = proxy.GetList();
                             list.Remove(current);
                             File.WriteAllLines(@"C:\my_work_files\pinterest\proxy.txt", list);
                         }
                     }
                     else
                     {
                         //var list = proxy.GetList();
                         //list.Remove(current);
                         //File.WriteAllLines(@"C:\my_work_files\pinterest\proxy.txt", list);
                     }
                   
                 }
                 //catch(WebException)
                 //{

                 //}
                 catch (Exception ex)
                 {
                     var a = ex;
                     var list = proxy.GetList();
                     list.Remove(current);
                     File.WriteAllLines(@"C:\my_work_files\pinterest\proxy.txt", list);
                     Console.WriteLine(acc.Email + ex.Message);
                 }
                 finally
                 {
                     drivers.SuperQuit();

                 }


             });

            
        }
    }
}
