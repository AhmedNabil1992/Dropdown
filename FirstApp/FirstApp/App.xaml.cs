using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
namespace FirstApp
{
    public partial class App : Application
    {
        UTF8Encoding utf = new UTF8Encoding();
        string res;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

        }

        protected override async void OnStart()
        {

            try
            {
                TcpClient client = new TcpClient();
                await client.ConnectAsync("192.168.1.10", 9999);
                if (client.Connected)
                {
                    Connection.Instance.client = client;
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
               
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
