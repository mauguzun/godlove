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

        public bool Proxy { get; set; } = false;
        public bool Selected { get; set; } = false;

        public static List<Account> GetAccounts()
        {
            List<Account> acounts = new List<Account>();
            foreach (string line in File.ReadAllLines(@"C:\my_work_files\pinterest\full_info.txt"))
            {
                string[] splited = line.Split(':');
                acounts.Add(new Account()
                {
                    Email = splited[0],
                    Password = splited[1],
                    UserName = splited[2],
                    Followers = String.IsNullOrEmpty(splited[4]) ? 0 : int.Parse(splited[4]),
                    Follow = String.IsNullOrEmpty(splited[5]) ? 0 : int.Parse(splited[5]),
                    Boards = splited[6]
                }); ;
            }

       
            return acounts;
        }
        
    }

   
 
}
