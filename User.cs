using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    internal class User
    {
        public string name;
        public double accountBalance;
        public int userID;

        public User(string name, double accountBalance, int userID)
        {
            this.name = name;
            this.accountBalance = accountBalance;
            this.userID = userID;
        }

        public void AddBalance(double balance)
        {
            accountBalance += balance;
        }

        public void Withdraw(double amount)
        {
            accountBalance -= amount;
        }

    }
}
