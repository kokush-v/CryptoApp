using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoApp.Core.ReqModels
{
    public class MarketResModel
    {
       public class Market
       {
            public string ExchangeId { get; set; }
            public string BaseId { get; set; }
            public string QuoteId { get; set; }
            public string BaseSymbol { get; set; }
            public string QuoteSymbol { get; set; }
            public string VolumeUsd24Hr { get; set; }
            public string PriceUsd { get; set; }
            public string VolumePercent { get; set; }
       }

        public Market[] Data {get; set;}
    }
}
