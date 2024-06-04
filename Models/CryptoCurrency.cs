using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfCryptoTracker.Models
{
    public class CryptoCurrency
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double CurrentPrice { get; set; }
        public double MarketCap { get; set; }
        public double TotalVolume { get; set; }
        public double PriceChangePercentage24h { get; set; }
    }
}
