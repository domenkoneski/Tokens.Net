using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tokens.Objects
{
    public class TradingPair
    {
        public int PriceDecimals;
        public int CounterCurrency;
        public string Title;
        public int BaseCurrency;
        public string Name;
        public double MinAmount;
        public int AmountDecimals;

        public TradingPair(int priceDecimals, int counterCurrency, string title, int baseCurrency, string name, double minAmount, int amountDecimals)
        {
            PriceDecimals = priceDecimals;
            CounterCurrency = counterCurrency;
            Title = title;
            BaseCurrency = baseCurrency;
            Name = name;
            MinAmount = minAmount;
            AmountDecimals = amountDecimals;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
