using System.Xml.Serialization;

namespace MessageTranslator
{
	[XmlRoot("RawRateObject")]
    public class RawRateObject
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
        public double MarketCapUSD { get; set; }

        [XmlElement("available_supply")]
        public double AvailableSupply { get; set; }

        [XmlElement("total_supply")]
        public double TotalSupply { get; set; }

        [XmlElement("max_supply")]
        public double MaxSupply { get; set; }

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
