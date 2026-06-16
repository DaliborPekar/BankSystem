using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace BankSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Random rand = new Random();

            List<User> users = new List<User>();

            if (File.Exists("test.csp"))
            {

                Console.WriteLine("Loading data initialized!");
                string[] x = File.ReadAllLines("test.csp");



                foreach (string y in x)
                {
                    string l = "";
                    string p = "";
                    string o = "";
                    int checker = 0;
                    string g = y;
                    while (checker != 4)
                    {
                        int i = g.IndexOf(",");
                        int startIndex = 0;
                        int endIndex = i;




                        string tempName = "";

                        for (int k = startIndex; k < endIndex; k++)
                        {
                            tempName += (g[k]);

                        }
                        checker++;

                        g = g.Remove(startIndex, endIndex + 1);
                        startIndex = endIndex + 1;
                        if (checker == 1)
                        {
                            l = tempName;

                        }
                        else if (checker == 2)
                        {

                            p = tempName;

                        }
                        else if (checker == 3)
                        {

                            o = tempName;
                        }
                    }
                    User user = new User(l, Convert.ToDouble(p), Convert.ToInt32(o));
                    users.Add(user);

                }

            }


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
                    case (6):
                        transfer();
                        break;
                    default:
                        mainMenu = false;
                        break;

                }
            }

            void AccountCreation()
            {
                bool addUsers = true;
                List<string> userData = new List<string>();
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
                    Console.WriteLine($"{user.Name} {user.AccountBalance} {user.UserID}");
                    users.Add(user);
                    userData.Add($"{user.Name},{user.AccountBalance},{user.UserID},");
                    Console.WriteLine("Do you want to continue adding new users?");
                    string temp = Console.ReadLine().ToLower();

                    addUsers = (temp == "y") ? true : false;
                }

                File.WriteAllLines("test.csp", userData);



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
                                Console.WriteLine($"New account balance is {user.AccountBalance}");
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

                            if ((user.AccountBalance - withdrawAmount) < 0)
                                Console.WriteLine("Not enough money!");
                            else
                            {
                                user.Withdraw(withdrawAmount);
                                Console.WriteLine($"New account balance is {user.AccountBalance}");
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
                        Console.WriteLine($"Account balance: {user.AccountBalance}");

                        Console.WriteLine("Do you want to check another user?");

                        string temp = Console.ReadLine().ToLower();
                        balance = (temp == "y") ? true : false;
                    }

                }
            }


            void accountList()
            {
                foreach (User user in users)
                {
                    Console.WriteLine($"{user.Name} {user.AccountBalance} {user.UserID}");
                }
            }


            void transfer()
            {
                bool transfer = true;
                while (transfer)
                {
                    Console.WriteLine("Enter the first full name or userID: ");
                    string fullname1 = Console.ReadLine();

                    User user1 = checkUser(fullname1);

                    Console.WriteLine("Enter the second full name or userID: ");
                    string fullname2 = Console.ReadLine();

                    User user2 = checkUser(fullname2);


                    if (user1 == null || user2 == null)
                    {
                        Console.WriteLine("That user doesnt exist! Try again!");

                    }
                    else
                    {


                        double transferAmount = 0;

                        bool gotInput = false;
                        while (!gotInput)
                        {
                            try
                            {
                                Console.WriteLine("Enter transfer amount: ");
                                transferAmount = Convert.ToDouble(Console.ReadLine());
                                gotInput = true;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Please enter a number!");
                            }
                        }


                        if ((user1.AccountBalance - transferAmount) < 0)
                        {
                            Console.WriteLine("Not enough money!");
                            Console.WriteLine("Do you want to try again transfer?");
                            string temp = Console.ReadLine();
                            transfer = (temp == "y") ? true : false;
                        }


                        else
                        {
                            user1.Withdraw(transferAmount);

                            user2.AddBalance(transferAmount);

                            Console.WriteLine(user1.AccountBalance);

                            Console.WriteLine(user2.AccountBalance);

                            Console.WriteLine("Do you want to make a new transfer?");
                            string temp = Console.ReadLine();
                            transfer = (temp == "y") ? true : false;
                        }
                    }


                }

            }


            User checkUser(string fullname)
            {
                foreach (User user in users)
                {
                    if (user.Name == fullname)
                    {
                        return user;
                    }
                    try
                    {
                        if (user.UserID == Convert.ToInt32(fullname))
                        {
                            return user;
                        }
                    }
                    catch (FormatException)
                    {

                    }
                }
                return null;
            }
        }
    }
}
