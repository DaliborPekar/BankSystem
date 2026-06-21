namespace BankSystem
{
    internal class User
    {
        public User(string name, double accountBalance, int userID, int pin)
        {
            this.Name = name;
            this.AccountBalance = accountBalance;
            this.UserID = userID;
            this.Pin = pin;
        }

        public string Name { get; set; }

        public double AccountBalance { get; set; }

        public int UserID { get; set; }

        public int Pin { get; set; }

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
