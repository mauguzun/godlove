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

namespace GodLoveMe.Pinterest
{

    class DriverInst
    {

        public RemoteWebDriver Driver { get; set; }
        public void InitDriver(bool visible = false, string proxy = null)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();

            if (!visible)
                options.AddArguments("headless");
            if (proxy != null)
                options.AddArgument($"--proxy-server={proxy}");  //

            Driver = new ChromeDriver(driverService, options);

        }






    }
}

}
}
