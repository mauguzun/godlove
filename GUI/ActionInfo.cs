using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    public class ActionInfo
    {
        public bool Done { get; set; }
        public string Info { get; set; }
        public string Desc { get; set; }

        public ActionInfo(bool done, string info)
        {
            this.Done = done;
            this.Info = info;
        }
    }
}
