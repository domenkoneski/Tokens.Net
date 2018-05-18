
namespace Tokens.Objects
{
    public class Balance
    {
        public double Total;
        public string Currency;
        public double Available;
        public long TimeStamp;

        public Balance(double total, string currency, double available, long timeStamp)
        {
            Total = total;
            Currency = currency;
            Available = available;
            TimeStamp = timeStamp;
        }

        public override string ToString()
        {
            return this.Available.ToString();
        }
    }
}
