using AddMeFast;
using GodLoveMe.Utils;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using PinCombain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GodLoveMe.Pinterest
{
    class Pinterest
    {
        public string AccountPath { get; set; } = "";
        public string Email { get; set; } = "email";
        public string UserName { get; set; } = null;
        public CookieManager CookieManager { get; set; } = new CookieManager();
        public RemoteWebDriver Driver { get; set; }


        public Pinterest(RemoteWebDriver driver)
        {
            this.Driver = driver;


        }

        public void MakeLoginWithCookie(List<DCookie> dCookie)
        {

            try
            {
                Driver.Url = "https://pinterest.com/";
                foreach (var cookie in dCookie)
                {
                    Driver.Manage().Cookies.AddCookie(cookie.GetCookie());
                }
            }
            catch { }


        }

        public void MakeLogin(string email, string password)
        {

            try
            {
                Driver.Url = "https://pinterest.com/login";
                Driver.FindElementById("password").SendKeys(password);
                Driver.FindElementById("email").SendKeys(email);


                Driver.FindElementByCssSelector("div[data-test-id='registerFormSubmitButton']").Click();
                Thread.Sleep(new TimeSpan(0, 0, 25));

                var labels = Driver.FindElementsByCssSelector("label[for='password']");


            }
            catch
            {

            }


        }

        internal string GetUserName()
        {
            string accountName = null;
            this.Driver.Url = "https://www.pinterest.com/settings#profile";
            return this.Driver.FindElementById("username").GetAttribute("value");
        }

        public bool CheckLogin()
        {

            try
            {
                if (Driver.FindElementsByCssSelector("#password-error").Count != 0)
                {
                    Driver.FindElementByCssSelector("#password-error button").Click();
                    Console.WriteLine("password reset");
                }
                Driver.Url = "https://pinterest.com";
                Driver.Url = "https://pinterest.com";
                if (Driver.FindElementsByCssSelector("[aria-label='Your profile']").Count() != 0)
                {

                    return true;
                }
                return false;
            }
            catch  { return false; }



           
            
        }
        public void SaveCookie(string filename)
        {
            var cookies = this.Driver.Manage().Cookies.AllCookies;
            this.CookieManager.Save(filename, cookies);

        }



        public bool MakePost()
        {
            Driver.Url = "http://drum.nl.eu.org/get";
            //
            Thread.Sleep(new TimeSpan(0, 0, 3));
            var search = Driver.FindElementsByCssSelector("#HeaderContent");
            var boards = Driver.FindElementsByCssSelector("div[data-test-id='boardWithoutSection']");
            if (search.Count == 0)
            {
                return false;
            }
            else if (boards.Count == 0 && search.Count != 0)
            {

                var ss = Driver.GetScreenshot();
                ss.SaveAsFile("omg.png", ScreenshotImageFormat.Png);


                Console.WriteLine("not boards " + this.Email);
                Console.WriteLine("https://pinterest.com/" + this.UserName);
                CreateBoard();
                return true;
            }
            foreach (var board in boards)
            {
                try
                {
                    board.Click();
                    Thread.Sleep(new TimeSpan(0, 0, 7));
                    Console.WriteLine("pinned" + DateTime.Now.ToLongTimeString() + " " + this.Email);
                    Console.WriteLine("https://pinterest.com/" + this.UserName);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + " " + this.Email);

                }

            }
            return true;
        }

        public void Follow()
        {
            Driver.Url = "https://www.pinterest.com/search/boards/?q=" + RandomValue.GetString(@"Data/city_names.txt") + "&rs=filter";

            var buttons = Driver.FindElementsByCssSelector("[data-test-id='board-follow-button'] button");

            ///todo check if have different
            foreach (var button in buttons)
            {
                try
                {

                    button.Click();

                }
                catch
                {
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                    Driver.FindElementByTagName("body").SendKeys(OpenQA.Selenium.Keys.ArrowDown);
                }


            }
        }

        public bool ValidName()
        {
            try
            {
                var asadd = Driver.FindElementByCssSelector("#initial-state");
                string json = Driver.FindElementByCssSelector("#initial-state").GetAttribute("innerHTML");
                JObject o = JObject.Parse(json);

                string name = o["viewer"]["fullName"].ToString();
                if (name.Contains('.') | name.Contains('+'))
                {
                    return false;
                }
                return true;
            }
            catch { return true; }


        }

        public void FillName()
        {
            try
            {
                Driver.Url = "https://www.pinterest.com/settings#profile";

                //  Driver.FindElementById("location").SendKeys(new RandomValue().GetString("city_names.txt"));


                for (int i = 0; i < 50; i++)
                {
                    Driver.FindElementById("first_name").SendKeys(Keys.Backspace);
                    Driver.FindElementById("last_name").SendKeys(Keys.Backspace);
                }

                Driver.FindElementById("first_name").SendKeys(RandomValue.GetString("Data/names.txt"));
                Driver.FindElementById("last_name").SendKeys(RandomValue.GetString("Data/names.txt"));
                Driver.FindElementById("first_name").SendKeys(Keys.Space);




                var buttons = Driver.FindElementsByTagName("button");
                foreach (var button in buttons)
                {
                    if (button.Text.Trim().ToUpper() == "DONE")
                    {
                        Actions actions = new Actions(Driver);
                        actions.MoveToElement(Driver.FindElementByCssSelector("div[data-test-id='settings-header']"));
                        actions.Perform();
                        button.Click();
                        Thread.Sleep(new TimeSpan(0, 0, 5));
                        Console.WriteLine("new name done");
                        return;
                    }
                }
            }

            catch (Exception ex)
            {
                var sdfs = ex.Message;
                return;
            }
        }

        public bool CreateBoard()
        {
            try
            {

                Driver.Url = $"https://www.pinterest.com/{this.UserName}/boards/";
                var buttons = Driver.FindElementsByCssSelector(".fixedHeader button");
                buttons[0].Click(); Thread.Sleep(new TimeSpan(0, 0, 5));

                Driver.FindElementByCssSelector("div[title='Create board']").Click(); Thread.Sleep(new TimeSpan(0, 0, 5));
                Driver.FindElementById("boardEditName").SendKeys(RandomValue.GetString(@"Data/city_names.txt"));
                buttons = Driver.FindElementsByTagName("button");
                foreach (var button in buttons)
                {
                    if (button.Text.Trim().ToUpper() == "CREATE")
                    {

                        button.Click();
                        Thread.Sleep(new TimeSpan(0, 0, 5));
                        Console.WriteLine("board done" + this.UserName);
                        Console.WriteLine("https://pinterest.com/" + this.UserName);
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }

        }


        public void AddImage()
        {


            var buttons = Driver.FindElementsByTagName("button");
            foreach (var item in buttons)
            {
                var xt = item.Text;
                if (item.Text.Contains("Change"))
                {

                    try
                    {

                        OpenQA.Selenium.Interactions.Actions action = new OpenQA.Selenium.Interactions.Actions(Driver);
                        action.MoveToElement(item);
                        item.Click();
                        Thread.Sleep(new TimeSpan(0, 0, 5));

                        IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                        string count = (string)js.ExecuteScript("document.querySelector('input[type=file]').setAttribute('style', 'display:block');");

                        var image = Driver.FindElementByCssSelector("input[type=file]");
                        if (image != null && image.Enabled && image.Displayed)
                        {
                            var x = new ImgRep();
                            x.LoadImages();
                            string imgPath = x.Random();
                            image.SendKeys(imgPath);
                            Thread.Sleep(new TimeSpan(0, 0, 15));
                            Console.WriteLine("img added");
                            // x.Delete(imgPath);
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("exception on imnage iploade", ex.Message);
                    }
                    //item.Click();
                }
            }

        }

    }
}

