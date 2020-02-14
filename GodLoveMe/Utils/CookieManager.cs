using OpenQA.Selenium;
using PinCombain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AddMeFast
{
    public class CookieManager
    {
        public static  string ACCOUNTS = @"C:\my_work_files\pinterest\acc\";

        public List<DCookie> Load(string filePath)
        {

            List<DCookie> dCookie = null;
            using (var reader = new StreamReader(filePath))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<DCookie>),
                    new XmlRootAttribute("list"));
                dCookie = (List<DCookie>)deserializer.Deserialize(reader);
            }

            return dCookie;



        }
        public static string ProxyFromName(string filePath)
        {
            if (filePath.Contains("_p_"))
            {
                string name =Path.GetFileNameWithoutExtension(filePath);
                var datas = name.Split(new string[] { "_p_" }, StringSplitOptions.None);

                return datas[1].Replace('_', ':');
            }
            return null;
        }

        public static string Filename(string email, string proxy=null)
        {
            if(proxy == null)
            {
                return email;
            }
            return email + "_p_" + proxy.Replace(":", "_");
        }

        public void Save(string filename, ReadOnlyCollection<Cookie> cookies)
        {
            //  var xs = Driver.Manage().Cookies.GetCookieNamed("_auth");
            ///  var cookies = Driver.Manage().Cookies.AllCookies;

            List<DCookie> listDc = new List<DCookie>();
            foreach (OpenQA.Selenium.Cookie cookie in cookies)
            {
                //_auth=1
                var dCookie = new DCookie();
                dCookie.Domain = cookie.Domain;
                dCookie.Expiry = cookie.Expiry;
                dCookie.Name = cookie.Name;
                dCookie.Path = cookie.Path;
                dCookie.Value = cookie.Value;
                dCookie.Secure = cookie.Secure;

                listDc.Add(dCookie);
            }
            XmlSerializer ser = new XmlSerializer(typeof(List<DCookie>), new XmlRootAttribute("list"));

            using (FileStream fs = new FileStream(ACCOUNTS + filename + ".xml", FileMode.Create))
            {
                ser.Serialize(fs, listDc);
            }
        }
    }
}
