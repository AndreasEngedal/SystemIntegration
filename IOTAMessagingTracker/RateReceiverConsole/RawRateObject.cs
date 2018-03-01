using RestSharp.Deserializers;

namespace RateReceiverConsole
{
	public class RawRateObject
	{
		public string id { get; set; }
		public string name { get; set; }
		public string symbol { get; set; }
		public string rank { get; set; }
		public string price_usd { get; set; }
		public string price_btc { get; set; }
		[DeserializeAs(Name = "24h_volume_usd")]
		public string volume_24h_usd { get; set; }
		public string market_cap_usd { get; set; }
		public string available_supply { get; set; }
		public string total_supply { get; set; }
		public string max_supply { get; set; }
		public string percent_change_1h { get; set; }
		public string percent_change_24h { get; set; }
		public string percent_change_7d { get; set; }
		public string last_updated { get; set; }
	}
}
