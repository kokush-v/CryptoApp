using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using CryptoApp.View;
using Newtonsoft.Json;
using TestApp.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static TestApp.Model.CoinCapResModel;

namespace CryptoApp
{

    public partial class MainWindow : System.Windows.Window
    {
        public MainView mainView;
        public AboutView aboutView;
        public BrushConverter converter = new BrushConverter();

        public MainWindow()
        {

            InitializeComponent();
            
            mainView = new MainView();
            aboutView = new AboutView();

            MainFrame.Content = mainView;
            Home.IsChecked = true;

            GetCoinsData(SearchBar);

        }

        public static async void GetCoinsData(System.Windows.Controls.ListBox TopList)
        {
            string url = "https://api.coincap.io/v2/assets";

            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync(url);
            string content = await res.Content.ReadAsStringAsync();
            CoinCapResModel coinRes = JsonConvert.DeserializeObject<CoinCapResModel>(content);

            foreach(var item in coinRes.Data)
            {
                item.Url = $"https://cryptologos.cc/logos/{item.Id}-{item.Symbol.ToLower()}-logo.png?v=024";

                TopList.Items.Add(item);
            }

            TopList.SelectedItem = TopList.Items[0];
        }


        private void Home_Checked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = mainView;
        }
        private void About_Checked(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = aboutView;
        }
        private void Theme_Change(object sender, RoutedEventArgs e)
        {
            System.Windows.Window mainWindow = System.Windows.Application.Current.MainWindow;
            IEnumerable<TextBlock> textBlocks = FindVisualChildren<TextBlock>(mainWindow);

            if (ThemeChange.IsChecked == true)
            {
                Body.Background = (Brush)converter.ConvertFromString("#272537");
                foreach (TextBlock textBlock in textBlocks)
                {
                    textBlock.Foreground = Brushes.White; 
                }
            }
            else
            {
                Body.Background = (Brush)converter.ConvertFromString("#D6D6D6");
                foreach (TextBlock textBlock in textBlocks)
                {
                    textBlock.Foreground = Brushes.Black;
                }
            }
        }
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject parent) where T : DependencyObject
        {
            if (parent != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                    if (child is T typedChild)
                    {
                        yield return typedChild;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string sText = SearchText.Text;



            SearchBar.Items.Filter = coin => ((Asset)coin).Name.ToLower().Contains(sText.ToLower());
        }

        private void Coin_Change(object sender, SelectionChangedEventArgs e)
        {

            Asset coinId = (Asset)SearchBar.SelectedItem;

            if (coinId != null)
            {
                AboutView.GetMarketsData(coinId.Id, coinId.Name, coinId.Url);

            }

        }
    }
}
