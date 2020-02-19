using AddMeFast;
using GodLoveMe;
using GodLoveMe.Pinterest;
using GodLoveMe.Utils;
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
    public partial class Form1 : Form
    {
        private const string PATH = @"C:\my_work_files\pinterest\full_info_copy.txt";
        private const string ALREADY = @"C:\my_work_files\pinterest\already.txt";
        private int repeatAccountInOneProxy;
        public static List<string> proxieList = File.ReadAllLines(@"C:\my_work_files\pinterest\proxy.txt").ToList();
        CookieManager manager = new CookieManager();


        Thread pinPosterThread;
        Thread proxyThread;
        Thread updatePasswordThread;

        public Form1()
        {
            AccountManager.GetInstance();
            InitializeComponent();

            this.dataGridView1.DataSource = AccountManager.Accounts;
            this.label1.Text = AccountManager.Accounts.Count.ToString();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AccountManager.GetInstance().Save();
            MessageBox.Show("saved");
        }


        private void setGroupBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !string.IsNullOrEmpty(groupNameTxt.Text))
            {
                string name = groupNameTxt.Text.Trim();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    AccountManager.Accounts.First(x => x.Email == row.Cells[0].Value).Group = name;
                    var select = AccountManager.Accounts.First(x => x.Email == row.Cells[0].Value);

                }
                this.dataGridView1.DataSource = AccountManager.Accounts;
            }
        }


        private void proxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] proxies = File.ReadAllLines(@"C:\my_work_files\pinterest\proxy.txt");
            foreach (Account acc in AccountManager.Accounts)
            {
                acc.Proxie = proxies.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
            }
        }

        private void PinMenuItem_Click(object sender, EventArgs e)
        {

            Status status = new Status();
            status.Accounts = new SortableBindingList<Account>(SelectAccount());
            status.Show();
            status.PinStart();

        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {


            Status status = new Status();
            status.Accounts = new SortableBindingList<Account>(SelectAccount());
            status.PinAction = PinAction.Follow;
            status.Show();
            status.PinStart();

        }
        private List<Account> SelectAccount()
        {
            List<Account> doJob = new List<Account>();
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    Account selected = AccountManager.Accounts.Where(a => a.Email == row.Cells[0].Value).FirstOrDefault();
                    if (selected != null)
                    {
                        doJob.Add(selected);
                    }
                }
            }

            return doJob;
        }

        private void SetInfo(string text)
        {
            label1.Invoke((MethodInvoker)delegate
            {
                // Running on the UI thread
                label1.Text = text;
            });
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

        private void prooxyCheckerToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (proxyThread != null)
            {
                proxyThread.Abort();

            }
            proxyThread = new Thread(() =>
            {
                SetInfo("proxy start ");
                Proxy.Checker();
                SetInfo("proxy done ");
            });
            proxyThread.Start();



        }

        private void UpdateMenu_Click(object sender, EventArgs e)
        {

            if (updatePasswordThread != null)
            {
                this.updatePasswordThread.Abort();

            }
            this.updatePasswordThread = new Thread(() =>
            {
                this.SetInfo("start update");
                string[] good = File.ReadAllLines(@"C:\my_work_files\pinterest\res\good.txt");
                List<Account> goodAcc = new List<Account>();
                foreach (string line in good)
                {
                    string[] lineArr = line.Split(':');
                    goodAcc.Add(new Account() { Email = lineArr[0].ToLower(), Password = lineArr[1] });
                }



                for (int i = 0; i < AccountManager.Accounts.Count(); i++)
                {
                    var query = from g in goodAcc
                                where g.Email == AccountManager.Accounts[i].Email
                                select g.Password;
                    if (query.Count() > 0 && AccountManager.Accounts[i].Password != query.LastOrDefault())
                    {
                        AccountManager.Accounts[i].Password = query.LastOrDefault();
                        AccountManager.Accounts[i].Status = "password upadted";
                    }

                }
                this.SetInfo("stop update ");
            });
            this.updatePasswordThread.Start();

        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 1)
                return;

            if (e.ColumnIndex == 1)
            {
                string userName = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                System.Diagnostics.Process.Start("https://pinterest.com/" + userName);
            }
            else if (e.ColumnIndex == 7)

            {
                string userName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                AccountManager.Accounts.Where(x => x.UserName == userName).FirstOrDefault().Proxie = "";
                var asdx = AccountManager.Accounts.Where(x => x.UserName == userName).FirstOrDefault();
                AccountManager.GetInstance().Save();

            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }



        private void checkAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status status = new Status();
            status.Accounts = new SortableBindingList<Account>(SelectAccount());
            status.Show();
            status.AccountCheck();
        }

        private void addAccountToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Thread t = new Thread(() =>
            {
                DriverInstance drivers = new DriverInstance();
                List<Account> newAccount = new List<Account>();
                string[] lines = File.ReadAllLines(@"C:\my_work_files\pinterest\res\good.txt");
                foreach (string line in lines)
                {
                    string[] linedata = line.Split(':');
                    if (newAccount.Any(x => x.Email == linedata[0]))
                    {
                        newAccount.Where(x => x.Email == linedata[0]).FirstOrDefault().Password = linedata[1];
                    }
                    else
                    {
                        newAccount.Add(new Account()
                        {
                            Email = linedata[0].ToLower(),
                            Password = linedata[1],
                        });
                    }

                }
                List<string> already = new List<string>();
                if (File.Exists(ALREADY))
                {
                    already = File.ReadAllLines(ALREADY).ToList();
                }
                newAccount.Reverse();
                foreach (Account acc in newAccount)
                {
                    SetInfo("Start checking" + acc.Email);
                    if (already.Contains(acc.Email) || AccountManager.Accounts.Where(x => x.Email == acc.Email).Any())
                    {
                        SetInfo("skip");
                    }
                    else
                    {
                        drivers.InitDriver(false);
                        Pinterest pin = new Pinterest(drivers.Driver);
                        pin.MakeLogin(acc.Email, acc.Password);
                        if (pin.CheckLogin())
                        {
                            SetInfo("finded checking" + acc.Email);
                            var newAcc = pin.AccountInfo(acc);
                            AccountManager.Accounts.Add(newAcc);
                            AccountManager.GetInstance().Save();
                        }
                        else if (pin.Error != "password reset")
                        {
                            File.AppendAllText(ALREADY, acc.Email + Environment.NewLine);
                        }


                        drivers.SuperQuit();
                    }
                }


            });

            t.Start();
        }

        private void resetStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountManager.Accounts.Select(c => { c.Status = ""; return c; }).ToList();
            AccountManager.GetInstance().Save();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountManager.GetInstance().Save();
            MessageBox.Show("saved");
        }
    }


}

