using System.ComponentModel.Design;

namespace BankSystem
{
    internal class BankManager
    {

        Dictionary<int, User> userDict = new Dictionary<int, User>();
        Random rand = new Random();

        public void Run()
        {
            Console.WriteLine("Welcome to BankSystem\n");
            FileLoading();

            Console.WriteLine("Do you want to login or register: (l for login or r for register)");
            string input = Console.ReadLine().ToLower();
            Console.Clear();

            if (input == "l")
            {
                if (noUsers)
                {
                    Console.WriteLine("There is no users available, please register.");
                    Registration();
                }
                else if (!noUsers)
                {
                    Login();
                }
            }
            else if (input == "r")
            {
                Registration();
            }

            if (input == "a")
            {
                Admin admin = new Admin();
                user = new User(admin.Name, admin.AccountBalance, admin.UserID, admin.Pin);
                AdminMainMenu();
            }
            else
            {
                MainMenu();
            }

            SaveFile();
        }

        bool noUsers = false;

        private void FileLoading()
        {
            if (File.Exists("testing.csp"))
            {
                string[] text = File.ReadAllLines("testing.csp");

                foreach (string row in text)
                {
                    string[] tokens = row.Split(",");

                    user = new User(tokens[0], Convert.ToDouble(tokens[1]), Convert.ToInt32(tokens[2]), Convert.ToInt32(tokens[3]));

                    userDict[user.UserID] = user;
                }
            }
            else
            {
                noUsers = true;
            }
        }

        User? user;

        private void Login()
        {
            bool logedIn = false;
            Console.WriteLine("Login:\n");
            while (!logedIn)
            {
                user = UserInputHandler();

                if (UserAuthorisation(user))
                {
                    Console.WriteLine("\nLogin succesful\n");
                    logedIn = true;
                    Console.Clear();
                }
            }
        }

        private void Registration()
        {
            bool registered = false;
            Console.WriteLine("Registration:\n");
            while (!registered)
            {
                Console.Write("Enter the name: \n");
                string name = Console.ReadLine();
                double accountBalance = GetAmount();

                bool takenUserID = false;

                Console.WriteLine("Enter the PIN: ");

                string input = Console.ReadLine();
                bool gotInput = false;

                while(!gotInput)
                {
                    if (int.TryParse(input, out int pin))
                    {
                        while (!takenUserID)
                        {
                            int userID = rand.Next(10, 100);

                            if (CheckUser(userID) == null)
                            {
                                user = new User(name, accountBalance, userID, pin);

                                Console.WriteLine($"{user.Name} {user.AccountBalance} {user.UserID} {user.Pin}");

                                userDict[user.UserID] = user;
                                takenUserID = true;
                                registered = true;
                                gotInput = true;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number");
                    }
                }
            }
        }

        private void MainMenu()
        {
            bool mainMenu = true;

            int menuSelect = 0;

            while (mainMenu)
            {
                Console.WriteLine("Select mode: ");
                Console.WriteLine("1 - Deposit\n2 - Withdraw\n3 - Balance\n4 - Transfer\n5 - Exit");

                bool gotInput = false;

                while (!gotInput)
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out menuSelect))
                    {
                        Console.Clear();
                        switch (menuSelect)
                        {
                            case 1:
                                Deposit();
                                break;

                            case 2:
                                Withdraw();
                                break;
                            case 3:
                                Balance();
                                break;
                            case 4:
                                Transfer();
                                break;
                            case 5:
                                mainMenu = false;
                                break;
                            default:
                                mainMenu = false;
                                break;
                        }

                        gotInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number\n");
                    }
                }
            }
        }

        private void AdminMainMenu()
        {
            bool mainMenu = true;

            int menuSelect = 0;

            while (mainMenu)
            {
                Console.WriteLine("Admin Menu:\n");
                Console.WriteLine("Select mode: ");
                Console.WriteLine("1 - AccountCreation\n2 - Deposit\n3 - Withdraw\n4 - Balance\n5 - AccountList\n6 - Transfer\n7 - Exit");
                bool gotInput = false;
                while(!gotInput)
                {
                    string input = Console.ReadLine();
                    if (int.TryParse(input, out menuSelect))
                    {
                        Console.Clear();
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

                        gotInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a number!\n");
                    }
                }
            }
        }

