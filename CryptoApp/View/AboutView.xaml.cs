using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using Newtonsoft.Json;
using CryptoApp.Core.ReqModels;
using static TestApp.Model.CoinCapResModel;

namespace CryptoApp.View
{
    public partial class AboutView : UserControl
    {

        public static ListBox markets;
        public static TextBlock title;
        public static Image logo;
       
        public AboutView()
        {
            InitializeComponent();

            markets = MarketList;
            title = Title;
            logo = Logo;
        }

        

        public static async void GetMarketsData(String coinId, String coinName, String coinImg)
        {
            markets.Items.Clear();

            title.Text = coinName;

            logo.Source = new BitmapImage(new Uri(coinImg));

            string url = $"https://api.coincap.io/v2/assets/{coinId}/markets";

            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync(url);
            string content = await res.Content.ReadAsStringAsync();
            MarketResModel marketRes = JsonConvert.DeserializeObject<MarketResModel>(content);

            for (int i = 0; i < 10; i++)
            {
                marketRes.Data[i].PriceUsd = Math.Round(double.Parse(marketRes.Data[i].PriceUsd), 4).ToString()+"$";

                markets.Items.Add(marketRes.Data[i]);
            }
        }

        public static void GetCharts(Asset coin)
        {

        }

    }
    public class CandleChartModel
    {
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Open { get; set; }
        public String Status { get; set; }
        public DateTime Date { get; set; }
        public double Volume { get; set; }
    }
    public class ViewModel
    {
        public ViewModel()
        {
            this.StockPriceDetails = new ObservableCollection<CandleChartModel>();
            DateTime date = new DateTime(2012, 4, 1);

            this.StockPriceDetails.Add(new CandleChartModel() { Date = date.AddDays(0), Open = 873.8, High = 878.85, Low = 855.5, Close = 860.5 });
            this.StockPriceDetails.Add(new CandleChartModel() { Date = date.AddDays(1), Open = 873.8, High = 878.85, Low = 855.5, Close = 860.5 });
            
        }
        public ObservableCollection<CandleChartModel> StockPriceDetails { get; set; }
    }
}
