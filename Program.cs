namespace BankSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            List<User> users = new List<User>();
           
            bool addUsers = true;

            while(addUsers)
            {
                Console.Write("Enter the name: \n");
                string name = Console.ReadLine();

                Console.Write("Enter the accountBalance: \n");
                double accountBalance = Convert.ToDouble(Console.ReadLine());


                User user = new User(name, accountBalance);
                
                users.Add(user);
                Console.WriteLine("Do you want to continue adding new users?");
                string temp = Console.ReadLine().ToLower();

                addUsers = (temp == "y") ? true : false;
            }
            bool addBalance = true;

            while(addBalance)
            {
                Console.WriteLine("Enter the full name: ");
                string fullname = Console.ReadLine();
                foreach (User user in users)
                {
                    if(user.name == fullname)
                    {
                        Console.WriteLine("Enter amount to add to the balance:");
                        double addAmount = Convert.ToDouble(Console.ReadLine());
                        user.AddBalance(addAmount);
                        Console.WriteLine($"New account balance is {user.accountBalance}");
                        break;
                    }
                    
                }
                Console.WriteLine("Do you want to continue adding to the balance?");
                string temp = Console.ReadLine().ToLower();
                addBalance = (temp == "y") ? true : false;
            }

            bool withdraw = true;

            while (withdraw)
            {
                Console.WriteLine("Enter the full name: ");
                string fullname = Console.ReadLine();
                foreach (User user in users)
                {
                    if (user.name == fullname)
                    {
                        Console.WriteLine("Enter amount to withdraw:");
                        double withdrawAmount = Convert.ToDouble(Console.ReadLine());
                        user.Withdraw(withdrawAmount);
                        Console.WriteLine($"New account balance is {user.accountBalance}");
                        break;
                    }

                }
                Console.WriteLine("Do you want to continue withdrawing?");
                string temp = Console.ReadLine().ToLower();
                withdraw = (temp == "y") ? true : false;
            }



           

        }
    }
}
