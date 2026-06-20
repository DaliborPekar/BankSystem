namespace BankSystem
{
    internal class BankManager
    {

        Dictionary<int, User> userDict = new Dictionary<int, User>();
        Random rand = new Random();

        public void Run()
        {
            FileLoading();
            MainMenu();
            SaveFile();
        }

        private void FileLoading()
        {

            if (File.Exists("users.csp"))
            {
                Console.WriteLine("Loading data initialized!");
                string[] text = File.ReadAllLines("users.csp");

                foreach (string row in text)
                {
                    string[] tokens = row.Split(",");
                    Console.WriteLine($"{tokens[0]}, {tokens[1]}, {tokens[2]}");

                    User user = new User(tokens[0], Convert.ToDouble(tokens[1]), Convert.ToInt32(tokens[2]));

                    userDict[user.UserID] = user;
                }
            }
            else
            {
                Console.WriteLine("Nothing to load");
            }
        }

        private void MainMenu()
        {
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
        }

        private void AccountCreation()
        {
            bool addUsers = true;

            while (addUsers)
            {
                Console.Write("Enter the name: \n");
                string name = Console.ReadLine();

                bool gotInput = false;

                double accountBalance = GetAmount();

                int userID = rand.Next(10, 100);

                User user = new User(name, accountBalance, userID);
                Console.WriteLine($"{user.Name} {user.AccountBalance} {user.UserID}");

                userDict[user.UserID] = user;
                Console.WriteLine("Do you want to continue adding new users?");
                string temp = Console.ReadLine().ToLower();

                addUsers = (temp == "y") ? true : false;
            }
        }

        private void Deposit()
        {
            bool addBalance = true;

            while (addBalance)
            {
                User? user = UserInputHandler();
                double addAmount = GetAmount();

                user.AddBalance(addAmount);
                Console.WriteLine($"New account balance is {user.AccountBalance}");

                Console.WriteLine("Do you want to continue adding to the balance?");
                string temp = Console.ReadLine().ToLower();
                addBalance = (temp == "y") ? true : false;
            }
        }

        private void Withdraw()
        {
            bool withdraw = true;

            while (withdraw)
            {
                User? user = UserInputHandler();

                double withdrawAmount = GetAmount();

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
        }

        private void Balance()
        {
            bool balance = true;

            while (balance)
            {
                User? user = UserInputHandler();

                Console.WriteLine($"Account balance: {user.AccountBalance}");

                Console.WriteLine("Do you want to check another user?");

                string temp = Console.ReadLine().ToLower();
                balance = (temp == "y") ? true : false;
            }
        }

        private void AccountList()
        {
            foreach (var item in userDict)
            {
                User user = item.Value;
                Console.WriteLine($"{user.Name} {user.AccountBalance} {user.UserID}");
            }
        }

        private void Transfer()
        {
            bool transfer = true;
            while (transfer)
            {
                bool gotInput = false;
                User? user1 = UserInputHandler();

                User? user2 = UserInputHandler();

                double transferAmount = GetAmount();


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
                userData.Add($"{user.Name},{user.AccountBalance},{user.UserID}");
            }

            File.WriteAllLines("users.csp", userData);

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

        private double GetAmount()
        {
            string input;
            double amount;
            do
            {
                Console.WriteLine("Enter amount: ");
                input = Console.ReadLine();

                if ((double.TryParse(input, out amount)) == false)
                {
                    Console.WriteLine("Please enter a number!");
                }
            }

            while ((double.TryParse(input, out amount)) == false);

            return amount;
        }
    }
}
