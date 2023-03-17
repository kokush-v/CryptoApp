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
using static CryptoApp.Core.ReqModels.MarketResModel;
using System.Runtime.InteropServices;

namespace CryptoApp.View
{
    public partial class AboutView : UserControl
    {

        public static ListBox markets;
        public static TextBlock title;
        public static Image logo;
        public static Asset Coin;


        public AboutView()
        {
            InitializeComponent();

            markets = MarketList;
            title = Title;
            logo = Logo;

        }

        

        public static async void GetMarketsData(Asset coin)
        {
            markets.Items.Clear();

            Coin = coin;

            title.Text = coin.Name;

            logo.Source = new BitmapImage(new Uri(coin.Url));

            string url = $"https://api.coincap.io/v2/assets/{coin.Id}/markets";

            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync(url);
            string content = await res.Content.ReadAsStringAsync();
            MarketResModel marketRes = JsonConvert.DeserializeObject<MarketResModel>(content);

           foreach(var item in marketRes.Data)
            {
                if(item.PriceUsd != null) 
                { 
                    item.PriceUsd = Math.Round(double.Parse(item.PriceUsd), 4).ToString() + "$";
                }
                else
                {
                    continue;
                }
                   

                markets.Items.Add(item);
            }

           
            
            if(Coin != null && (Market)markets.Items[0] != null)
            {
                GetCharts(Coin, (Market)markets.Items[0]);
            }
        }

        public static async void GetCharts(Asset coin, Market market)
        {
            string url = $"https://finnhub.io/api/v1/crypto/candle?symbol={market.ExchangeId.ToUpper()}:{coin.Symbol.ToUpper()}{market.QuoteSymbol}&resolution=M&token=cga54lhr01qqlesgdgrgcga54lhr01qqlesgdgs0";

            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync(url);
            string content = await res.Content.ReadAsStringAsync();
            CandleRes charts = JsonConvert.DeserializeObject<CandleRes>(content);

            if (charts.l != null)
            {
                ViewModel.StockPriceDetails.Clear();
                for (int i = 0; i < charts.l.Count; i++)
                {
                    ViewModel.StockPriceDetails.Add(new CandleChartModel() { Date = new DateTime(charts.t[i]), Open = charts.o[i], High = charts.h[i], Low = charts.l[i], Close = charts.c[i] });
                }
            }
            else
            {
                MessageBox.Show("Sorry we couldn`t find this coin on markets", "Eror", MessageBoxButton.OK);
            }


        }

        private void MarketList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (Coin != null && (Market)markets.SelectedItem != null)
            {
                GetCharts(Coin, (Market)markets.SelectedItem);
            }
        }
    }
    public class ViewModel
    {
        public ViewModel()
        {
            StockPriceDetails = new ObservableCollection<CandleChartModel>();
        }
        public static ObservableCollection<CandleChartModel> StockPriceDetails { get; set; }
    }
}
