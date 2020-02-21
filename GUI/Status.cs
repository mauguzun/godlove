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
        private bool show = false;
        static  string FOLLOWED = @"C:\Users\mauguzun\Desktop\stat.txt";
        public static List<string> proxieList = File.ReadAllLines(@"C:\my_work_files\pinterest\proxy.txt").ToList();
        public SortableBindingList<Account> Accounts { get; set; }
        private PinAction pinAction = PinAction.Pin;
        public static List<string> AlreadyFollowedMyAccount = new List<string>();

      
        int startedDriver = 0;
        int stopedDriver = 0;
        public PinAction PinAction
        {
            get
            {
                return pinAction;
            }
            set
            {

                pinAction = value;
                this.Text = pinAction.ToString();
                this.Refresh();
            }

        }


        public Status()
        {
            InitializeComponent();
            
            if(File.Exists(FOLLOWED))
            {
                AlreadyFollowedMyAccount = File.ReadAllLines(FOLLOWED).ToList();
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
                            drivers.InitDriver(show, acc.Proxie.Replace("_", ":"));

                        }

                        Pinterest pin = new Pinterest(drivers.Driver);

                      
                        pin.MakeLogin(acc.Email, acc.Password);
                        if (pin.CheckLogin())
                        {
                            pin.SaveCookie(CookieManager.Filename(acc.Email, acc.Proxie.Replace('_', ':')));
                            if (pin.ValidName() == false)
                            {

                                pin.FillName();
                            }
                            var response = true;
                            while (true)
                            {
                                switch (this.PinAction)
                                {
                                    case PinAction.Follow:
                                        response = pin.Follow();
                                        break;


                                    case PinAction.Repin:
                                        response = pin.Repin();
                                        break;


                                    case PinAction.FollowSelf:
                                        var newbies  = AccountManager.Accounts.Where(x => x.Followers == 0);
                                        foreach (var item in newbies)
                                        {
                                            response = pin.Follow(item.UserName);
                                            if(response == false)
                                            {
                                                drivers.SuperQuit();
                                            }
                                            else
                                            {
                                                AlreadyFollowedMyAccount.Add(item.UserName);
                                                File.AppendAllLines(FOLLOWED, AlreadyFollowedMyAccount);
                                            }
                                           AppendTextBox(this.PinAction  + " - " + item.UserName);
                                        }
                                       
                                        break;

                                    default:
                                        response = pin.MakePost();
                                        break;

                                }
                                if (this.pinAction == PinAction.Follow)
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
                                AppendTextBox(this.PinAction  + " - " + acc.Proxie   + " - " + acc.Email);
                                acc.Status = this.PinAction + DateTime.Now.ToString();

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
                proxieList.Remove(proxie.Replace("_", ":"));
                File.WriteAllLines(@"C:\my_work_files\pinterest\proxy.txt", proxieList);
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
                Parallel.ForEach(Accounts, new ParallelOptions() { MaxDegreeOfParallelism = 12 }, (acc) =>
                {
                    this.startedDriver++;
                    DriverInstance drivers = new DriverInstance ();
                    drivers.InitDriver(false);
                    acc = CheckOneAccount(acc,drivers);
                    drivers.SuperQuit();
                    this.stopedDriver++;
                    AppendTextBox(acc.Email + " checked ");
                });

            });
         
            AppendTextBox("done ");
        }

        private Account CheckOneAccount(Account acc, DriverInstance drivers)
        {
            
            AppendTextBox("account start proxy " + acc.Email);
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
                labelInfo.Text = Accounts.Count + "/" + this.startedDriver + "/" + this.stopedDriver;
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
    }
}
