using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Cookie = OpenQA.Selenium.Cookie;

namespace PinCombain
{
    [Serializable]
    [XmlRoot("base")]
    public class DCookie
    {

        public string Name { get; set; }
        public string Value { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public DateTime? Expiry { get; set; }
        public bool Secure { get; set; }

        public OpenQA.Selenium.Cookie GetCookie()
        {
            Cookie ck = new Cookie(this.Name, this.Value, this.Domain, this.Path, this.Expiry);

            return ck;
        }
    }


  
}
