
namespace Tokens.Objects
{
    public class Ticker
    {
        public double Bid;
        public double Open;
        public double Low;
        public double High;
        public double Last;
        public double Ask;
        public double Vwap;
        public long TimeStamp;
        public double Volume;

        public Ticker(double bid, double open, double low, double high, double last, double ask, double vwap, long timeStamp, double volume)
        {
            Bid = bid;
            Open = open;
            Low = low;
            High = high;
            Last = last;
            Ask = ask;
            Vwap = vwap;
            TimeStamp = timeStamp;
            Volume = volume;
        }

        public override string ToString()
        {
            return "Ticker: " + TimeStamp + " Last: " + Last;
        }
    }
}
