namespace IOTAMessagingTracker
{
	public class RateObject
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Symbol { get; set; }
		public int Rank { get; set; }
		public double PriceUSD { get; set; }
		public double PriceBTC { get; set; }
		public string VolumeUSD24h { get; set; }
		public double MarketCapUSD { get; set; }
		public double AvailableSupply { get; set; }
		public double TotalSupply { get; set; }
		public double MaxSupply { get; set; }
		public double PercentChange1h { get; set; }
		public double PercentChange24h { get; set; }
		public double PercentChange7d { get; set; }
		public string LastUpdated { get; set; }
	}
}
