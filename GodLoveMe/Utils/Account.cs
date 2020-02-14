using AddMeFast;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodLoveMe
{
    public class Account

    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string FullName { get; set; }
       

        public int? Followers { get; set; } = null;
        public int? Follow { get; set; } = null;
        public string Boards { get; set; } = null;

        private string proxie;
        public string Proxie
        {
            get { return proxie; }
            set
            {
                if (value != null)
                    proxie = value.Replace(':', '_');
            }
        }

        public string Cookie { get; set; } = null;

        public string Group { get; set; } = null;

        public string Status { get; set; } = null;

        public override string ToString()
        {
            return this.Email + ':' + this.Password + ':' + this.UserName + ":" + this.Proxie + ":" + this.Followers + ":" + this.Follow + ":" + this.Boards + ":" + this.Group + ":" + this.Status;
        }

        public static List<Account> GetAccountExtraInfo()
        {
            List<Account> acounts = new List<Account>();
            List<Account> cookiesAccount = AccountsPaths();

            foreach (string line in File.ReadAllLines(@"C:\my_work_files\pinterest\full_info_copy.txt"))
            {
                string[] splited = line.Split(':');
                var acc = new Account()
                {
                    Email = splited[0],
                    Password = splited[1],
                    UserName = splited[2],
                    Proxie = splited[3],
                    Followers = String.IsNullOrEmpty(splited[4]) ? 0 : int.Parse(splited[4]),
                    Follow = String.IsNullOrEmpty(splited[5]) ? 0 : int.Parse(splited[5]),
                    Boards = splited[6]
                };
                if (splited.Count() > 6)
                {
                    acc.Group = splited[7];
                }
                if (splited.Count() > 7)
                {
                    acc.Status = splited[8];
                }
                if (cookiesAccount.Any(x => x.Email == acc.Email))
                {
                    acc.Cookie = cookiesAccount.Where(x => x.Email == acc.Email).FirstOrDefault().Cookie;

                }
                acounts.Add(acc);
            }


            return acounts;
        }
        public static List<Account> AccountsPaths()
        {
            List<Account> acc = new List<Account>();
            var cookiesFiles = Directory.GetFiles(CookieManager.ACCOUNTS);
            foreach (var cookiesFile in cookiesFiles)
            {
                string[] para = Path.GetFileNameWithoutExtension(cookiesFile).Split(new string[] { "_p_" }, StringSplitOptions.RemoveEmptyEntries);
                acc.Add(new Account()
                {
                    Email = para[0],
                    Proxie = para[1],
                    Cookie = cookiesFile,
                });
            }


            return acc;
        }
        public static List<Account> GetAccounts()
        {
            List<Account> acounts = new List<Account>();
            foreach (string line in File.ReadAllLines(@"C:\my_work_files\pinterest\source_all_account_for_blaster.txt"))
            {
                string[] splited = line.Split(':');
                acounts.Add(new Account()
                {
                    Email = splited[0],
                    Password = splited[1],
                    UserName = splited[2],

                }); ;
            }


            return acounts;
        }

    }



}
