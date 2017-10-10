using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using TradeMaster.Infrastructure;

namespace TradeMaster.UI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginWindowViewModel _viewModel;
        public LoginWindow()
        {
            InitializeComponent();

            DataContext = _viewModel = new LoginWindowViewModel();
            Loaded += LoginWindow_Loaded;
        }

        private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //if (File.Exists(@"D:\accessToken.txt") == false || File.ReadAllText(@"D:\accessToken.txt").Length == 0)
            //{
                webBrowser.Navigate(AuthenticationManager.LoginURL);
            //}
            //else
            //{
            //    AuthenticationManager.AccessToken = File.ReadAllText(@"D:\accessToken.txt");
            //    MainWindow mainWindow = new MainWindow() { AccessToken = AuthenticationManager.AccessToken };
            //    mainWindow.Show();
            //    this.Close();
            //}
        }

        private void WebBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            string url = e.Uri.AbsoluteUri;
            string[] list = url.Split("&".ToCharArray());
            foreach (string str in list)
            {
                string[] values = str.Split("=".ToCharArray());
                if (values[0].Equals("request_token"))
                {
                    AuthenticationManager.AccessToken = values[1];
                    AppConstants.ApiKey = AuthenticationManager.APIKey;
                    AppConstants.Apisecret = AuthenticationManager.Apisecret;                    
                    string token = values[1];

                    //kitecon = new KiteConnect(apiKey);
                    //dynamic data = DataHelper.Saveaccesstoken(ref connection, token);
                    //AppConstants.AccessToken = data["access_token"];
                    //AppConstants.PublicToken = data["public_token"];
                    //AppConstants.UserId = data["user_id"];
                    //connection.SetAccessToken(AppConstants.AccessToken);


                    File.WriteAllText(@"D:\accessToken.txt", AuthenticationManager.AccessToken);
                    MainWindow mainWindow = new MainWindow() { AccessToken = token, DataContext = new MainWindowViewModel(token) };
                    mainWindow.Show();

                    this.Close();
                }
            }
        }
    }
}
