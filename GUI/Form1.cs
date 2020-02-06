using AddMeFast;
using GodLoveMe;
using GodLoveMe.Pinterest;
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
        SortableBindingList<Account> accounts;
        private int repeatAccountInOneProxy;

        public Form1()
        {
            InitializeComponent();
            accounts = new SortableBindingList<Account>(Account.GetAccountExtraInfo());
            this.dataGridView1.DataSource = accounts;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowHeadersVisible = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.Delete(PATH);
            foreach (Account account in this.accounts)
            {
                File.AppendAllText(PATH, account.ToString() + Environment.NewLine);

            }
            MessageBox.Show("saved");
        }


        private void setGroupBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && !string.IsNullOrEmpty(groupNameTxt.Text))
            {
                string name = groupNameTxt.Text.Trim();
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    this.accounts.First(x => x.Email == row.Cells[0].Value).Group = name;
                    var select = this.accounts.First(x => x.Email == row.Cells[0].Value);

                }
                this.dataGridView1.DataSource = accounts;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DriverInstance drivers = new DriverInstance();
            //if (dataGridView1.SelectedRows.Count > 0 && !string.IsNullOrEmpty(groupNameTxt.Text))
            //{
            Thread t = new Thread(() =>
            {
                Parallel.ForEach(this.accounts.Where(x => x.Group == "denis"), new ParallelOptions() {
                    MaxDegreeOfParallelism = 4 }, (acc) =>
                   {

                       var proxy = new GetProxy.ProxyReader();
                       string current = proxy.GetList()[0];
                       try
                       {


                           drivers.InitDriver(false, proxy.GetList()[0]);
                           Pinterest pin = new Pinterest(drivers.Driver);

                           pin.MakeLogin(acc.Email, acc.Password);
                           if (pin.CheckLogin())
                           {
                               pin.SaveCookie(CookieManager.Filename(acc.Email, current));
                               if (pin.ValidName() == false)
                               {
                                   pin.FillName();
                               }
                               repeatAccountInOneProxy++;
                               if (repeatAccountInOneProxy > 7)
                               {
                                   var list = proxy.GetList();
                                   list.Remove(current);
                                   File.WriteAllLines(@"C:\my_work_files\pinterest\proxy.txt", list);
                               }
                           }
                           else
                           {
                               //var list = proxy.GetList();
                               //list.Remove(current);
                               //File.WriteAllLines(@"C:\my_work_files\pinterest\proxy.txt", list);
                           }

                       }

                       catch (Exception ex)
                       {
                           var a = ex;
                           var list = proxy.GetList();
                           list.Remove(current);
                           File.WriteAllLines(@"C:\my_work_files\pinterest\proxy.txt", list);
                           Console.WriteLine(acc.Email + ex.Message);
                       }
                       finally
                       {
                           drivers.SuperQuit();

                       }
                   });
            });
            t.Start();
            //}

          
         
        }
    }
}
