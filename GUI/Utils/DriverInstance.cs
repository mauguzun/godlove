using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using PinCombain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AddMeFast
{
    public class DriverInstance
    {
        public RemoteWebDriver Driver { get; set; }
        public string Account { get; set; }
        public void InitDriver(bool visible = false, string proxy = null)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();

            if (!visible)
                options.AddArguments("headless");
            if (proxy != null)
                options.AddArgument($"--proxy-server={proxy}");  //

            var user_agent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.50 Safari/537.36";



            options.AddArgument("--disable-infobars"); // disabling infobars
            options.AddArgument($"--user-agent={user_agent}"); // disabling infobars
               options.AddArgument("--disable-extensions"); // disabling extensions
            options.AddArgument("--disable-gpu"); // applicable to windows os only
            options.AddArgument("--disable-dev-shm-usage"); // overcome limited resource problems
            options.AddArgument("--no-sandbox"); // Bypass OS security model


            Driver = new ChromeDriver(driverService, options);



        }
        public void Save()
        {
            var xs = Driver.Manage().Cookies.GetCookieNamed("_auth");
            var cookies = Driver.Manage().Cookies.AllCookies;

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

            using (FileStream fs = new FileStream("data/" + Account + ".xml", FileMode.Create))
            {
                ser.Serialize(fs, listDc);
            }
        }

        public void SuperQuit()
        {
            try
            {
                this.Driver.Close();
                this.Driver.Quit();
                this.Driver.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(" on quit " + ex.Message);
            }
        }

        public void MakeLogin(string filePath)
        {

            List<DCookie> dCookie;
            using (var reader = new StreamReader(filePath))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<DCookie>),
                    new XmlRootAttribute("list"));
                dCookie = (List<DCookie>)deserializer.Deserialize(reader);
            }

            foreach (var cookie in dCookie)
            {
                Driver.Manage().Cookies.AddCookie(cookie.GetCookie());
            }


        }

    }
}
