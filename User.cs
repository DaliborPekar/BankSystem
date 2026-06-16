using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    internal class User
    {
        //private string name;
        //private double accountBalance;
        //private int userID;

        public User(string name, double accountBalance, int userID)
        {
            this.Name = name;
            this.AccountBalance = accountBalance;
            this.UserID = userID;

           

            


        }

        public string Name { get; set; }

        public double AccountBalance { get; set; }

        public int UserID { get; set; }


        




        public void AddBalance(double balance)
        {
            AccountBalance += balance;
        }
        
        public void Withdraw(double amount)
        {
            AccountBalance -= amount;
        }

    }
}
