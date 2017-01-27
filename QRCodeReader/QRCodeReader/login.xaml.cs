using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace QRCodeReader
{
    public partial class login : PhoneApplicationPage
    {
        public login()
        {
            InitializeComponent();
            this.Loaded += login_Loaded;
        }

        private void login_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.login_index == "2")
            {
                header_cp.Visibility = Visibility.Collapsed;
                header_sp.Visibility = Visibility.Collapsed;
                header_wt.Visibility = Visibility.Collapsed;
                mobile.Visibility = Visibility.Collapsed;
                header_m.Visibility = Visibility.Collapsed;
                conpassword.Visibility = Visibility.Collapsed;
                sleep.Visibility = Visibility.Collapsed;
                wake.Visibility = Visibility.Collapsed;
            }
            else if (App.login_index == "1")
            {
                header_sp.Visibility = Visibility.Collapsed;
                header_wt.Visibility = Visibility.Collapsed;
                sleep.Visibility = Visibility.Collapsed;
                wake.Visibility = Visibility.Collapsed;
            }
            else if (App.login_index == "3")
            {
                header_p.Visibility = Visibility.Collapsed;
                mobile.Visibility = Visibility.Collapsed;
                header_m.Visibility = Visibility.Collapsed;
                header_cp.Visibility = Visibility.Collapsed;
                password.Visibility = Visibility.Collapsed;
                conpassword.Visibility = Visibility.Collapsed;
            }
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            string NewPassword = string.Empty;
            if (App.login_index == "1")
            {
                if (password.Password == conpassword.Password)
                {
                    NewPassword = conpassword.Password;
                    App.password = NewPassword;
                    App.dmobile = mobile.Text;
                    string uri = "http://prescribecfd.azurewebsites.net/passd.php?";
                    uri = uri + "uid=" + App.duid + "&name=" + App.dname + "&gender=" + App.dgender + "&dob=" + App.dyob + "&address=" + App.daddress + "&pass=" + App.password + "&ex=" + App.login_index+"&mobile="+App.dmobile;
                    ///Console.WriteLine(uri);
                    ///await this.Dispatcher.InvokeAsync(() =>
                    ///{
                    ///    MessageBox.Show(uri);
                    ///});
                    HttpClient client = new HttpClient();
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(uri.Replace(" ","%20"));
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        ///dynamic stuff = JsonConvert.DeserializeObject(responseBody);
                        App.err = responseBody;
                    }
                    catch (HttpRequestException f)
                    {
                        App.err = f.Message;
                    }
                    client.Dispose();
                    ///await this.Dispatcher.InvokeAsync(() =>
                    ///{
                    ///    MessageBox.Show(App.err);
                    ///});
                    NavigationService.Navigate(new Uri("/doctormain.xaml", UriKind.Relative));                                                            
                }
                else
                {
                    await this.Dispatcher.InvokeAsync(() =>
                    {
                        MessageBox.Show("Password Mismatch?");
                    });
                    
                    password.Password = string.Empty;
                    conpassword.Password = string.Empty;
                }
            }
            else if (App.login_index == "2")
            {
                ///pull data from server and pass it as dictionary
                ///password doesn't match flush everything return 0
                ///else return 1
                string garbage = string.Empty;
                string uri = "http://prescribecfd.azurewebsites.net/passd.php?";
                uri = uri + "uid=" + App.duid + "&pass=" + password.Password + "&ex=" + App.login_index;
                ///await this.Dispatcher.InvokeAsync(() =>
                ///{
                ///    MessageBox.Show(uri);
                ///});
                HttpClient client = new HttpClient();
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic stuff = JsonConvert.DeserializeObject(responseBody);
                    garbage = stuff.id;
                }
                catch (HttpRequestException f)
                {
                    App.err = f.Message;
                }
                client.Dispose();
                if (garbage == "1")
                {
                    App.password = password.Password;
                    NavigationService.Navigate(new Uri("/doctormain.xaml", UriKind.Relative));
                }
                else if(garbage == "0")
                {
                    password.Password = string.Empty;
                    mobile.Visibility = Visibility.Visible;
                    mobile.Text = "Incorrect Password!";
                }
            }
            else if (App.login_index == "3")
            {
                string wake_time = wake.Text;
                string sleep_time = sleep.Text;
                string garbage;
                string uri2 = "http://prescribecfd.azurewebsites.net/passp.php?";
                uri2 = uri2 + "uid=" + App.puid + "&name=" + App.pname + "&gender=" + App.pgender + "&dob=" + App.pyob + "&address=" + App.paddress + "&st=" + sleep_time + "&wt=" + wake_time;
                HttpClient client = new HttpClient();
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri2);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic stuff = JsonConvert.DeserializeObject(responseBody);
                    garbage = stuff.id;
                }
                catch (HttpRequestException f)
                {
                    App.err = f.Message;
                }
                client.Dispose();
                ///save to server
                ///pull data from server and pass as dictionary to next page.
                NavigationService.Navigate(new Uri("/pub_user.xaml", UriKind.Relative));
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}