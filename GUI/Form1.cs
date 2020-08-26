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
        private const string ALREADY = @"C:\my_work_files\pinterest\already.txt";
        private int repeatAccountInOneProxy;
        public static List<string> proxieList = File.ReadAllLines(@"C:\my_work_files\pinterest\proxy.txt").ToList();
        CookieManager manager = new CookieManager();


        Thread pinPosterThread;
        Thread proxyThread;
        Thread updatePasswordThread;
        private List<Account> filteredAccounts;
        public static bool show = false;
        public static string pinSite = "http://drum.nl.eu.org/get";

        public Form1()
        {
            AccountManager.GetInstance();
            InitializeComponent();

            this.dataGridView1.DataSource =  new SortableBindingList<Account>(  AccountManager.Accounts ) ;
            this.labelCount.Text = AccountManager.Accounts.Count.ToString();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.AutoResizeColumns();
            this.dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            pinDomainTxt.Text = pinSite;
        }



        private void setGroupBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !string.IsNullOrEmpty(groupNameTxt.Text))
            {
                string name = groupNameTxt.Text.Trim();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (AccountManager.Accounts.Where(x => x.Email == row.Cells[0].Value).Any())
                    {
                        AccountManager.Accounts.FirstOrDefault(x => x.Email == row.Cells[0].Value).Group = name;
                        var select = AccountManager.Accounts.First(x => x.Email == row.Cells[0].Value);
                    }

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
            status.show = show;
            status.Accounts = new SortableBindingList<Account>(SelectAccount());
            status.PinAction = PinAction.Pin;
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
            labelCount.Invoke((MethodInvoker)delegate
            {
                labelCount.Text = AccountManager.Accounts.Count.ToString();
            });
            labelStatus.Invoke((MethodInvoker)delegate
            {
                labelStatus.Text = text;
            });
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

        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
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
            status.PinAction = PinAction.CheckName;
            status.show = show;
            status.AccountCheck();
        }

        private void addAccountToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Thread t = new Thread(() =>
            {

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

                newAccount = newAccount.Where(p => !already.Any(p2 => p2 == p.Email)).ToList();
                newAccount = newAccount.Where(p => !AccountManager.Accounts.Any(p2 => p2.Email == p.Email)).ToList();


                SetInfo("Account count for test " + newAccount.Count());
                newAccount.Reverse();
                Parallel.ForEach(newAccount, new ParallelOptions() { MaxDegreeOfParallelism = 7 }, (acc) =>
                   {
                       DriverInstance drivers = new DriverInstance();
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
                               //      SetInfo("finded checking" + acc.Email);
                               var newAcc = pin.AccountInfo(acc);
                               AccountManager.Accounts.Add(newAcc);
                     
                               AccountManager.GetInstance().Save();

                           }
                           else if (pin.Error != "password reset")
                           {
                               try { File.AppendAllText(ALREADY, acc.Email + Environment.NewLine); }
                               catch { }

                           }



                       }
                       drivers.SuperQuit();
                   });
                

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

            if (!String.IsNullOrEmpty(textBoxQuery.Text.Trim()))
            {
                SetInfo("Update filtered account");
                foreach (var acc in this.filteredAccounts)
                {
                    AccountManager.Accounts.Where(x => x.Email == acc.Email).FirstOrDefault().Follow = acc.Follow;
                    AccountManager.Accounts.Where(x => x.Email == acc.Email).FirstOrDefault().Followers = acc.Followers;
                    AccountManager.Accounts.Where(x => x.Email == acc.Email).FirstOrDefault().FullName = acc.FullName;
                }
            }
            AccountManager.GetInstance().Save();
            MessageBox.Show("saved");
        }

        private void clearProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {


            SelectAccount().Select(c => { c.Proxie = ""; return c; }).ToList();
            AccountManager.GetInstance().Save();
        }

        private void rePinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void followToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status status = new Status();
            status.Accounts = new SortableBindingList<Account>(SelectAccount());
            status.PinAction = PinAction.Follow;
            status.Show();
            status.show = show;
            status.PinStart();
        }

        private void followWhere0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status status = new Status();
            status.Accounts = new SortableBindingList<Account>(SelectAccount());
            status.PinAction = PinAction.FollowSelf;
            status.Show();
            status.show = show;
            status.PinStart();
        }

        private void updatePasswordToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void textBoxQuery_TextChanged(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(textBoxQuery.Text.Trim()))
            {
                dataGridView1.DataSource = AccountManager.Accounts;
            }
            else
            {
                var x = comboBoxColumn.SelectedItem == null ? "UserName" : comboBoxColumn.SelectedItem.ToString();
                x = x.Trim();
                this.filteredAccounts = AccountManager.Accounts.ToList().Where(a => a.GetType().GetProperty(x).GetValue(a).ToString().Contains(textBoxQuery.Text.Trim())).ToList();
                dataGridView1.DataSource = this.filteredAccounts;
            }
        }

        private void renameAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(() =>
            {
                this.SetInfo("start rename ");
                Parallel.ForEach(SelectAccount(), new ParallelOptions() { MaxDegreeOfParallelism = 3 }, (acc) =>
                {

                    acc.Proxie = null;
                    DriverInstance instance = new DriverInstance();
                    this.SetInfo("start rename " + acc.UserName);
                    if (string.IsNullOrEmpty(acc.Proxie))
                    {
                        instance.InitDriver(false);
                    }
                    else
                    {
                        instance.InitDriver(false, acc.Proxie.Replace("_", ":"));

                    }
                    var pin = new Pinterest(instance.Driver);

                    pin.MakeLogin(acc.Email, acc.Password);
                    if (pin.CheckLogin())
                    {
                        this.SetInfo("cant logined  " + acc.UserName);
                        pin.FillName();
                        var accountNewName = pin.AccountInfo(acc);
                        AccountManager.Accounts.Where(x => x.Email == acc.Email).FirstOrDefault().FullName = accountNewName.FullName;
                        AccountManager.GetInstance().Save();
                    }
                    else
                    {
                        this.SetInfo("cant login  " + acc.UserName);
                    }
                });
            });
            t.Start();

        }

        private void FolloMenu_Click(object sender, EventArgs e)
        {

        }

        private void addXmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    foreach (var path in files)


                    {
                        if (path.Contains(".xml"))
                        {
                            DriverInstance drivers = new DriverInstance();
                            drivers.InitDriver(false);
                            Console.WriteLine(path);
                            Pinterest pin = new Pinterest(drivers.Driver);
                            pin.MakeLogin(manager.Load(path));
                            if (!pin.CheckLogin())
                            {
                                pin.MakeLogin(Path.GetFileNameWithoutExtension(path), "trance_333");
                            }

                            if (pin.CheckLogin())
                            {
                                if (pin.ValidName() == false)
                                {
                                    pin.FillName();

                                }
                                try { pin.AddImage(); }
                                catch { }


                                Account acc = new Account();
                                acc.Email = Path.GetFileNameWithoutExtension(path);
                                acc.Password = "trance_333";
                                acc = pin.AccountInfo(acc);
                                AccountManager.Accounts.Add(acc);
                                try
                                {
                                    AccountManager.GetInstance().Save();
                                }
                                catch
                                { }


                            }
                            drivers.SuperQuit();
                        }
                        File.Delete(path);

                    };
                }
            }
        }

        private void optionsToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {

        }

        private void visbleBox_TextChanged(object sender, EventArgs e)
        {
           if (visbleBox.SelectedItem.ToString() == "On")
            {
                show = true;
            }else
            {
                show = false;
            }
        }

        private void yourPinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status status = new Status();
            status.Accounts = new SortableBindingList<Account>(SelectAccount());
            status.Show();
            status.show = show;
            status.PinAction = PinAction.Repin;
            status.RepinPinList = File.ReadAllLines(Status.PINNED);

            status.PinStart();
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status status = new Status();
            status.Accounts = new SortableBindingList<Account>(SelectAccount());
            status.Show();
            status.show = show;
            status.PinAction = PinAction.RepinOther;
            status.PinStart();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            labelSelectedCount.Text = dataGridView1.SelectedRows.Count.ToString();
           
        }

        private void proxyAssignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            proxieList.AddRange(File.ReadAllLines(@"C:\my_work_files\pinterest\proxy.txt").ToList());

        }

        private void pinDomainTxt_DoubleClick(object sender, EventArgs e)
        {
            Form1.pinSite = pinDomainTxt.Text;
            MessageBox.Show(Form1.pinSite);
        }

        private void extraPinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Status status = new Status();
            status.Accounts = new SortableBindingList<Account>(SelectAccount());
            status.Show();
            status.show = show;
            status.PinAction = PinAction.PinCsv;
            status.PinStart();
        }
    }


}

