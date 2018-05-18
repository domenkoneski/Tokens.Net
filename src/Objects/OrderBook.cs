using System.Collections.Generic;

namespace Tokens.Objects
{
    public class OrderBook
    {
        public long TimeStamp;
        public List<OrderBookEntry> Bids;
        public List<OrderBookEntry> Asks;

        public OrderBook(long timeStamp)
        {
            this.TimeStamp = timeStamp;
            Bids = new List<OrderBookEntry>();
            Asks = new List<OrderBookEntry>();
        }

        public override string ToString()
        {
            return "Orderbook: " + TimeStamp;
        }

        public class OrderBookEntry
        {
            public double Amount;
            public double Price;
        }
    }
}
