using System;
using System.Collections.Generic;
using Tokens.Objects;

namespace Tokens.Net
{
    class Examples
    {
        static void Main(string[] args)
        {
            //  Create an account object using generated API and secret keys from your Tokens account. Don't worry, these keys don't work.
            Account account = new Account(apiKey: "ylFzUtpBQOhdPSIobIjKlmZIX7wKPgmU", secretKey: "HkMtoAh3jvxakKCRdRNUa3XeTW7d3nlF");

            //  Get currency balance of your account.
            double balance = account.GetBalance(currency: "dtr").Available;

            // Get orders for a trading pair.
            List<Order> orders = account.GetAllOrders(tradingPair: "dtrusdt");

            //  Or get all orders.
            List<Order> allOrders = account.GetAllOrders();

            //  List one order only by its ID.
            Order order = account.GetOrder("27f7c3d1-ba36-433f-9473-44dc4e57c238");

            //  Place an order.
            string orderID = account.AddLimitOrder("dtrusdt", Trade.TradeType.Sell, 30, 3000);

            //  Order ID is null if there is an error placing the limit order.
            if (orderID != null)
            {
            }

            //  Cancel an order.
            bool result = account.CancelOrder("062843cf-c550-4da1-bb82-734b0fd16f00");
     

            //  Create new exchange object.
            //  Exchange object holds the public access to the Tokens API.
            Exchange exchange = new Exchange();

            //  List all trading pairs on the platform.
            List<TradingPair> pairs = exchange.GetTradingPairs();

            //  Get ticker for BTC/USDT pair.
            Ticker ticker = exchange.GetTicker("btcusdt", hourlyTimeSpan: false);

            //  Or use the list of trading pairs (use ToString() to use the trading pair name).
            ticker = exchange.GetTicker(pairs[0].ToString(), hourlyTimeSpan: false);

            //  Hardcode the pair you wish to list trades for.
            List<Trade> trades = exchange.GetTrades("btcusdt", Utils.TimeFrame.Day);

            //  Or list trades using our list of pairs.
            trades = exchange.GetTrades(pairs[0].ToString(), Utils.TimeFrame.Day);

            //  Get the order book.
            OrderBook orderBook = exchange.GetOrderBook("btcusdt");

            //  Order book timestamp.
            long timeStamp = orderBook.TimeStamp;
            
            //  Check the bids in this order.
            foreach (OrderBook.OrderBookEntry entry in orderBook.Bids)
            {
                double amount = entry.Amount;
                double price = entry.Price;
            }

            Console.ReadLine();
        }
    }
}