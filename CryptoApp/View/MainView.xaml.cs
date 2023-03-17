using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using TestApp.Model;
using static CryptoApp.Core.ReqModels.MarketResModel;
using static TestApp.Model.CoinCapResModel;

namespace CryptoApp.View
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();

            GetCoinsData(TopList);

            
        }

        public static async void GetCoinsData(ListBox TopList)
        {
            string url = "https://api.coincap.io/v2/assets";

            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync(url);
            string content = await res.Content.ReadAsStringAsync();
            CoinCapResModel coinRes = JsonConvert.DeserializeObject<CoinCapResModel>(content);

            for (int i = 0; i < 10; i++)
            {
                coinRes.Data[i].PriceUsd = Math.Round(double.Parse(coinRes.Data[i].PriceUsd), 4).ToString()+"$";

                coinRes.Data[i].Url = $"https://cryptologos.cc/logos/{coinRes.Data[i].Id}-{coinRes.Data[i].Symbol.ToLower()}-logo.png?v=024";

                TopList.Items.Add(coinRes.Data[i]);
            }

            TopList.SelectedItem = TopList.Items[0];
        }

        private void Coin_Change(object sender, SelectionChangedEventArgs e)
        {

            Asset coinId = (Asset)TopList.SelectedItem ;

            AboutView.GetMarketsData(coinId.Id, coinId.Name, coinId.Url);

        }
    }
}
