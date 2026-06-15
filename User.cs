using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem
{
    internal class User
    {
        public string name;
        public double accountBalance;

        public User(string name, double accountBalance)
        {
            this.name = name;
            this.accountBalance = accountBalance;
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
