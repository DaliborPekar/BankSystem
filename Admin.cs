using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    internal class Admin
    {
        public Admin()
        {
            this.Name = "Admin";
            this.AccountBalance = 9999;
            this.UserID = 8;
            this.Pin = 2104;
        }

        public string Name { get; set; }

        public double AccountBalance { get; set; }

        public int UserID { get; set; }

        public int Pin { get; set; }
    }
}
