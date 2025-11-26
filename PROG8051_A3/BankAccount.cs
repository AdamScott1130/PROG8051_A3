using PROG8051_A3_Account;
using PROG8051_A3_User;

namespace PROG8051_A3_BankAccount
{
    public class BankAccount : Account
    {
        // Attributes
        private decimal balance;
        private List<string> selectorOptions = new List<string>(["Check Balance", "Deposit", "Withdraw", "Exit"]);
        // Constructors
        public BankAccount(List<String> owners, uint accId, decimal balanceProvided = 0) : base(owners, accId)
        {
            this.balance = balanceProvided;
        }
        // Properties
        public decimal GetBalance
        {
            get { return this.balance; }
        }


        // Methods
        public void Deposit(decimal amount)
        {
            //validation and operation
            if (amount > 0)
            {
                this.balance += amount;
            }
        }
        private void Transfer(decimal transferAmount, BankAccount toBankAccount)
        {
            //transfer money to another account
            // deduct from original "from" account, add to the "to" account
            // validation and opearation

            if (transferAmount > 0 && this.balance >= transferAmount)//
            {
                this.balance -= transferAmount;//"this" is the FROM account where amount will be transferred and deducted from
                toBankAccount.Deposit(transferAmount);//when you modify toBankAccount, you modify object at the reference memory location
            }

        }

        private void Withdraw(decimal amount)
        {
            //validation and operation
            if (amount > 0 && balance >= amount)
            {
                this.balance -= amount;
            }
        }

        public override void Buy(decimal amount, string name = "", string additionalInfo = "")
        {
            Deposit(amount);
        }

        public override void Sell(decimal amount, string name = "")
        {
            Withdraw(amount);
        }

        public override List<string> SelectorOptions()
        {
            return this.selectorOptions;
        }

        public override string ToString()
        {
            //print details based on action like deposit, transfer, withdraw
            string accountInfo = "Owner(s): ";
            foreach (string owner in this.Owners)
            {
                accountInfo += $"{owner}    ";
            }
            accountInfo += $"\nID: {this.Id}\nBalance: {this.balance:C}";
            return accountInfo;
        }
    }
}
