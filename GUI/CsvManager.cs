using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class CsvManager
    {
        private static readonly CsvManager instance = new CsvManager();
        // Explicit static constructor to tell C# compiler  
        // not to mark type as beforefieldinit  
        static CsvManager()
        {
        }
        private CsvManager()
        {
        }
        public static CsvManager Instance
        {
            get
            {
                return instance;
            }
        }

        public Dictionary<string ,string>  GetUrl(string filename)
        {
            
            if (File.Exists(filename))
            {
                List<string> lines = File.ReadAllLines(filename).ToList<string>();
                if (lines.Count > 0 )
                {
                  
                    if (lines[0].Contains("*"))
                    {
                        string[] values = lines[0].Split('*');
                        string url = $"https://www.pinterest.com/pin/create/button/?url={values[1]}&media={values[0]}&description={Defaut(values)}";

                        var d = new Dictionary<string, string>();
                        d[lines[0]] = url;
                        return d;
                    }
                    lines.Remove(lines[0]);
                    File.WriteAllLines(filename, lines);

                }
                //  File.Delete(filename);
                return null;

            }

            return null;

        }

        public void Remove(string filename, string url)
        {
            List<string> lines = File.ReadAllLines(filename).ToList<string>();
            lines.Remove(url);
            File.WriteAllLines(filename, lines);
        }
        private string Defaut(string[] value)
        {

            return (value.Length == 3) ? value[2] : "";

        }

    }
}
