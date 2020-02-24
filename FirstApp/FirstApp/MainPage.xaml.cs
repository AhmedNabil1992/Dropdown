using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace FirstApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        // private string result;

        UTF8Encoding utf = new UTF8Encoding();
        string res;
        string result;
        TcpClient client; // Creates a TCP Client
        NetworkStream stream; //Creats a NetworkStream (used for sending and receiving data)
        byte[] datalength = new byte[4]; // creates a new byte with length 4 ( used for receivng data's lenght)
        int i;
        public MainPage()
        {
            InitializeComponent();

            var image = new Image { Source = "palet02.png" };
            VersionLBL.Text = "Version : " + GetVersion();
           

        }
        public string GetVersion()
        {
            return typeof(App).GetTypeInfo().Assembly.GetName().Version.ToString();
        }

        private void Exitapp(object sender, EventArgs args)
        {
            throw new Exception();
        }

        async void ScanQRAsync(object sender, EventArgs args)
        {
            var scanBarObject = new ZXingScannerPage();
            await Navigation.PushAsync(scanBarObject);
            scanBarObject.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    await App.Current.MainPage.DisplayAlert("Result", result.Text, "OK");
                });
            };
            //Handle result
        }
        async void LoginAppAsync(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(Useridtxt.Text))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter User ID", "OK");

                Useridtxt.Focus();
            }
            else if (string.IsNullOrEmpty(Passwordtxt.Text))
            {
                await App.Current.MainPage.DisplayAlert("Empty Values", "Please enter Password", "OK");

                Passwordtxt.Focus();
            }
            else 
            {
                WebClient client = new WebClient();

                string url = "http://192.168.182.122/WMS/login.php?user1=" + Useridtxt.Text + "&" + "pass1=" + Passwordtxt.Text ;

                byte[] html = client.DownloadData(url);
                res = utf.GetString(html);

                if (res == "upX")
                {
                    await App.Current.MainPage.DisplayAlert("Login Error", "Please Check User ID and Password", "OK");
                }
                else
                {
                    string json = res;
                    var mydata = JObject.Parse(json);
                    string Name = mydata["Username"].ToString();
                    string Position = mydata["UserID"].ToString();
                   // await App.Current.MainPage.DisplayAlert("Info", Name, "OK");
                   // await App.Current.MainPage.DisplayAlert("Info", Position, "OK");

                    Application.Current.MainPage = new NavigationPage(new Gridview());

                }
            }
        }
    }
}

