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
                users.Add(new User("User"+i, "Pass"+i, "Firstname Lastname", temp));
                Console.WriteLine($"Added BankAccount with username {users[(int)i-1].Username()} and password Pass{i}");
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
            bool isConnected = false;
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
                                                Console.Write(CreateOptionSelector(currentAccount.SelectorOptions()));
                                                accountFunctionsInput = GetUintInput((uint)currentAccount.SelectorOptions().Count);


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