        private void AccountCreation()
        {
            bool addUsers = true;

            while (addUsers)
            {
                Console.WriteLine("AccountCreation:\n");
                Console.Write("Enter the name: \n");
                string name = Console.ReadLine();

                double accountBalance = GetAmount();

                bool takenUserID = false;

                Console.WriteLine("Enter the PIN: ");

                int pin = Convert.ToInt32(Console.ReadLine());

                while (!takenUserID)
                {
                    int userID = rand.Next(10, 100);

                    if (CheckUser(userID) == null)
                    {
                        User user = new User(name, accountBalance, userID, pin);
                        Console.WriteLine($"{user.Name} {user.AccountBalance} {user.UserID} {user.Pin}");

                        userDict[user.UserID] = user;
                        takenUserID = true;

                        Console.WriteLine("Do you want to continue adding new users?");
                        string temp = Console.ReadLine().ToLower();

                        addUsers = (temp == "y") ? true : false;
                    }

                    Console.Clear();
                }
            }
        }

        private void Deposit()
        {
            bool addBalance = true;

            while (addBalance)
            {
                Console.WriteLine("Deposit:\n");
                double addAmount = GetAmount();

                user.AddBalance(addAmount);
                Console.WriteLine($"New account balance is {user.AccountBalance}");

                Console.WriteLine("Do you want to continue adding to the balance?");
                string temp = Console.ReadLine().ToLower();
                addBalance = (temp == "y") ? true : false;
                Console.Clear();
            }
        }

        private void Withdraw()
        {
            bool withdraw = true;

            while (withdraw)
            {
                Console.WriteLine("Withdraw:\n");
                double withdrawAmount = GetAmount();

                if ((user.AccountBalance - withdrawAmount) < 0)
                {
                    Console.WriteLine("Not enough money in the account!");
                }
                else
                {
                    user.Withdraw(withdrawAmount);
                    Console.WriteLine($"New account balance is {user.AccountBalance}");
                }

                Console.WriteLine("Do you want to continue withdrawing?");

                string temp = Console.ReadLine().ToLower();
                withdraw = (temp == "y") ? true : false;
                Console.Clear();
            }
        }

        private void Balance()
        {
            bool balance = true;

            while (balance)
            {
                Console.WriteLine("Balance:\n");
                Console.WriteLine($"Account balance: {user.AccountBalance}");

                Console.WriteLine("Do you want to check another user?");

                string temp = Console.ReadLine().ToLower();
                balance = (temp == "y") ? true : false;
                Console.Clear();
            }
        }

        private void AccountList()
        {
            Console.WriteLine("AccountList:\n");
            foreach (var item in userDict)
            {
                User user = item.Value;
                Console.WriteLine($"{user.Name} {user.AccountBalance} {user.UserID} {user.Pin}");
            }

            Console.ReadKey();
            Console.Clear();
        }

        private void Transfer()
        {
            bool transfer = true;
            while (transfer)
            {
                Console.WriteLine("Transfer:\n");
                User? user2 = UserInputHandler();

                double transferAmount = GetAmount();

                if ((user.AccountBalance - transferAmount) >= 0)
                {
                    user.Withdraw(transferAmount);

                    user2.AddBalance(transferAmount);

                    Console.WriteLine(user.AccountBalance);

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

                Console.Clear();
            }
        }

        private User? CheckUser(int userID)
        {
            if (userDict.ContainsKey(userID))
            {
                return userDict[userID];
            }

            return null;
        }

        private void SaveFile()
        {
            List<string> userData = new List<string>();
            Console.WriteLine("Saving data");

            foreach (var item in userDict)
            {
                User user = item.Value;
                userData.Add($"{user.Name},{user.AccountBalance},{user.UserID},{user.Pin}");
            }

            File.WriteAllLines("testing.csp", userData);

            Console.ReadKey();
        }

        private User UserInputHandler()
        {
            User? user = null;

            do
            {
                Console.WriteLine("Enter the userID: ");
                string input = Console.ReadLine();

                int userID;

                if (int.TryParse(input, out userID))
                {
                    user = CheckUser(userID);

                    if (user == null)
                    {
                        Console.WriteLine("Enter correct ID\n");
                    }
                }
                else
                {
                    Console.WriteLine("Enter the number!!\n");
                }
            }
            while (user == null);

            return user;
        }

        private bool UserAuthorisation(User user)
        {
            int input;
            int atempts = 3;
            do
            {
                Console.WriteLine("Enter PIN: ");
                input = Convert.ToInt32(Console.ReadLine());
                if (user.Pin != input)
                {
                    Console.WriteLine("Wrong PIN!");
                    atempts--;
                    Console.WriteLine($"{atempts} attempts remaining!\n");
                }
            }

            while (user.Pin != input && atempts > 0);

            if (atempts == 0)
            {
                Console.WriteLine("Authorization failed. Going back to main menu!!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private double GetAmount()
        {
            string input;
            double amount;
            do
            {
                Console.WriteLine("Enter amount: ");
                input = Console.ReadLine();

                if (double.TryParse(input, out amount) == false)
                {
                    Console.WriteLine("Please enter a number!");
                }
            }
            while (double.TryParse(input, out amount) == false);

            return amount;
        }
    }
}
