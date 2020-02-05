using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace GodLoveMe.Utils
{
    public class Proxy
    {

        public static void Checker()
        {
           
            int tested = 0;

            string[] dirty = File.ReadAllLines(@"C:\my_work_files\pinterest\proxy.txt");
            File.Delete(@"Data/Clear.txt");
            Parallel.ForEach(dirty, new ParallelOptions() { MaxDegreeOfParallelism = 75 }, (row) =>
             {

                 WebClient wc = new WebClient();
                 string[] hostPort = row.Split(':');
                 wc.Proxy = new WebProxy(hostPort[0], Int32.Parse(hostPort[1]));
                 try
                 {
                     string f = wc.DownloadString(new Uri("https://google.com/"));
                     File.AppendAllText(@"Data/Clear.txt", row+Environment.NewLine);
                     Console.WriteLine("ok " + tested.ToString() + " / "+ dirty.Count() );
                    
                 }
                 catch {}
                 finally { tested++; }
            
               
             });

            Console.WriteLine("proxy done");

        }



    }
}
