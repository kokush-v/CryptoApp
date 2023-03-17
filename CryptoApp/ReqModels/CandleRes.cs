using System;
using System.Collections.Generic;

public class CandleRes
{
    public List<double> c { get; set; } // Closing prices
    public List<double> h { get; set; } // High prices
    public List<double> l { get; set; } // Low prices
    public List<double> o { get; set; } // Opening prices
    public string s { get; set; } // Status
    public List<long> t { get; set; } // Timestamps
    public List<double> v { get; set; } // Volume
}