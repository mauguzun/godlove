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
    class AppendUsers : AllStart
    {
        public override void Print()
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {

            DriverInstance drivers = new DriverInstance();
            CookieManager manager = new CookieManager();

            string[] lines = Directory.GetFiles(@"C:\my_work_files\pincombain\PinCombain\bin\Debug\data");
            Console.Title = lines.Count().ToString();
            Console.WriteLine("start");


            Parallel.ForEach(lines, new ParallelOptions() { MaxDegreeOfParallelism =7},
              (path) =>
             {
                 if (path.Contains(".xml"))
                 {
                     Console.WriteLine(Directory.GetFiles(@"C:\my_work_files\pincombain\PinCombain\bin\Debug\data").Count() + " files ");
                     drivers.InitDriver(false);
                     Console.WriteLine(path);
                     Pinterest.Pinterest pin = new Pinterest.Pinterest(drivers.Driver);
                     pin.MakeLoginWithCookie(manager.Load(path));
                     if (pin.CheckLogin())
                     {
                         if (pin.ValidName() == false)
                         {
                             pin.FillName();

                         }
                         try { pin.AddImage(); }
                         catch { }
                         while (true)
                         {
                             try
                             {
                                 File.AppendAllText(@"C:\my_work_files\pinterest\source_all_account_for_blaster.txt",
                          Path.GetFileNameWithoutExtension(path) + ":trance_333:" + pin.GetUserName() + Environment.NewLine);
                                 break;
                             }
                             catch { }
                         }


                     }

                 }
                 File.Delete(path);
                 drivers.SuperQuit();
             });

        }


    }
}
