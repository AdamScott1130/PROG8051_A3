using PROG8051_A3_Account;
using PROG8051_A3_ITradable;
using PROG8051_A3_User;
namespace PROG8051_A3_SharesAccount
{
    public class SharesAccount : Account
    {
        // helper class 
        public class ShareHolding
        {
            //Attributes
            public int SharesAmount;
            public decimal SharesPercent;

            //Constructor
            public ShareHolding(int amount, decimal percent)
            {
                this.SharesAmount = amount;
                this.SharesPercent = percent;
            }
        }
        public class SharesAccount : Account, ITradable
        {
            //Attributes
            private Dictionary<string, ShareHolding> shares;

            // Constructors
            public SharesAccount(List<User> owners, uint accId, string providedCurrency) : base(owners, accId, providedCurrency)
            {
                this.shares = new Dictionary<string, ShareHolding>();//creates an empty dictionary, Key = share name, Value = Shareholding Obj (Quantity and percentage)
            }

            // Properties
            public Dictionary<string, ShareHolding> Shares
            {
                get { return this.shares; }
            }

            // Methods
            public void Buy(string shareName, decimal amount, string additionalInfo)
            {
                // For shares:
                // shareName = share name
                // amount = quantity (cast to int)
                // additionalInfo = percentage (parse as string)
                int quantity = (int)amount;
                decimal percentage = decimal.Parse(additionalInfo);

                if (!shares.ContainsKey(shareName))
                {
                    shares[shareName] = new ShareHolding(quantity, percentage);
                }
                else
                {
                    shares[shareName].SharesAmount += quantity;
                    shares[shareName].SharesPercent += percentage;
                }
            }

            public void Sell(string shareName, decimal amount)
            {
                int quantity = (int)amount;
                if (shares.ContainsKey(shareName))
                {
                    //check quantity
                    if (shares[shareName].SharesAmount >= 0)
                    {
                        // Calculate what percentage of shares we're selling
                        decimal percentageToSell = shares[shareName].SharesPercent * quantity / shares[shareName].SharesAmount;

                        shares[shareName].SharesAmount -= quantity;
                        shares[shareName].SharesPercent -= percentageToSell;

                        // If all shares sold, remove from dictionary
                        if (shares[shareName].SharesAmount == 0)
                            shares.Remove(shareName);
                    }
                    else
                    {
                        Console.WriteLine($"Error: Cannot sell {quantity} shares. Only {shares[shareName].SharesAmount} available");
                    }
                }
                else
                {
                    Console.WriteLine($"Error: You don't own any {shares[shareName]} shares");
                }
            }

            public override string ToString()
            {
                string result = $"Shares account #{this.Id}\n";
                result += $"Holdings:\n";

                foreach (var share in shares)
                {
                    result += $"{share.Key}: {share.Value.SharesAmount} shares ({share.Value.SharesPercent}%)\n";
                }
                return result;
            }
        }
    }
}
