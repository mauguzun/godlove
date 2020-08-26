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
        private const string Path = @"C:\my_work_files\pinterest\proxy.txt";

        public static void Checker()
        {
           
            int tested = 0;

            string[] dirty = File.ReadAllLines(Path);
            File.Delete(Path);
            Parallel.ForEach(dirty, new ParallelOptions() { MaxDegreeOfParallelism = 95 }, (row) =>
             {

                 WebClient wc = new WebClient();
                 string[] hostPort = row.Split(':');
                 wc.Proxy = new WebProxy(hostPort[0], Int32.Parse(hostPort[1]));
                 try
                 {
                     string f = wc.DownloadString(new Uri("https://www.pinterest.com/"));
                     File.AppendAllText(Path, row+Environment.NewLine);
                    
                 }
                 catch {}
                 finally { tested++; }
            
               
             });

             

        }



    }
}
