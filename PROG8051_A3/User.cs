using PROG8051_A3_Account; 
using PROG8051_A3_IConnection;
namespace PROG8051_A3_User
{
    public class User : IConnection
    {
        // Attributes
        string username;
        string password;
        string name;
        List<Account> accounts;
        // Constructors
        public User(string usernameProvided, string passwordProvided, string nameProvided, List<Account> accountsProvided)
        {
            this.username = usernameProvided;
            this.password = passwordProvided;
            this.name = nameProvided;
            this.accounts = accountsProvided;
        }
        // Properties
        public Account AccountAccess(uint idProvided)
            // Returns Account instance corresponding to the ID entered, null if no matching account
        {
            Account accountAccessed = null;
            foreach (Account acct in accounts)
            {
                if (acct.Id == idProvided)
                {
                    accountAccessed = acct;
                    break;
                }
            }
            return accountAccessed;
        }
        // Methods
        public bool ConnectUser(string passwordProvided)
        {
            if (passwordProvided == this.password)
            {
                Console.WriteLine($"Connected to the payment gateway. Welcome {this.name}. ");
                return true;
            }
            else
            {
                Console.WriteLine($"Failed to connect user: Incorrect Password.");
                return false;
            }
        }

        public void DisconnectUser()
        {
            Console.WriteLine($"Disconnected user {this.name} to the payment gateway.");
        }
        public void ViewAccounts()
        {
            foreach (Account acct in this.accounts){
                Console.WriteLine(acct);
            }
        }




    }
}
