using AddMeFast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodLoveMe.Start
{
    public abstract class AllStart
    {
        protected 
              Dictionary<string, int> allreadyTried = new Dictionary<string, int>();

        protected DriverInstance drivers = new DriverInstance();
        protected  CookieManager manager = new CookieManager();
        public int ThreadCount { get; set; } = 3;
        public int Attemps { get; set; } = 15;
        public bool Follow { get; internal set; }

        abstract public void Print();
        abstract public void Start();
    }
}
