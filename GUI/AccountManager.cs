using AddMeFast;
using GodLoveMe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GUI
{
    class AccountManager
    {
        private const string PATH = @"C:\my_work_files\pinterest\full_info_copy.txt";
        private static AccountManager instance;
        public static List<Account> Accounts { get; set; }

        private AccountManager()
        { }

        public static AccountManager GetInstance()
        {
            if (instance == null)
            {
                instance = new AccountManager();
                SetAccountExtraInfo();
            }

            return instance;
        }

        public static void SetAccountExtraInfo()
        {
            var accounts = new List<Account>();
            List<Account> cookiesAccount = AccountsPaths();

            foreach (string line in File.ReadAllLines(@"C:\my_work_files\pinterest\full_info_copy.txt"))
            {
                string[] splited = line.Split(':');
                var acc = new Account()
                {
                    Email = splited[0],
                    Password = splited[1],
                    UserName = splited[2],

                };
                if (splited.Count() > 3)
                {
                    acc.FullName = splited[3];


                    acc.Proxie = splited[4];
                    acc.Followers = String.IsNullOrEmpty(splited[5]) ? 0 : int.Parse(splited[5]);
                    acc.Follow = String.IsNullOrEmpty(splited[6]) ? 0 : int.Parse(splited[6]);
                    acc.Boards = splited[7];
                  }
                if (splited.Count() > 7)
                {
                    acc.Group = splited[8];
                }
                if (splited.Count() > 8)
                {
                    var x = splited.Count();
                    acc.Status = splited[9];
                }
                if (cookiesAccount.Any(x => x.Email == acc.Email))
                {
                    acc.Cookie = cookiesAccount.Where(x => x.Email == acc.Email).FirstOrDefault().Cookie;

                }
                accounts.Add(acc);
            }

           
            Accounts = new List<Account>(accounts);

        }
        private static ReaderWriterLockSlim lock_ = new ReaderWriterLockSlim();
        public void Save()
        {
            lock_.EnterWriteLock();
            try
            {
                File.Delete(PATH);

                var acc = new List<Account>();

                foreach (var item in Accounts.ToList())
                {
                    if(acc.Where(x=>x.UserName == item.UserName).Any())
                    {
                        if (acc.Where(x => x.UserName == item.UserName).FirstOrDefault().Password == "trance_333")
                        {
                            acc.Where(x => x.UserName == item.UserName).FirstOrDefault().Password = item.Password;
                        }
                    }
                    else
                    {
                        acc.Add(item);
                    }
                   
                }


                var csv = new StringBuilder();
                foreach (Account account in acc)
                {
                    csv.AppendLine(account.ToString());
                }
                File.WriteAllText(PATH, csv.ToString());

            }
            finally
            {
                lock_.ExitWriteLock();
            }


           
        }
        public static List<Account> AccountsPaths()
        {
            List<Account> acc = new List<Account>();
            var cookiesFiles = Directory.GetFiles(CookieManager.ACCOUNTS);
            foreach (var cookiesFile in cookiesFiles)
            {
                string[] para = Path.GetFileNameWithoutExtension(cookiesFile).Split(new string[] { "_p_" }, StringSplitOptions.RemoveEmptyEntries);
                if (para.Count() > 1)
                {
                    acc.Add(new Account()
                    {
                        Email = para[0],
                        Proxie = para[1],
                        Cookie = cookiesFile,
                    });
                }

            }


            return acc;
        }
    }
}
