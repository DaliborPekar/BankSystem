namespace BankSystem
{
    internal class User
    {
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
            this.AccountBalance += balance;
        }

        public void Withdraw(double amount)
        {
            this.AccountBalance -= amount;
        }
    }
}
