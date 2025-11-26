using PROG8051_A3_Account;
using PROG8051_A3_ITradable;
using PROG8051_A3_User;
namespace PROG8051_A3_SharesAccount
{
    public class SharesAccount : Account, ITradable
    {

        //Attributes
        private Dictionary<string, decimal> shares;
        private List<string> selectorOptions = new List<string>(["Check Report", "Buy Shares", "Sell Shares", "Exit"]);

        // Constructors
        public SharesAccount(List<String> owners, uint accId) : base(owners, accId)
        {
            this.shares = new Dictionary<string, decimal>();//creates an empty dictionary, Key = share name, Value = amount owned
        }

        // Properties
        public Dictionary<string, decimal> Shares
        {
            get { return this.shares; }
        }

        // Methods
        public override void Buy(decimal amount, string shareName = "", string additionalInfo = "")
        {
            // For shares:
            // shareName = share name
            // amount = quantity (cast to int)
            int quantity = (int)amount;

            if (!shares.ContainsKey(shareName))
            {
                shares[shareName] = quantity;
            }
            else
            {
                shares[shareName]+= quantity;
            }
        }

        public override void Sell(decimal amount, string shareName)
        {
            int quantity = (int)amount;
            if (shares.ContainsKey(shareName))
            {
                //check quantity
                if (shares[shareName]>= 0)
                {

                    shares[shareName] -= quantity;

                    // If all shares sold, remove from dictionary
                    if (shares[shareName] == 0)
                        shares.Remove(shareName);
                }
                else
                {
                    Console.WriteLine($"Error: Cannot sell {quantity} shares. Only {shares[shareName]} available");
                }
            }
            else
            {
                Console.WriteLine($"Error: You don't own any {shares[shareName]} shares");
            }
        }

        public override List<string> SelectorOptions()
        {
            return this.selectorOptions;
        }

        public override string ToString()
        {
            string accountInfo = "Owner(s): ";
            foreach (string owner in this.Owners)
            {
                accountInfo += $"{owner}    ";
            }

            accountInfo += $"\nAccount Type: Shares Account\nID: {this.Id}\nHoldings:\n";

            foreach (var share in shares)
            {
                accountInfo += $"{share.Key}: {share.Value} shares\n";
            }
            return accountInfo;
        }
    }
}
