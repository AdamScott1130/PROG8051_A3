using PROG8051_A3_Account;
using PROG8051_A3_ITradable;
using PROG8051_A3_User;

namespace PROG8051_A3_GoodsAccount
{
    // helper class 
    public class GoodsHolding
    {
        //Attributes
        public decimal GoodsAmount;
        public string GoodsUnit;

        //Constructor
        public GoodsHolding(decimal amount, string unit)
        {
            this.GoodsAmount = amount;
            this.GoodsUnit = unit;
        }
    }
    public class GoodsAccount : Account, ITradable
    {
        // Attributes
        public Dictionary<string, GoodsHolding> goods;
        private List<string> selectorOptions = new List<string>(["Check Report", "Buy Goods", "Sell Goods", "Exit"]);

        // Constructors
        public GoodsAccount(List<String> owners, uint accId) : base(owners, accId)
        {
            this.goods = new Dictionary<string, GoodsHolding>();
        }

        // Properties
        public Dictionary<string, GoodsHolding> Goods
        {
            get { return this.goods; }
        }

        // Methods
        public override void Buy(decimal amount, string goodsName, string additionalInfo)
        {
            // For goods:
            // goodsName = good name
            // amount = amount (already decimal)
            // additionalInfo = unit (e.g., "grams", "kg")
            string unit = additionalInfo;

            // check if the goods exist already, if not, create a new entry
            if (!goods.ContainsKey(goodsName))
            {
                goods[goodsName] = new GoodsHolding(amount, unit);//new entry in goods dictionary
            }
            //else, validate goods exist, then 
            else
            {
                if (goods[goodsName].GoodsAmount >= amount)
                {
                    goods[goodsName].GoodsAmount -= amount;
                }
                else
                {
                    Console.WriteLine($"Error: Cannot buy {goodsName} {unit}");
                }
            }

        }
        public override void Sell(decimal amount, string itemName = "")
        {
            // No need to cast - goods can have decimal amounts (0.5 kg of gold)

            if (goods.ContainsKey(itemName))
            {
                if (goods[itemName].GoodsAmount >= amount)
                {
                    // Reduce amount
                    goods[itemName].GoodsAmount -= amount;

                    // If nothing left, remove from dictionary
                    if (goods[itemName].GoodsAmount == 0)
                    {
                        goods.Remove(itemName);
                    }
                }
                else
                {
                    Console.WriteLine($"Error: Cannot sell {amount} {goods[itemName].GoodsUnit} of {itemName}. Only {goods[itemName].GoodsAmount} available.");
                }
            }
            else
            {
                Console.WriteLine($"Error: You don't own any {itemName}.");
            }
        }

        public override List<string> SelectorOptions()
        {
            return this.selectorOptions;
        }
        public override string ToString()
        {
            string result = $"Shares account #{this.Id}\n";
            result += $"Holdings:\n";

            foreach (var good in goods)
            {
                result += $"{good.Key}: {good.Value.GoodsAmount} goods ({good.Value.GoodsUnit}%)\n";
            }
            return result;
        }

    }
}
