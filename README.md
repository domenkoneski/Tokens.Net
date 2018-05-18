# Tokens .Net
C# .Net library to interact with Tokens.Net cryptocurrency exchange.

### Note:
- Tokens.Net exchange is currently in Beta stage, endpoints or API accesses can change.

## Prerequisites:
Library uses Newtonsoft Json.Net, use NuGet `Install-Package Newtonsoft.Json`

# Documentation
## Public Methods
To access public methods such as ticker, order books or trades create new exchange object.
You don't need to create an API key for these methods.
```cs
Exchange exchange = new Exchange();
```

List all trading pairs on the platform.
```cs
List<TradingPair> pairs = exchange.GetTradingPairs();
```

Get ticker for BTC / USDT pair.
```cs
Ticker ticker = exchange.GetTicker("btcusdt", hourlyTimeSpan: false);
```

Or use list of trading pairs to access their tickers.
```cs
List<TradingPair> pairs = exchange.GetTradingPairs();
Ticker ticker = exchange.GetTicker(pairs[0].ToString(), hourlyTimeSpan: false);
```

Get trades for a time frame.
```cs
List<Trade> trades = exchange.GetTrades("btcusdt", Utils.TimeFrame.Day);
List<Trade> trades = exchange.GetTrades(pairs[0].ToString(), Utils.TimeFrame.Day);
```

Access order book for a trading pair
```cs
OrderBook orderBook = exchange.GetOrderBook("btcusdt");
long timeStamp = orderBook.TimeStamp;
foreach (OrderBook.OrderBookEntry entry in orderBook.Bids)
{
    double amount = entry.Amount;
    double price = entry.Price;
}
```

## Private Methods
Make sure to create your API and secret keys on Tokens plaftorm [HERE](https://platform.tokens.net/account/api-keys/). Also make sure to follow [Token's Security Policy](https://www.tokens.net/security-policy/).

Create an account object using generated API and secret keys from your Tokens account. Don't worry, these keys don't work.
```cs
Account account = new Account(apiKey: "ylFzUtpBQOhdPSIobIjKlmZIX7wKPgmU", secretKey: "HkMtoAh3jvxakKCRdRNUa3XeTW7d3nlF");
```

Get account's balance for currency:
```cs
double balance = account.GetBalance(currency: "dtr").Available;
```
Get open orders for a trading pair or get all open orders on account.
```cs
List<Order> orders = account.GetAllOrders(tradingPair: "dtrusdt");
List<Order> allOrders = account.GetAllOrders();
```

List one order only by its ID.
```cs
Order order = account.GetOrder("27f7c3d1-ba36-433f-9473-44dc4e57c238");
```

Place an order. Returns orderID or null if there was an error.
```cs
string orderID = account.AddLimitOrder("dtrusdt", Trade.TradeType.Sell, 30, 3000);
if (orderID != null)
{
  //  Order was placed!
}
```

Cancel an order with corresponding ID.
```cs
bool result = account.CancelOrder("062843cf-c550-4da1-bb82-734b0fd16f00");
```
