using AddMeFast;
using GodLoveMe.Pinterest;
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

namespace GUI
{
    public class Pinterest
    {
        public string AccountPath { get; set; } = "";
        public string Email { get; set; } = "email";
        public string UserName { get; set; } = null;
        public CookieManager CookieManager { get; set; } = new CookieManager();
        public RemoteWebDriver Driver { get; set; }
        public string Error { get; private set; }

        public Pinterest(RemoteWebDriver driver)
        {
            this.Driver = driver;


        }

        public void MakeLogin(List<DCookie> dCookie)
        {

            try
            {
                Driver.Url = "https://" + dCookie[0].Domain;
                Driver.Manage().Cookies.DeleteAllCookies(); //Delete all of them
                foreach (var cookie in dCookie)
                {
                    Driver.Manage().Cookies.AddCookie(cookie.GetCookie());
                }


                Driver.Url = "https://" + dCookie[0].Domain;
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
                    this.Error = "password reset";
                    return false;
                }
                else if (Driver.FindElementsByCssSelector("[aria-label='Reset password']").Count != 0)
                {
                    Driver.FindElementByCssSelector("[aria-label='Reset password']").Click();
                    this.Error = "password reset";
                    return false;
                }
                Driver.Url = "https://pinterest.com";
                Driver.Url = "https://pinterest.com";
                if (Driver.FindElementsByCssSelector("#HeaderContent").Count() != 0)
                {

                    return true;
                }
                return false;
            }
            catch(Exception e) {
                var s = e.Message;
                return false; }





        }

         

        public ActionInfo Repin(string url,bool firstTime = false)
        {
            
            this.Driver.Url = url;
            if(firstTime)
            {
                Driver.Navigate();
            }
            Thread.Sleep(new TimeSpan(0, 0, 3));

            if (Driver.FindElementsByCssSelector(".experienceSystemPushOverlay").Count != 0)
            {
                Driver.FindElementsByCssSelector(".experienceSystemPushOverlay")[0].Click();

            }
            var save = Driver.FindElementsByCssSelector("[data-test-id='SaveButton']");
            if (save.Count() == 0)
            {
                save = Driver.FindElementsByCssSelector("[data-test-id='PinBetterSaveButton']");
                if(save.Count != 0)
                {
                    save[0].Click();
                    return new ActionInfo(true, url);
                }
            }


            if (save.Count() > 0)
            {


                save[0].Click();
                Thread.Sleep(new TimeSpan(0, 0,3));

                var boards = Driver.FindElementsByCssSelector("div[data-test-id='boardWithoutSection']");
                if (boards.Count() > 0)
                {
                    boards[0].Click();
                    Thread.Sleep(new TimeSpan(0, 0, 4));
                    return new ActionInfo(true, "repined");
                }

            }
            return new ActionInfo(false, "repined");
        }



        public void SaveCookie(string filename)
        {

            this.CookieManager.Save(filename, Driver.Manage().Cookies.AllCookies);

        }



        public ActionInfo MakePost()
        {
           
          
            Driver.Url = Form1.pinSite;
            //
            Thread.Sleep(new TimeSpan(0, 0, 3));
            var search = Driver.FindElementsByCssSelector("#HeaderContent");
            var boards = Driver.FindElementsByCssSelector("div[data-test-id='boardWithoutSection']");
            if (search.Count == 0)
            {
                return new ActionInfo(false, "Not logined ?");

            }
            else if (boards.Count == 0 && search.Count != 0)
            {

                var ss = Driver.GetScreenshot();
                ss.SaveAsFile("omg.png", ScreenshotImageFormat.Png);



                CreateBoard();
                return new ActionInfo(true, "We make new board ! ");

            }
            foreach (var board in boards)
            {
                try
                {
                    board.Click();
                    Thread.Sleep(new TimeSpan(0, 0, 7));
                    var pinnned = Driver.FindElementsByCssSelector("[data-test-id='seeItNow'] a");
                    var modal = Driver.FindElementsByCssSelector("[data-test-id='error-modal']");


                    if (pinnned.Count() > 0)
                    {


                        return new ActionInfo(true, pinnned[0].GetAttribute("href"));

                    }
                    else if (modal.Count() > 0)
                    {

                        return new ActionInfo(false, modal[0].Text);
                    }

                    return new ActionInfo(false, "Extra false"); ;
                }
                catch (Exception ex)
                {


                    return new ActionInfo(true, ex.Message); ;

                }

            }
            return new ActionInfo(false, "Extra false"); ;
        }

        public ActionInfo Follow(string nickname)
        {
            Driver.Url = "https://www.pinterest.com/" + nickname;
            var buttons = Driver.FindElementsByCssSelector("div");
            if (buttons.Count() > 0)
            {
                foreach (var button in buttons)
                {
                    if (button.Text.Trim().ToLower() == "follow")
                    {
                        button.Click();
                        return new ActionInfo(true, "followed ");

                    }
                }
            }

            return new ActionInfo(false, "");
        }

        public ActionInfo Follow()
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
            return new ActionInfo(true, "followed not checked");
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

        public Account AccountInfo(Account acc)
        {
            try
            {
                if (acc.UserName == null)
                {
                    Driver.Url = "https://www.pinterest.com/settings#profile";
                    acc.UserName = Driver.FindElementById("username").GetAttribute("value");
                    acc.FullName = Driver.FindElementById("first_name").GetAttribute("value") + " " +
                        "" + Driver.FindElementById("last_name").GetAttribute("value");
                    return acc;
                }
                else
                {
                    Driver.Url = "https://pinterest.com/" + acc.UserName;
                }

                var asadd = Driver.FindElementByCssSelector("#initial-state");
                string json = Driver.FindElementByCssSelector("#initial-state").GetAttribute("innerHTML");
                JObject o = JObject.Parse(json); ;

                acc.FullName = o["resourceResponses"][0]["response"]["data"]["user"]["full_name"].ToString();
                acc.Follow = Int32.Parse(o["resourceResponses"][0]["response"]["data"]["user"]["following_count"].ToString());
                acc.Followers = Int32.Parse(o["resourceResponses"][0]["response"]["data"]["user"]["follower_count"].ToString());

                // acc.UserName = o["resourceResponses"][0]["response"]["data"]["user"]["user_name"].ToString();
                acc.Boards = o["resourceResponses"][0]["response"]["data"]["user"]["board_count"].ToString();


                return acc;
            }
            catch
            {
                acc.Status = "deleted";
            }
            return acc;
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

                Driver.FindElementById("first_name").SendKeys(RandomValue.GetString("Data/names.txt").ToLower());
                Driver.FindElementById("last_name").SendKeys(RandomValue.GetString("Data/names.txt").ToLower());
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
                        break;

                    }
                }
                AddImage();
                return;
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
                Thread.Sleep(new TimeSpan(0, 0, 2));
                var buttons = Driver.FindElementsByCssSelector("[data-test-id='createBoardCard']");
                buttons[0].Click();
                Thread.Sleep(new TimeSpan(0, 0, 5));

                //Driver.FindElementByCssSelector("div[title='Create board']").Click(); 
                // Thread.Sleep(new TimeSpan(0, 0, 5));
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
            catch (Exception ex)
            {
                var d = ex.Message;
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

