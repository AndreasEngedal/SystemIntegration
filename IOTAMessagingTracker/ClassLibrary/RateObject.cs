using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassLibrary
{
    public class RateObject
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("symbol")]
        public string Symbol { get; set; }

        [XmlElement("rank")]
        public int Rank { get; set; }

        [XmlElement("price_usd")]
        public double PriceUSD { get; set; }

        [XmlElement("price_btc")]
        public double PriceBTC { get; set; }

        [XmlElement("24h_volume_usd")]
        public string VolumeUSD24h { get; set; }

        [XmlElement("market_cap_usd")]
        public int MarketCapUSD { get; set; }

        [XmlElement("available_supply")]
        public int AvailableSupply { get; set; }

        [XmlElement("total_supply")]
        public int TotalSupply { get; set; }

        [XmlElement("max_supply")]
        public int MaxSupply { get; set; }

        [XmlElement("percent_change_1h")]
        public double PercentChange1h { get; set; }

        [XmlElement("percent_change_24h")]
        public double PercentChange24h { get; set; }

        [XmlElement("percent_change_7d")]
        public double PercentChange7d { get; set; }

        [XmlElement("last_updated")]
        public string LastUpdated { get; set; }
    }
}
