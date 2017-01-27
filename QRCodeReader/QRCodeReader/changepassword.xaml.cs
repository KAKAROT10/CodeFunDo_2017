using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace QRCodeReader
{
    public partial class changepassword : PhoneApplicationPage
    {
        public changepassword()
        {
            InitializeComponent();
        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            ///App.password = password.Password;
            ///import to server
            ///old password , uid, new password
            ///0 if false flush msg wrong password
            ///1 if true
            string garbage;
            string uri = "http://prescribecfd.azurewebsites.net/changepassd.php?";
            uri = uri + "duid=" + App.duid + "&pass=" + oldpassword.Password + "&npass=" + password.Password;
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic stuff = JsonConvert.DeserializeObject(responseBody);
                App.err = stuff.id;
            }
            catch (HttpRequestException f)
            {
                App.err = f.Message;
            }
            client.Dispose();
            if(App.err == "0")
            {
                password.Password = string.Empty;
                oldpassword.Password = string.Empty;
                conpassword.Password = string.Empty;

                await this.Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show("Password Mismatch?");
                });
            }
            else if(App.err == "1")
            {
                App.password = password.Password;
                NavigationService.Navigate(new Uri("/doctormain.xaml", UriKind.Relative));
            }
            ///NavigationService.Navigate(new Uri("/doctormain.xaml", UriKind.Relative));
        }

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/doctormain.xaml", UriKind.Relative));
        }
    }
}