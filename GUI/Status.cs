using AddMeFast;
using GodLoveMe;
using GodLoveMe.Pinterest;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Status : Form
    {
        private const string PROXIEFILE = @"C:\my_work_files\pinterest\proxy.txt";
        
        public int limit = 5;
        public bool show = false;
        private bool firstTime = true;

        public static string FOLLOWED = @"C:\Users\mauguzun\Desktop\stat.txt";
        public static string PINNED = "pinned.txt";
        public static string CSV = @"C:\Users\mauguzun\Desktop\om.csv";

        public static List<string> proxieList = File.ReadAllLines(@"C:\my_work_files\pinterest\proxy.txt").ToList();
        public string[] RepinPinList { get; internal set; }
        public SortableBindingList<Account> Accounts { get; set; }
        private PinAction pinAction = PinAction.Pin;
        public static List<string> AlreadyFollowedMyAccount = new List<string>();
        Dictionary<string, int> attemp = new Dictionary<string, int>();

        int startedDriver = 0;
        int stopedDriver = 0;
        int succeAcction = 0;
        public PinAction PinAction
        {
            get
            {
                return pinAction;
            }
            set
            {

                pinAction = value;
                this.Text = pinAction.ToString() + this.Accounts.Count();
                this.Refresh();
            }

        }



        public Status()
        {
            InitializeComponent();

            if (File.Exists(FOLLOWED))
            {
                AlreadyFollowedMyAccount = File.ReadAllLines(FOLLOWED).ToList();
            }
            this.richTextBox.DetectUrls = true;
            this.richTextBox.TextChanged += RichTextBox_TextChanged;
        }

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox.Lines.Count() > 100)
            {
                richTextBox.Text = "";
            }
        }

        public void PinStart()
        {
            Setup();

            AppendTextBox("start ");
            Thread t = new Thread(() =>
            {

                Parallel.ForEach(Accounts, new ParallelOptions() { MaxDegreeOfParallelism = 7 }, (acc) =>
                {
                    DriverInstance drivers = new DriverInstance();
                    this.startedDriver++;
                    try
                    {
                        AppendTextBox(acc.Email + " try login");
                        if (string.IsNullOrEmpty(acc.Proxie))
                        {
                            drivers.InitDriver(show);
                        }
                        else
                        {

                            drivers.InitDriver(show, new GetProxy.ProxyReader().GetList().OrderBy(x => Guid.NewGuid()).FirstOrDefault());

                        }

                        Pinterest pin = new Pinterest(drivers.Driver);


                        pin.MakeLogin(acc.Email, acc.Password);
                        pin.UserName = acc.UserName;
                        if (pin.CheckLogin())
                        {
                            pin.SaveCookie(CookieManager.Filename(acc.Email, acc.Proxie.Replace('_', ':')));
                            if (pin.ValidName() == false)
                            {

                                pin.FillName();
                            }
                            GUI.ActionInfo response = new ActionInfo(false, null);
                            while (true)
                            {
                                Dictionary<string, string> url = null;
                                switch (this.PinAction)
                                {
                                    case PinAction.Follow:
                                        limit = 5;
                                        response = pin.Follow();
                                        break;


                                    case PinAction.Repin:

                                        int d = RepinPinList.Count(); ;
                                        if (RepinPinList.Count() > succeAcction)
                                            response = pin.Repin(RepinPinList[succeAcction], firstTime);
                                        else
                                            this.Close();

                                        firstTime = false;

                                        break;
                                    //
                                    case PinAction.RepinOther:

                                        pin.Driver.Url = "https://www.pinterest.com/";
                                        var pinsElement = pin.Driver.FindElementsByCssSelector("[data-force-refresh]");
                                        List<string> hrefs = new List<string>();

                                        foreach (var item in pinsElement)
                                        {
                                            if (!hrefs.Contains(item.GetAttribute("href")))
                                                hrefs.Add(item.GetAttribute("href"));
                                        }
                                        foreach (var href in hrefs)
                                        {
                                            response = pin.Repin(href, firstTime);
                                            if (response.Done == false)
                                                break;
                                            else
                                                AppendTextBox(href);

                                            firstTime = false;
                                        }
                                        break;



                                    case PinAction.FollowSelf:
                                        var newbies = AccountManager.Accounts.Where(x => x.Followers == 0);
                                        foreach (var item in newbies)
                                        {
                                            response = pin.Follow(item.UserName);
                                            if (response.Done == false)
                                            {
                                                drivers.SuperQuit();
                                            }
                                            else
                                            {
                                                AlreadyFollowedMyAccount.Add(item.UserName);
                                                File.AppendAllLines(FOLLOWED, AlreadyFollowedMyAccount);
                                            }
                                            AppendTextBox(" followed  " + item.UserName);
                                        }

                                        break;

                                    case PinAction.PinCsv:
                                        var urls = CsvManager.Instance;
                                        url = urls.GetUrl(@"C:\Users\mauguzun\Desktop\om.csv");
                                        if (url == null)
                                        {

                                            AppendTextBox("cant find csv line to pinn " + this.PinAction + acc.Email);
                                            drivers.SuperQuit();
                                            this.stopedDriver++;
                                        }
                                        response = pin.MakePost(url[url.Keys.FirstOrDefault()]);
                                        break;

                                    default:
                                        response = pin.MakePost(Form1.pinSite);
                                        break;

                                }
                                if (this.pinAction == PinAction.Follow && response.Done == true)
                                {
                                    int? before = acc.Follow;
                                    DriverInstance temp = new DriverInstance();
                                    temp.InitDriver(false);
                                    acc = CheckOneAccount(acc, temp);
                                    temp.SuperQuit();

                                    if (before == acc.Follow)
                                    {
                                        AppendTextBox("not work " + this.PinAction + acc.Email);
                                        drivers.SuperQuit();
                                        this.stopedDriver++;
                                        break;
                                    }
                                }
                                else if (this.pinAction == PinAction.Pin && response.Done == true)
                                {
                                    File.AppendAllText(PINNED, response.Info + Environment.NewLine);
                                }
                                else if (this.pinAction == PinAction.PinCsv && response.Done == true)
                                {
                                    File.AppendAllText(PINNED, response.Info + Environment.NewLine);
                                    var urls = CsvManager.Instance;
                                    urls.Remove(CSV, url.Keys.FirstOrDefault());
                                }

                                if (attemp.Keys.Contains(acc.Email))
                                {
                                    if (attemp[acc.Email] > limit)
                                    {
                                        AppendTextBox(this.PinAction + "= limit =" + acc.Email);
                                        drivers.SuperQuit();
                                    }
                                    attemp[acc.Email]++;
                                }
                                else
                                {
                                    attemp[acc.Email] = 0;
                                }



                                succeAcction++;
                                AppendTextBox(this.PinAction + " - " + response.Done + " - " + response.Info);
                                acc.Status = this.PinAction + DateTime.Now.ToString();

                                //if(response.Done == false)
                                //{
                                //    break;
                                //}

                            }


                        }
                        else
                        {
                            AppendTextBox(pin.Error + ":" + acc.Email);
                            drivers.SuperQuit();
                            this.stopedDriver++;
                        }
                        this.SetInfo();
                    }

                    catch (Exception ex)
                    {
                        DeleteProxie(acc.Proxie);
                        acc.Proxie = null;
                        AppendTextBox("delete proxy " + acc.Proxie.Replace("_", ":"));
                    }
                    finally
                    {
                        drivers.SuperQuit();

                    }
                });
            });
            t.Start();
        }



        public void DeleteProxie(string proxie)
        {
            try
            {
                var list = File.ReadAllLines(PROXIEFILE).ToList();
                list.Remove(proxie.Replace("_", ":"));
                File.WriteAllLines(PROXIEFILE, list);
            }
            catch { }
        }

        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        public void AppendTextBox(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendTextBox), new object[] { value });
                return;
            }

            richTextBox.Text = DateTime.Now.ToLongTimeString() + ":" + value + "\n" + richTextBox.Text;
            richTextBox.Refresh();
        }

        private void logToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            richTextBox.Visible = true;
            this.dataGridView1.Visible = false;
        }

        private void accountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox.Visible = false;
            this.dataGridView1.Visible = true;
        }



        public void AccountCheck()
        {
            Setup();
            AppendTextBox("start check account");




            Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(Accounts, new ParallelOptions() { MaxDegreeOfParallelism = 14 }, (acc) =>
                {
                    AppendTextBox(acc.Email + " checked ");
                    this.startedDriver++;
                    DriverInstance drivers = new DriverInstance();
                    drivers.InitDriver(false);
                    acc = CheckOneAccount(acc, drivers);
                    drivers.SuperQuit();
                    this.stopedDriver++;
                    this.succeAcction++;

                    acc.Status = this.PinAction + DateTime.Now.ToString();
                    labelInfo.Text = Accounts.Count + "/start " + this.startedDriver + "/ stop " + this.stopedDriver + "/ good  " + this.succeAcction;

                });

            });

            AppendTextBox("done ");
        }

        private Account CheckOneAccount(Account acc, DriverInstance drivers)
        {

            AppendTextBox("check" + acc.Email);
            try
            {
                Pinterest pin = new Pinterest(drivers.Driver);
                acc = pin.AccountInfo(acc);
            }

            catch (Exception ex)
            {
                acc.Status = "account not exist";
                AccountManager.GetInstance().Save();
            }


            return acc;
        }

        public void LoadFromString()
        {

        }
        private void SetInfo()
        {
            labelInfo.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                labelInfo.Text = Accounts.Count + "/start " + this.startedDriver + "/ stop " + this.stopedDriver + "/ good  " + this.succeAcction;
            });
        }

        private void Setup()
        {
            this.dataGridView1.DataSource = Accounts;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.AutoResizeColumns();
            this.dataGridView1.Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {

                this.dataGridView1.Columns[i].Visible = false;
            }
            this.dataGridView1.Columns[0].Visible = true;
            this.dataGridView1.Columns[1].Visible = true;
            this.dataGridView1.Columns[10].Visible = true;
        }

        private void Status_Load(object sender, EventArgs e)
        {

        }
    }
}
