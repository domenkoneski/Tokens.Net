using System;

namespace Tokens.Objects
{
    public class Trade
    {
        public enum TradeType { Buy, Sell }

        public long ID;
        public long DateTime;
        public double Price;
        public TradeType Type;
        public double Amount;

        public Trade(long iD, long dateTime, double price, TradeType type, double amount)
        {
            ID = iD;
            DateTime = dateTime;
            Price = price;
            Type = type;
            Amount = amount;
        }

        public override string ToString()
        {
            return "Trade: " + ID + " Date: " + DateTime;
        }
    }
}
