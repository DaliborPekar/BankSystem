// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BankSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, User> userDict = new Dictionary<int, User>();
            Random rand = new Random();

            List<string> userData = new List<string>();

            if (File.Exists("test.csp"))
            {
                Console.WriteLine("Loading data initialized!");
                string[] x = File.ReadAllLines("test.csp");

                foreach (string y in x)
                {
                    string l = string.Empty;
                    string p = string.Empty;
                    string o = string.Empty;
                    int checker = 0;
                    string g = y;
                    while (checker != 4)
                    {
                        int i = g.IndexOf(",");
                        int startIndex = 0;
                        int endIndex = i;

                        string tempName = string.Empty;

                        for (int k = startIndex; k < endIndex; k++)
                        {
                            tempName += g[k];
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

                    userDict[user.UserID] = user;
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
                    case 1:
                        AccountCreation();
                        break;

                    case 2:
                        Deposit();
                        break;

                    case 3:
                        Withdraw();
                        break;
                    case 4:
                        Balance();
                        break;
                    case 5:
                        AccountList();
                        break;
                    case 6:
                        Transfer();
                        break;
                    case 7:
                        mainMenu = false;
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
                        string input;
                        Console.Write("Enter the accountBalance: \n");
                        input = Console.ReadLine();

                        if (double.TryParse(input, out accountBalance))
                        {
                            gotInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a number");
                        }
                    }

                    int userID = rand.Next(10, 100);

                    User user = new User(name, accountBalance, userID);
                    Console.WriteLine($"{user.Name} {user.AccountBalance} {user.UserID}");
                    userData.Add($"{user.Name},{user.AccountBalance},{user.UserID},");
                    userDict[user.UserID] = user;
                    Console.WriteLine("Do you want to continue adding new users?");
                    string temp = Console.ReadLine().ToLower();

                    addUsers = (temp == "y") ? true : false;
                }

                userData.Clear();
            }

            void Deposit()
            {
                bool addBalance = true;
                double addAmount = 0;

                while (addBalance)
                {
                    bool gotInput = false;
                    User? user = null;

                    while (!gotInput)
                    {
                        Console.WriteLine("Enter the first userID: ");
                        string input = Console.ReadLine();

                        int userID;

                        if (int.TryParse(input, out userID))
                        {
                            user = CheckUser(userID);
                            if (user != null)
                            {
                                gotInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Enter correct ID\n");
                                gotInput = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Enter the number!!\n");
                        }
                    }

                    gotInput = false;

                    while (!gotInput)
                    {
                        string input;
                        Console.WriteLine("Enter amount to add to the balance:");
                        input = Console.ReadLine();

                        if (double.TryParse(input, out addAmount))
                        {
                            user.AddBalance(addAmount);
                            Console.WriteLine($"New account balance is {user.AccountBalance}");
                            gotInput = true;

                            Console.WriteLine("Do you want to continue adding to the balance?");
                            string temp = Console.ReadLine().ToLower();
                            addBalance = (temp == "y") ? true : false;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a number");
                        }
                    }
                }
            }

            void Withdraw()
            {
                bool withdraw = true;

                while (withdraw)
                {
                    bool gotInput = false;
                    User? user = null;

                    while (!gotInput)
                    {
                        Console.WriteLine("Enter the userID: ");
                        string input = Console.ReadLine();

                        int userID;

                        if (int.TryParse(input, out userID))
                        {
                            user = CheckUser(userID);
                            if (user != null)
                            {
                                gotInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Enter correct ID\n");
                                gotInput = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Enter the number!!\n");
                        }
                    }

                    gotInput = false;

                    while (!gotInput)
                    {
                        Console.WriteLine("Enter amount to withdraw:");
                        string input = Console.ReadLine();

                        if (double.TryParse(input, out double withdrawAmount))
                        {
                            if ((user.AccountBalance - withdrawAmount) < 0)
                            {
                                Console.WriteLine("Not enough money!");
                            }
                            else
                            {
                                user.Withdraw(withdrawAmount);
                                Console.WriteLine($"New account balance is {user.AccountBalance}");
                            }

                            Console.WriteLine("Do you want to continue withdrawing?");
                            string temp = Console.ReadLine().ToLower();
                            withdraw = (temp == "y") ? true : false;
                        }
                        else
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
                    bool gotInput = false;
                    User? user = null;

                    while (!gotInput)
                    {
                        Console.WriteLine("Enter the userID: ");
                        string input = Console.ReadLine();

                        int userID;

                        if (int.TryParse(input, out userID))
                        {
                            user = CheckUser(userID);
                            if (user != null)
                            {
                                gotInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Enter correct ID\n");
                                gotInput = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Enter the number!!\n");
                        }
                    }

                    Console.WriteLine($"Account balance: {user.AccountBalance}");

                    Console.WriteLine("Do you want to check another user?");

                    string temp = Console.ReadLine().ToLower();
                    balance = (temp == "y") ? true : false;
                }
            }

            void AccountList()
            {
                foreach (var item in userDict)
                {
                    User user = item.Value;
                    Console.WriteLine($"{user.Name} {user.AccountBalance} {user.UserID}");
                }
            }

            void Transfer()
            {
                bool transfer = true;
                while (transfer)
                {
                    bool gotInput = false;
                    User? user1 = null;
                    User? user2 = null;
                    while (!gotInput)
                    {
                        Console.WriteLine("Enter the first userID: ");
                        string input1 = Console.ReadLine();

                        int userID;

                        if (int.TryParse(input1, out userID))
                        {
                            user1 = CheckUser(userID);
                            if (user1 != null)
                            {
                                gotInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Enter correct ID\n");
                                gotInput = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Enter the number!!\n");
                        }
                    }

                    gotInput = false;

                    while (!gotInput)
                    {
                        Console.WriteLine("Enter the second userID: ");
                        string input2 = Console.ReadLine();

                        int userID;

                        if (int.TryParse(input2, out userID))
                        {
                            user2 = CheckUser(userID);
                            if (user2 != null)
                            {
                                gotInput = true;
                            }
                            else
                            {
                                Console.WriteLine("Enter correct ID\n");
                                gotInput = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Enter the number!!\n");
                        }
                    }

                    double transferAmount = 0;

                    gotInput = false;
                    while (!gotInput)
                    {
                        Console.WriteLine("Enter transfer amount: ");
                        string input = Console.ReadLine();

                        if (double.TryParse(input, out transferAmount))
                        {
                            gotInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Please enter a number!");
                        }
                    }

                    if ((user1.AccountBalance - transferAmount) >= 0)
                    {
                        user1.Withdraw(transferAmount);

                        user2.AddBalance(transferAmount);

                        Console.WriteLine(user1.AccountBalance);

                        Console.WriteLine(user2.AccountBalance);

                        Console.WriteLine("Do you want to make a new transfer?");
                        string temp = Console.ReadLine();
                        transfer = (temp == "y") ? true : false;
                    }
                    else
                    {
                        Console.WriteLine("Not enough money!");
                        Console.WriteLine("Do you want to try again transfer?");
                        string temp = Console.ReadLine();
                        transfer = (temp == "y") ? true : false;
                    }
                }
            }

            User? CheckUser(int userID)
            {
                if (userDict.ContainsKey(userID))
                {
                    return userDict[userID];
                }

                return null;
            }

            // Saving userData
            Console.WriteLine("Saving data");

            Console.ReadKey();

            foreach (var item in userDict)
            {
                User user = item.Value;
                userData.Add($"{user.Name},{user.AccountBalance},{user.UserID},");
            }

            File.WriteAllLines("test.csp", userData);
        }
    }
}
