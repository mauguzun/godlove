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
    public class AppnedFfromFileLoginPassword : AllStart
    {
        string path = null;
        public override void Print()
        {
            
        }

        public override void Start()
        {
            DriverInstance drivers = new DriverInstance();
            drivers.InitDriver(true);
            Pinterest.Pinterest pin = new Pinterest.Pinterest(drivers.Driver);


            Console.WriteLine("url to file");
            this.path = Console.ReadLine();
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                Console.WriteLine(lines.Count());
                foreach(string line in lines)
                {
                    string []para = line.Split(':');
                    pin.MakeLogin(para[0], para[1]);
                    if (pin.CheckLogin())
                    {
                        if (pin.ValidName() == false)
                        {
                            pin.FillName();
                            try { pin.AddImage(); }
                            catch { }
                        }


                        File.AppendAllText(@"C:\my_work_files\pinterest\source_all_account_for_blaster.txt",
                            Path.GetFileNameWithoutExtension(path) + ":trance_333:" + pin.GetUserName() + Environment.NewLine);

                    }

                }
                File.Delete(path);
            }
        }
    }
}
