
namespace Tokens.Utils
{
    /// <summary>
    /// Various endpoints for public and private methods.
    /// </summary>
    public static class Statics
    {
        #region Public GET Methods
        public static string Public_Ticker = "https://api.tokens.net/public/ticker/{0}/";
        public static string Public_TickerHour = "https://api.tokens.net/public/ticker/hour/{0}/";

        public static string Public_Trades = "https://api.tokens.net/public/trades/{0}/{1}/";

        public static string Public_OrderBook = "https://api.tokens.net/public/order-book/{0}/";
        public static string Public_ListTradingPairs = "https://api.tokens.net/public/trading-pairs/get/all/";
        #endregion

        #region Private GET Methods
        public static string Private_TokenBalance = "https://api.tokens.net/private/balance/{0}/";
        public static string Private_ListOpenOrdersAll = "https://api.tokens.net/private/orders/get/all/";
        public static string Private_ListOpenOrder = "https://api.tokens.net/private/orders/get/{0}/";
        public static string Private_ListOpenOrdersPair = "https://api.tokens.net/private/orders/get/{0}/";
        #endregion

        #region Private POST Methods
        public static string Private_AddOpenOrder = "https://api.tokens.net/private/orders/add/limit/";
        public static string Private_CancelOpenOrder = "https://api.tokens.net/private/orders/cancel/{0}/";
        #endregion
    }

    public enum TimeFrame
    {
        Day, Hour, Minute 
    }
}
