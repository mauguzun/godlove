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
        public static List<string> proxieList = File.ReadAllLines(@"C:\my_work_files\pinterest\proxy.txt").ToList();
        Thread main;
        public SortableBindingList<Account> Accounts { get; set; }
        public PinAction PinAction { get; set; } = PinAction.Pin;

        
        public Status()
        {
            InitializeComponent();


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
                    try
                    {
                        AppendTextBox(acc.Email + " try login");
                        if (string.IsNullOrEmpty(acc.Proxie))
                        {
                            drivers.InitDriver(false);
                        }
                        else
                        {
                            drivers.InitDriver(false, acc.Proxie.Replace("_", ":"));

                        }

                        Pinterest pin = new Pinterest(drivers.Driver);

                        //if (acc.Cookie == null)
                        //{
                        pin.MakeLogin(acc.Email, acc.Password);

                        //}
                        //else
                        //{
                        //    pin.MakeLoginWithCookie(manager.Load(acc.Cookie));

                        //}
                        if (pin.CheckLogin())
                        {
                            pin.SaveCookie(CookieManager.Filename(acc.Email, acc.Proxie.Replace('_', ':')));


                            if (pin.ValidName() == false)
                            {

                                pin.FillName();
                            }


                            var response = true;
                            while (pin.MakePost())
                            {
                                switch (this.PinAction)
                                {
                                    case PinAction.Follow:
                                        response = pin.Follow();
                                        break;

                                    default:
                                        response = pin.MakePost();
                                        break;

                                }
                                AppendTextBox(this.PinAction + acc.Email);
                                acc.Status = this.PinAction + DateTime.Now.ToString();
                            }
                            //in.Follow();
                            // MakePin(acc, pin);

                        }
                        else
                        {
                            AppendTextBox(pin.Error + ":" + acc.Email);
                            drivers.SuperQuit();
                        }

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

            
            AppendTextBox("start ");



            //Thread t = new Thread(() =>
            //{
            Task.Factory.StartNew(() =>
            {
                Parallel.ForEach(Accounts, new ParallelOptions() { MaxDegreeOfParallelism = 12 }, (acc) =>
                {
                    DriverInstance drivers = new DriverInstance();
                    AppendTextBox("account start proxy " + acc.Email);
                    try
                    {
                        drivers.InitDriver(false);
                        Pinterest pin = new Pinterest(drivers.Driver);
                        acc = pin.AccountInfo(acc);
                       
                        drivers.SuperQuit();
                    }

                    catch (Exception ex)
                    {
                        acc.Status = "account not exist";
                        AccountManager.GetInstance().Save();
                        drivers.SuperQuit();
                    }
                    finally
                    {
                        drivers.SuperQuit();
                     


                    }
                });

            });
            //});
            //t.Start(); 
            AppendTextBox("done ");
        }


        public void LoadFromString()
        {

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
