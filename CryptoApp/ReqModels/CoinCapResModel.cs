using System;

namespace TestApp.Model
{
    public class CoinCapResModel
    {
        public class Asset
        {
            public string Id { get; set; }
            public string Symbol { get; set; }
            public string Name { get; set; }
            public string Rank { get; set; }
            public string PriceUsd { get; set; }
            public string PriceBtc { get; set; }
            public string VolumeUsd24Hr { get; set; }
            public string MarketCapUsd { get; set; }
            public string AvailableSupply { get; set; }
            public string TotalSupply { get; set; }
            public string MaxSupply { get; set; }
            public string PercentChange1Hr { get; set; }
            public string PercentChange24Hr { get; set; }
            public string PercentChange7D { get; set; }
            public string Url { get; set; }
            public DateTime LastUpdated { get; set; }
        }

        public Asset[] Data { get; set; }

    }



}
