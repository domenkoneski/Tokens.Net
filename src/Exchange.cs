using System.Collections.Generic;
using Tokens.Objects;
using Tokens.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Tokens
{
    /// <summary>
    /// The "public" part of accessing Tokens platform such as
    /// trades, tickers and order books.
    /// </summary>
    public class Exchange
    {
        public Exchange() { }

        /// <summary>
        /// Returns ticker for the last day.
        /// </summary>
        /// <param name="tradingPair">Trading pair, e.g. "btcusdt"</param>
        /// <param name="hourlyTimeSpan">Retrieve ticker for the last hour instead of last day.</param>
        /// <returns></returns>
        public Ticker GetTicker(string tradingPair, bool hourlyTimeSpan = false)
        {
            string endPoint = hourlyTimeSpan ? Statics.Public_Ticker : Statics.Public_TickerHour;
            endPoint = string.Format(endPoint, tradingPair);

            return JsonConvert.DeserializeObject<Ticker>(Networking.GetResponse(endPoint));
        }

        /// <summary>
        /// Return lists of trades for specific trading pair and time frame.
        /// </summary>
        /// <param name="tradingPair">E.g. "btcusdt"</param>
        /// <param name="timeFrame">Time frame (daily, hourly, minute frame).</param>
        /// <returns></returns>
        public List<Trade> GetTrades(string tradingPair, TimeFrame timeFrame)
        {
            string endpoint = string.Format(Statics.Public_Trades, timeFrame.ToString().ToLower(), tradingPair);
            string response = Networking.GetResponse(endpoint, null);
      
            JObject tradeArray = JObject.Parse(response);

            List<Trade> trades = new List<Trade>();
            foreach (JObject tradeObject in (JArray) tradeArray["trades"])
                trades.Add(JsonConvert.DeserializeObject<Trade>(tradeObject.ToString()));

            return trades;
        }

        /// <summary>
        /// Retrieves lists of all trading pairs on Token exchange.
        /// </summary>
        /// <returns></returns>
        public List<TradingPair> GetTradingPairs()
        {
            string endpoint = Statics.Public_ListTradingPairs;
            string response = Networking.GetResponse(endpoint, null);

            JArray pairsArray = JArray.Parse(response);

            List<TradingPair> pairs = new List<TradingPair>();
            foreach (JObject pairObject in pairsArray)
                pairs.Add(JsonConvert.DeserializeObject<TradingPair>(pairObject.ToString()));

            return pairs;
        }

        public OrderBook GetOrderBook(string tradingPair)
        {
            string endpoint = string.Format(Statics.Public_OrderBook, tradingPair);
            string response = Networking.GetResponse(endpoint);

            JObject bookJson = JObject.Parse(response);

            OrderBook orderBook = new OrderBook((long)bookJson["timestamp"]);
            
            foreach (JArray bid in (JArray) bookJson["bids"])
            {
                orderBook.Bids.Add(new OrderBook.OrderBookEntry()
                {
                    Amount = double.Parse(bid[0].ToString()),
                    Price = double.Parse(bid[1].ToString())
                });
            }

            foreach (JArray ask in (JArray)bookJson["asks"])
            {
                orderBook.Asks.Add(new OrderBook.OrderBookEntry()
                {
                    Amount = double.Parse(ask[0].ToString()),
                    Price = double.Parse(ask[1].ToString())
                });
            }

            return orderBook;
        }
    }
}