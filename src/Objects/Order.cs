namespace Tokens.Objects
{
    public class Order
    {
        public long Timestamp;
        public string ID;
        public long Created;
        public Trade.TradeType Type;
        public string OrderStatus;
        public double Price;
        public double Amount;
        public double RemainingAmount;
        public string CurrencyPair;
        public string TakeProfit;

        public Order(long timestamp, string iD, long created, Trade.TradeType type, string orderStatus, double price, double amount, double remainingAmount, string currencyPair, string takeProfit)
        {
            Timestamp = timestamp;
            ID = iD;
            Created = created;
            Type = type;
            OrderStatus = orderStatus;
            Price = price;
            Amount = amount;
            RemainingAmount = remainingAmount;
            CurrencyPair = currencyPair;
            TakeProfit = takeProfit;
        }

        public override string ToString()
        {
            return "Order: " + this.ID;
        }
    }
}