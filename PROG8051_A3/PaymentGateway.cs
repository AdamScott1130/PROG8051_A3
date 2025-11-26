using PROG8051_A3_Account;
using PROG8051_A3_BankAccount;
using PROG8051_A3_GoodsAccount;
using PROG8051_A3_SharesAccount;
using PROG8051_A3_User;
namespace PROG8051_A3_PaymentGateway
{
    public class PaymentGateway()
    {
        static uint GetUintInput(uint numOptions = uint.MaxValue)
        {
            bool validInput = false;
            uint userInput = 0;
            while (validInput == false)
            {
                try
                {
                    userInput = uint.Parse(Console.ReadLine() ?? string.Empty);
                    // Input must be between 1 and numOptions
                    if (userInput < 1 || userInput > numOptions)
                    {
                        Console.WriteLine("Enter a valid input!");
                    }
                    else
                    {
                        validInput = true; // break loop
                    }
                }
                catch
                {
                    Console.WriteLine("Enter a valid input!");
                }
            }
            return userInput;
        }

        static List<User> CreateListOfUsers()
        {
            // Populates a list of users objects with each user having at least one account
            Console.WriteLine("----------");
            Console.WriteLine("Populating system with accounts.");
            List<User> users = new List<User>();
            for (uint i = 1; i < 5; i++)
            {
                List<Account> temp = new List<Account> ();
                temp.Add(new BankAccount(new List<String>(["User" + i]), i));
                temp.Add(new GoodsAccount(new List<String>(["User" + i]), 5+i));
                temp.Add(new SharesAccount(new List<String>(["User" + i]), 10+i));
                users.Add(new User("User"+i, "Pass"+i, "Firstname Lastname", temp));
                Console.WriteLine($"Added User with username {users[(int)i-1].Username()} and password Pass{i}");
            }


            Console.WriteLine("----------");
            return users;
        }
        static string CreateOptionSelector(List<string> options)
        {
            string optionSelector = "";
            for (int i = 0; i < options.Count; i++)
            {
                optionSelector += $"{i + 1}.    {options[i]}\n";
            }
            return optionSelector;


        }

        static void Main()
        {
            List<User> users = CreateListOfUsers();
            List<string> userSelectorOptions = new List<string>(["Select User to Connect", "Exit"]);
            List<string> accountSelectorOptions = new List<string>(["View Accounts", "Select Account", "Exit"]);

            uint input = 0;
            uint accountSelectionInput = 0;
            uint accountFunctionsInput = 0;
            string usernameProvided = "";
            string passwordProvided = "";
            decimal amountEntered = 0.0m;
            string nameEntered = "";
            string additionalInfoEntered = "";
            bool isConnected = false;
            bool validAmountEntered = false;
            User? currentUser = null;
            Account? currentAccount = null;
            while (input != 2)
            {
                Console.Write(CreateOptionSelector(userSelectorOptions));
                input = GetUintInput((uint)userSelectorOptions.Count);
                switch (input)
                {
                    case 1:
                        while (currentUser == null)
                        {
                            Console.Write("Enter Username: ");
                            usernameProvided = Console.ReadLine() ?? string.Empty;

                            foreach (User user in users)
                            {
                                if (user.Username() == usernameProvided)
                                {
                                    currentUser = user;
                                    break;
                                }
                            }
                            if (currentUser == null)
                            {
                                Console.WriteLine("Username not found.");
                            }
                        }
                        // currUser now points to a valid User object. Ask for a password and check if it matches.
                        while (!isConnected)
                        {
                            Console.Write("Enter Password: ");
                            passwordProvided = Console.ReadLine() ?? string.Empty;
                            if (passwordProvided == "0")
                            {
                                break;
                            }
                            isConnected = currentUser.ConnectUser(passwordProvided);
                            if (isConnected)
                            {

                                while (accountSelectionInput != 3)
                                {

                                    Console.Write(CreateOptionSelector(accountSelectorOptions));
                                    accountSelectionInput = GetUintInput((uint)accountSelectorOptions.Count);
                                    switch (accountSelectionInput)
                                    {
                                        case 1:
                                            currentUser.ViewAccounts();
                                            break;
                                        case 2:
                                            Console.WriteLine("Enter the ID of the account you wish to access:");
                                            currentAccount = currentUser.AccountAccess(GetUintInput());
                                            if (currentAccount == null)
                                            {
                                                Console.WriteLine("No account matching ID provided. Try again...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Account Selected. What would you like to do?");
                                                while (accountFunctionsInput != 4) 
                                                {
                                                    Console.Write(CreateOptionSelector(currentAccount.SelectorOptions()));
                                                    accountFunctionsInput = GetUintInput((uint)currentAccount.SelectorOptions().Count);
                                                    switch (accountFunctionsInput)
                                                    {
                                                        case 1://Check Report
                                                            Console.WriteLine(currentAccount.ToString());
                                                            break;
                                                        case 2://Buy
                                                            Console.WriteLine("Enter Amount:");
                                                            while (validAmountEntered == false)
                                                            {
                                                                try
                                                                {
                                                                    amountEntered = decimal.Parse(Console.ReadLine());
                                                                    validAmountEntered = true; // break loop
                                                                }
                                                                catch
                                                                {
                                                                    Console.WriteLine("Enter a valid amount!");
                                                                }
                                                            }
                                                            validAmountEntered = false; // reset for next input


                                                            if (currentAccount is BankAccount)
                                                            {
                                                                currentAccount.Buy(amountEntered);
                                                            }
                                                            else if (currentAccount is SharesAccount)
                                                            {
                                                                Console.WriteLine("Enter Share Name:");
                                                                nameEntered = Console.ReadLine() ?? string.Empty;
                                                                currentAccount.Buy(amountEntered, nameEntered);
                                                            }
                                                            else if (currentAccount is GoodsAccount)
                                                            {
                                                                Console.WriteLine("Enter Goods Name:");
                                                                nameEntered = Console.ReadLine() ?? string.Empty;
                                                                Console.WriteLine("Enter Goods Unit (e.g., kg, grams):");
                                                                additionalInfoEntered = Console.ReadLine() ?? string.Empty;
                                                                currentAccount.Buy(amountEntered, nameEntered, additionalInfoEntered);
                                                            }
                                                            break;
                                                        case 3://Sell
                                                            Console.WriteLine("Enter Amount:");
                                                            while (validAmountEntered == false)
                                                            {
                                                                try
                                                                {
                                                                    amountEntered = decimal.Parse(Console.ReadLine());
                                                                    validAmountEntered = true; // break loop
                                                                }
                                                                catch
                                                                {
                                                                    Console.WriteLine("Enter a valid amount!");
                                                                }
                                                            }
                                                            validAmountEntered = false; // reset for next input
                                                            if (currentAccount is BankAccount)
                                                            {
                                                                currentAccount.Sell(amountEntered);
                                                            }
                                                            else if (currentAccount is SharesAccount)
                                                            {
                                                                Console.WriteLine("Enter Share Name:");
                                                                nameEntered = Console.ReadLine() ?? string.Empty;
                                                                currentAccount.Sell(amountEntered, nameEntered);
                                                            }
                                                            else if (currentAccount is GoodsAccount)
                                                            {
                                                                Console.WriteLine("Enter Goods Name:");
                                                                nameEntered = Console.ReadLine() ?? string.Empty;
                                                                currentAccount.Sell(amountEntered, nameEntered);
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }
                                                }
                                            }
                                                break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}