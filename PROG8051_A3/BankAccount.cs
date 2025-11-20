using PROG8051_A3_Account;

namespace PROG8051_A3_BankAccount
{
    public class BankAccount : Account
    {
        // Attributes
        decimal balance;
        // Constructors

        // Properties

        // Methods
        private void Buy(decimal amount)
        {
            this.balance -= amount;
        }
        public void Withdraw( decimal amount)
        {
            Buy();
        }
            


    }
}
