using AddMeFast;
using GodLoveMe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
    class AccountManager
    {
        private const string PATH = @"C:\my_work_files\pinterest\full_info_copy.txt";
        private static AccountManager instance;
        public static SortableBindingList<Account> Accounts { get; set; }

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
                    Proxie = splited[3],
                    Followers = String.IsNullOrEmpty(splited[4]) ? 0 : int.Parse(splited[4]),
                    Follow = String.IsNullOrEmpty(splited[5]) ? 0 : int.Parse(splited[5]),
                    Boards = splited[6]
                };
                if (splited.Count() > 6)
                {
                    acc.Group = splited[7];
                }
                if (splited.Count() > 7 )
                {
                    var x = splited.Count();
                    acc.Status = splited[8];
                }
                if (cookiesAccount.Any(x => x.Email == acc.Email))
                {
                    acc.Cookie = cookiesAccount.Where(x => x.Email == acc.Email).FirstOrDefault().Cookie;

                }
                accounts.Add(acc);
            }


            Accounts = new SortableBindingList<Account>(accounts);

        }
        public  void Save()
        {
            File.Delete(PATH);
            foreach (Account account in Accounts)
            {
                File.AppendAllText(PATH, account.ToString() + Environment.NewLine);

            }
        }
        public static List<Account> AccountsPaths()
        {
            List<Account> acc = new List<Account>();
            var cookiesFiles = Directory.GetFiles(CookieManager.ACCOUNTS);
            foreach (var cookiesFile in cookiesFiles)
            {
                string[] para = Path.GetFileNameWithoutExtension(cookiesFile).Split(new string[] { "_p_" }, StringSplitOptions.RemoveEmptyEntries);
                if(para.Count() > 1)
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
