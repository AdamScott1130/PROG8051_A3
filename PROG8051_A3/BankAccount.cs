using PROG8051_A3_Account;
using PROG8051_A3_User;

namespace PROG8051_A3_BankAccount
{
    public class BankAccount : Account
    {
        // Attributes
        private decimal balance;
        private string currency;
        // Constructors
        public BankAccount(List<User> owners, uint accId, string currency) : base(owners, accId, currency)
        {
            this.balance = 0;
            this.currency = currency;
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
        public void Transfer(decimal transferAmount, BankAccount toBankAccount)
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

        public void Withdraw(decimal amount)
        {
            //validation and operation
            if (amount > 0 && balance >= amount)
            {
                this.balance -= amount;
            }
        }
        public string GetCurrency()
        {
            return this.currency;
        }

        public override string ToString()
        {
            //print details based on action like deposit, transfer, withdraw
            return "The Bank Account details are ";
        }
    }
}
