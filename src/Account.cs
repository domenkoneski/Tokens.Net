using System.Collections.Generic;
using Tokens.Utils;
using Newtonsoft.Json;
using Tokens.Objects;
using Newtonsoft.Json.Linq;
using System;

namespace Tokens
{
    /// <summary>
    /// The "private" part of Tokens platform.
    /// Use this class to access account features such 
    /// as placing orders and balance status.
    /// </summary>
    public class Account
    {
        public Credentials Credentials { get; }

        private JsonSerializerSettings SerializationSettings;

        public Account(string secretKey, string apiKey)
        {
            this.Credentials = new Credentials(apiKey, secretKey);

            SerializationSettings = new JsonSerializerSettings();
            SerializationSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        public Account(Credentials credentials)
        {
            this.Credentials = credentials;

            SerializationSettings = new JsonSerializerSettings();
            SerializationSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        /// <summary>
        /// Get currency balance.
        /// </summary>
        /// <param name="currency">Token/Currency symbol. E.g.: btc, dtr...</param>
        /// <returns></returns>
        public Balance GetBalance(string currency)
        {
            string endpoint = string.Format(Statics.Private_TokenBalance, currency);
            string response = Networking.GetResponseSigned(endpoint, this.Credentials, null);

            return JsonConvert.DeserializeObject<Balance>(response);
        }

        /// <summary>
        /// Get a list of all orders on this account.
        /// </summary>
        /// <returns></returns>
        public List<Order> GetAllOrders(string tradingPair = "")
        {
            string endpoint = tradingPair == "" ?
                              Statics.Private_ListOpenOrdersAll :
                              string.Format(Statics.Private_ListOpenOrdersPair, tradingPair);
                            
            string response = Networking.GetResponseSigned(endpoint, this.Credentials, null);
            JObject responseJson = JObject.Parse(response);

            List<Order> orders = new List<Order>();
            foreach (JObject orderJson in (JArray)responseJson["openOrders"])
                orders.Add(JsonConvert.DeserializeObject<Order>(orderJson.ToString(), this.SerializationSettings));

            return orders;
        }

        /// <summary>
        /// Get order information using its ID.
        /// </summary>
        /// <param name="tradingPairOrID"></param>
        /// <returns></returns>
        public Order GetOrder(string id)
        {
            string endpoint = string.Format(Statics.Private_ListOpenOrder, id);

            string response = Networking.GetResponseSigned(endpoint, this.Credentials, null);

            return JsonConvert.DeserializeObject<Order>(response, this.SerializationSettings);
        }

        /// <summary>
        /// Places new limit order.
        /// </summary>
        /// <param name="tradingPair">E.g. btcusdt</param>
        /// <param name="side">Buy or sell</param>
        /// <param name="amount"></param>
        /// <param name="price"></param>
        /// <returns>Returns order's ID or null if there was an error placing the order.</returns>
        public string AddLimitOrder(string tradingPair, Trade.TradeType side, double amount, double price)
        {
            string endpoint = Statics.Private_AddOpenOrder;

            KeyValuePair<string, string>[] data = {
                new KeyValuePair<string, string>("tradingPair", tradingPair),
                new KeyValuePair<string, string>("side", side.ToString().ToLower()),
                new KeyValuePair<string, string>("amount", amount.ToString()),
                new KeyValuePair<string, string>("price", price.ToString())
            };

            string response = Networking.PostResponseSigned(endpoint, this.Credentials, data);

            JObject responseJson = JObject.Parse(response);
            if ((string) responseJson["status"] == "ok")
                return (string) responseJson["orderdId"];
            return 
                null;
        }

        /// <summary>
        /// Cancels the order.
        /// </summary>
        /// <param name="orderID">Returns true if order was cancelled.</param>
        /// <returns></returns>
        public bool CancelOrder(string orderID)
        {
            string endpoint = string.Format(Statics.Private_CancelOpenOrder, orderID);

            string response = Networking.PostResponseSigned(endpoint, this.Credentials, null);

            JObject responseJson = JObject.Parse(response);

            return (string)responseJson["status"] == "ok";
        }
    }
}
