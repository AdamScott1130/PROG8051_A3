using PROG8051_A3_Account;
using PROG8051_A3_BankAccount;
using PROG8051_A3_GoodsAccount;
using PROG8051_A3_SharesAccount;
using PROG8051_A3_User;
using System.Diagnostics.Metrics;
namespace PROG8051_A3_PaymentGateway
{
    public class PaymentGateway()
    {
        static uint GetUintInput(uint numOptions)
        {
            bool validInput = false;
            uint userInput = 0;
            while (validInput == false)
            {
                try
                {
                    userInput = uint.Parse(Console.ReadLine());
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
            List<User> users = new List<User>();
            for (int i = 0; i < 5; i++)
            {
                users.Append(new User("User"+i, "Pass"+i, "Firstname Lastname", new List<Account>(new BankAccount(new List<String>(["User"+i]), false, i, 10000.00d)) ));
            }


            return users;
        }
        static string CreateOptionSelector(List<string> options)
        {
            string optionSelector = "";
            for (int i = 0; i < options.Count; i++)
            {
                optionSelector += $"{i + 1}.    {options[i]}";
            }
            return optionSelector;


        }

        static void Main()
        {
            List<User> users = CreateListOfUsers();

            List<string> userSelectorOptions = new List<string>(["Select User to Connect", "Exit"]);
            List<string> accountSelectorOptions = new List<string>(["View Accounts", "Select Account", "Exit"]);
            uint input = 0;
            uint accountInput = 0;
            string usernameProvided = "";
            string passwordProvided = "";
            bool isConnected = false;
            User currentUser = null;
            while (input != 5)
            {
                Console.WriteLine(CreateOptionSelector(userSelectorOptions));
                input = GetUintInput((uint)userSelectorOptions.Count);
                switch (input)
                {
                    case 1:
                        while (currentUser == null)
                        {
                            Console.Write("Enter Username: ");
                            usernameProvided = Console.ReadLine();
                            foreach (User user in users)
                            {
                                if (user.username == usernameProvided)
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
                            passwordProvided = Console.ReadLine();
                            if (passwordProvided == "0")
                            {
                                break;
                            }
                            isConnected = currentUser.ConnectUser(passwordProvided);
                            if (isConnected)
                            {
                                Console.WriteLine(CreateOptionSelector(accountSelectorOptions));
                                accountInput = GetUintInput((uint)accountSelectorOptions.Count);
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