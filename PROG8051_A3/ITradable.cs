namespace PROG8051_A3_ITradable
{
    public interface ITradable
    {
        // Methods
        void Buy(decimal amount, string name = "", string additionalInfo = "");
        void Sell(decimal amount, string name = "");

    }
}
