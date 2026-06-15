using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace BankSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Random rand = new Random();

            List<User> users = new List<User>();

            bool mainMenu = true;

            int menuSelect = 0;

            while (mainMenu)
            {
                Console.WriteLine("Select mode: ");
                menuSelect = Convert.ToInt32(Console.ReadLine());
                switch (menuSelect)
                {
                    case (1):
                        AccountCreation();
                        break;

                    case (2):
                        Deposit();
                        break;

                    case (3):
                        Withdraw();
                        break;
                    case (4):
                        Balance();
                        break;
                    case (5):
                        accountList();
                        break;
                    default:
                        mainMenu = false;
                        break;

                }
            }

            void AccountCreation()
            {
                bool addUsers = true;

                while (addUsers)
                {
                    Console.Write("Enter the name: \n");
                    string name = Console.ReadLine();

                    bool gotInput = false;

                    double accountBalance = 0;

                    while (!gotInput)
                    {
                        try
                        {
                            Console.Write("Enter the accountBalance: \n");
                            accountBalance = Convert.ToDouble(Console.ReadLine());
                            gotInput = true;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter a number");
                        }
                    }

                    int userID = rand.Next(10, 100);

                    User user = new User(name, accountBalance, userID);
                    Console.WriteLine($"{user.name} {user.accountBalance} {user.userID}");
                    users.Add(user);
                    Console.WriteLine("Do you want to continue adding new users?");
                    string temp = Console.ReadLine().ToLower();

                    addUsers = (temp == "y") ? true : false;
                }
            }

            void Deposit()
            {
                bool addBalance = true;
                double addAmount = 0;

                while (addBalance)
                {

                    Console.WriteLine("Enter the full name or userID: ");
                    string fullname = Console.ReadLine();


                    User user = checkUser(fullname);


                    if (user == null)
                    {
                        Console.WriteLine("That user doesnt exist! Try again!");
                        addBalance = true;
                    }

                    else
                    {
                        bool gotInput = false;

                        while (!gotInput)
                        {

                            try
                            {

                                Console.WriteLine("Enter amount to add to the balance:");
                                addAmount = Convert.ToDouble(Console.ReadLine());
                                user.AddBalance(addAmount);
                                Console.WriteLine($"New account balance is {user.accountBalance}");
                                gotInput = true;

                                Console.WriteLine("Do you want to continue adding to the balance?");
                                string temp = Console.ReadLine().ToLower();
                                addBalance = (temp == "y") ? true : false;


                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Please enter a number");
                            }

                        }

                    }
                }
            }
            void Withdraw()
            {
                bool withdraw = true;

                while (withdraw)
                {
                    Console.WriteLine("Enter the full name or userID: ");
                    string fullname = Console.ReadLine();

                    User user = checkUser(fullname);

                    if (user == null)
                    {
                        Console.WriteLine("That user doesnt exist! Try again!");
                        withdraw = true;
                    }
                    else
                    {
                        try
                        {
                            Console.WriteLine("Enter amount to withdraw:");
                            double withdrawAmount = Convert.ToDouble(Console.ReadLine());

                            if ((user.accountBalance - withdrawAmount) < 0)
                                Console.WriteLine("Not enough money!");
                            else
                            {
                                user.Withdraw(withdrawAmount);
                                Console.WriteLine($"New account balance is {user.accountBalance}");
                            }
                            Console.WriteLine("Do you want to continue withdrawing?");
                            string temp = Console.ReadLine().ToLower();
                            withdraw = (temp == "y") ? true : false;
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Please enter a number!");
                        }
                    }
                }
            }
            void Balance()
            {
                bool balance = true;

                while (balance)
                {

                    Console.WriteLine("Enter the full name or userID: ");
                    string fullname = Console.ReadLine();

                    User user = checkUser(fullname);

                    if (user == null)
                    {
                        Console.WriteLine("That user doesnt exist! Try again!");
                        balance = true;
                    }
                    else
                    {
                        Console.WriteLine($"Account balance: {user.accountBalance}");

                        Console.WriteLine("Do you want to check another user?");

                        string temp = Console.ReadLine().ToLower();
                        balance = (temp == "y") ? true : false;
                    }

                }
            }


            void accountList()
            {
                foreach(User user in users)
                {
                    Console.WriteLine($"{user.name} {user.accountBalance} {user.userID}");
                }
            }


            User checkUser(string fullname)
            {
                foreach (User user in users)
                {
                    if (user.name == fullname)
                    {
                        return user;
                    }
                    try
                    {
                        if (user.userID == Convert.ToInt32(fullname))
                        {
                            return user;
                        }
                    }
                    catch(FormatException)
                    {
                       
                    }
                    
                }
                return null;
            }
        }
    }
}
