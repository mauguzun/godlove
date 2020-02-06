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

        public string Name { get; set; }
        public string LastName { get; set; }

        public int ?Followers { get; set; } = null;
        public int ?Follow { get; set; } = null;
        public string Boards { get; set; } = null;

        public string Proxie { get; set; } = null;
        public bool Selected { get; set; } = false;

        public string Group { get; set; } = null;

        public override string ToString()
        {
            return this.Email + ':' + this.Password + ':' + this.UserName + "::" + this.Followers + ":" + this.Follow + ":" + this.Boards + ":" + this.Group;
        }

        public static List<Account> GetAccountExtraInfo()
        {
            List<Account> acounts = new List<Account>();
            foreach (string line in File.ReadAllLines(@"C:\my_work_files\pinterest\full_info_copy.txt"))
            {
                string[] splited = line.Split(':');
               var acc =  new Account()
                {
                    Email = splited[0],
                    Password = splited[1],
                    UserName = splited[2],
                    Followers = String.IsNullOrEmpty(splited[4]) ? 0 : int.Parse(splited[4]),
                    Follow = String.IsNullOrEmpty(splited[5]) ? 0 : int.Parse(splited[5]),
                    Boards = splited[6]
                }; 
                if(splited.Count() > 6)
                {
                    acc.Group = splited[7];
                }
                acounts.Add(acc);
            }

       
            return acounts;
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
