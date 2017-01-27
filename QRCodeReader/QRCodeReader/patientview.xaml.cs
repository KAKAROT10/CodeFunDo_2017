using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Media;
using System.Threading;
using System.Text;
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
    public partial class patientview : PhoneApplicationPage
    {
        public patientview()
        {
            InitializeComponent();
            this.Loaded += patientview_Loaded;
        }
        dynamic stuff = App.bhagat;
        private async void patientview_Loaded(object sender, RoutedEventArgs e)
        {
            
            if (App.navi == 1)
            {
                
                button1.Content = "Rate this Prescription";
                int l = Convert.ToInt32(stuff.rating);
                if(l==0)
                {
                    button1.Content = "Rate this Prescription";
                }
                else
                {
                    button1.Content = "Rated";
                }
                
            }
            else
            {
                
                g.Visibility = Visibility.Collapsed;
                button1.Content = "Extend Prescription";
            }
            spp.Visibility = Visibility.Collapsed;
            ///processing patient string   
            Patient_Name.Text = stuff[0].pname;
            date_field.Text = stuff[0].pdate;

            ///symptoms.Text = stuff.symptoms;
            ///disgnosis.Text = stuff.diagnosis;
            ///prescription.Text = stuff.prescription;

            StackPanel s_panel = new StackPanel();
            scrollvi.Content = s_panel;
            TextBlock tb1 = new TextBlock();
            tb1.VerticalAlignment = VerticalAlignment.Top;
            tb1.Text = "Symptoms :";
            s_panel.Children.Add(tb1);
            TextBox tb01 = new TextBox();
            tb01.VerticalAlignment = VerticalAlignment.Top;
            tb01.Text = stuff[0].symptoms;
            s_panel.Children.Add(tb01);

           

            TextBlock tb2 = new TextBlock();
            tb2.VerticalAlignment = VerticalAlignment.Top;
            tb2.Text = "Diseases :";
            s_panel.Children.Add(tb2);
            TextBox tb02 = new TextBox();
            tb02.VerticalAlignment = VerticalAlignment.Top;
            tb02.Text = stuff[0].diagnosis;
            s_panel.Children.Add(tb02);
            
            TextBlock tb3 = new TextBlock();
            tb3.VerticalAlignment = VerticalAlignment.Top;
            tb3.Text = " Medicine: ";
            s_panel.Children.Add(tb3);
            ///Network
            string uri2 = "http://prescribecfd.azurewebsites.net/showmedd.php?";
            uri2 = uri2 + "pid=" + stuff[0].pid;
                        
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri2);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic stuff2 = JsonConvert.DeserializeObject(responseBody);
                App.verma = stuff2;
                
            }
            catch (HttpRequestException f)
            {
                App.err = f.Message;
            }
            client.Dispose();

            ///network Offline.
            dynamic stuff3 = App.verma;
            
            StackPanel ss = new StackPanel();
            Grid gd5 = new Grid();
            ColumnDefinition col5 = new ColumnDefinition();
            ColumnDefinition col6 = new ColumnDefinition();
            ColumnDefinition col7 = new ColumnDefinition();
            //ColumnDefinition col4 = new ColumnDefinition();
            gd5.ColumnDefinitions.Add(col5);
            gd5.ColumnDefinitions.Add(col6);
            gd5.ColumnDefinitions.Add(col7);
            //gd.ColumnDefinitions.Add(col4);
            RowDefinition row11 = new RowDefinition();
            row11.Height = new GridLength(50);
            gd5.RowDefinitions.Add(row11);

            TextBlock tb30x = new TextBlock();
            tb30x.VerticalAlignment = VerticalAlignment.Top;
            tb30x.Text = "Medicine";
            TextBlock tb31x = new TextBlock();
            tb31x.VerticalAlignment = VerticalAlignment.Top;
            tb31x.Text = "Days";
            TextBlock tb32x = new TextBlock();
            tb32x.VerticalAlignment = VerticalAlignment.Top;
            tb32x.Text = "Frequency";

            Grid.SetRow(tb30x, 0);
            Grid.SetRow(tb31x, 0);
            Grid.SetRow(tb32x, 0);

            Grid.SetColumn(tb30x, 0);
            Grid.SetColumn(tb31x, 1);
            Grid.SetColumn(tb32x, 2);

            gd5.Children.Add(tb30x);
            gd5.Children.Add(tb31x);
            gd5.Children.Add(tb32x);
            for (int j = 0; ; j++)
            {
                try
                {
                    ColumnDefinition col1 = new ColumnDefinition();
                    ColumnDefinition col2 = new ColumnDefinition();
                    ColumnDefinition col3 = new ColumnDefinition();
                    //ColumnDefinition col4 = new ColumnDefinition();
                    gd5.ColumnDefinitions.Add(col1);
                    gd5.ColumnDefinitions.Add(col2);
                    gd5.ColumnDefinitions.Add(col3);
                    //gd.ColumnDefinitions.Add(col4);
                    RowDefinition row1 = new RowDefinition();
                    row1.Height = new GridLength(50);
                    gd5.RowDefinitions.Add(row1);

                    TextBlock tb30 = new TextBlock();
                    tb30.VerticalAlignment = VerticalAlignment.Top;
                    tb30.Text = stuff3[j].medicine;
                    TextBlock tb31 = new TextBlock();
                    tb31.VerticalAlignment = VerticalAlignment.Top;
                    tb31.Text = stuff3[j].days;
                    TextBlock tb32 = new TextBlock();
                    tb32.VerticalAlignment = VerticalAlignment.Top;
                    tb32.Text = stuff3[j].frequency;

                    Grid.SetRow(tb30, j+1);
                    Grid.SetRow(tb31, j+1);
                    Grid.SetRow(tb32, j+1);

                    Grid.SetColumn(tb30, 0);
                    Grid.SetColumn(tb31, 1);
                    Grid.SetColumn(tb32, 2);

                    gd5.Children.Add(tb30);
                    gd5.Children.Add(tb31);
                    gd5.Children.Add(tb32);
                }
                catch
                {
                    break;
                }
            }
            ss.Children.Add(gd5);
            s_panel.Children.Add(ss);

            TextBlock tb4 = new TextBlock();
            tb4.VerticalAlignment = VerticalAlignment.Top;
            tb4.Text = "Tests :";
            s_panel.Children.Add(tb4);
            TextBox tb04 = new TextBox();
            tb04.VerticalAlignment = VerticalAlignment.Top;
            tb04.Text = stuff[0].test;
            s_panel.Children.Add(tb04);
            
            TextBlock tb5 = new TextBlock();
            tb5.VerticalAlignment = VerticalAlignment.Top;
            tb5.Text = "Comments :";
            s_panel.Children.Add(tb5);
            TextBox tb05 = new TextBox();
            tb05.VerticalAlignment = VerticalAlignment.Top;
            tb05.Text = stuff[0].comment;
            s_panel.Children.Add(tb05);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (App.navi == 1)
            {
                NavigationService.Navigate(new Uri("/pub_user.xaml", UriKind.Relative));
            }
            else if(App.navi == 2)
            {
                NavigationService.Navigate(new Uri("/patientscroller.xaml", UriKind.Relative));
            }
            else if(App.navi == 3)
            {
                NavigationService.Navigate(new Uri("/doctor_history.xaml", UriKind.Relative));
            }
        }
        int zi = 0;
        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            if (App.navi == 0)
            {
                g.Visibility = Visibility.Visible;
                textBox.Text = "";
            }
            else
            {
                if (zi == 0)
                {
                    spp.Visibility = Visibility.Visible;
                    button1.Content = "Extend";
                    zi++;
                }
                else if (zi == 1)
                {
                    string uri2 = "http://prescribecfd.azurewebsites.net/extendpd.php?";
                    string x = stuff[0].nod;
                    
                    int y = Convert.ToInt32(x) + Convert.ToInt32(textBox.Text);
                    
                    uri2 = uri2 + "pid=" + stuff[0].pid + "&duid=" + stuff[0].duid + "&pass=" + App.password + "&days=" + y.ToString();
                    await this.Dispatcher.InvokeAsync(() =>
                    {
                        MessageBox.Show(uri2);
                    });
                    HttpClient client = new HttpClient();
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(uri2);
                        response.EnsureSuccessStatusCode();
                        string responseBody = await response.Content.ReadAsStringAsync();
                        dynamic stuffn = JsonConvert.DeserializeObject(responseBody);
                        App.nunu = stuffn.id;
                    }
                    catch (HttpRequestException f)
                    {
                        App.err = f.Message;
                    }
                    client.Dispose();
                    if (App.nunu == "1")
                    {
                        spp.Visibility = Visibility.Collapsed;
                        await this.Dispatcher.InvokeAsync(() =>
                        {
                            MessageBox.Show("Prescription Addeed.");
                        });
                    }
                    else if (App.nunu == "0")
                    {
                        await this.Dispatcher.InvokeAsync(() =>
                        {
                            MessageBox.Show("Illegal operation!");
                        });
                    }
                }
            }
        }
        int h = 0;
        private async void new_f(object sender, RoutedEventArgs e)
        {
            string uri2 = "http://prescribecfd.azurewebsites.net/prate.php?";
            ///string x = stuff.nod;
            ///int y = Convert.ToInt32(x) + Convert.ToInt32(textBox.Text);
            uri2 = uri2 + "pid=" + stuff.pid + "&puid=" + App.puid + "&rating=" + h.ToString();
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri2);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic stuffn = JsonConvert.DeserializeObject(responseBody);
                App.nunu = stuffn.id;
            }
            catch (HttpRequestException f)
            {
                App.err = f.Message;
            }
            client.Dispose();
            g.Visibility = Visibility.Collapsed;
            button1.Content = "Rated";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            button2.Foreground = new SolidColorBrush(Colors.Yellow);
            button3.Foreground = new SolidColorBrush(Colors.White);
            button4.Foreground = new SolidColorBrush(Colors.White);
            button5.Foreground = new SolidColorBrush(Colors.White);
            button6.Foreground = new SolidColorBrush(Colors.White);
            h = 1;
            this.Loaded += new_f;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            button2.Foreground = new SolidColorBrush(Colors.Yellow);
            button3.Foreground = new SolidColorBrush(Colors.Yellow);
            button4.Foreground = new SolidColorBrush(Colors.White);
            button5.Foreground = new SolidColorBrush(Colors.White);
            button6.Foreground = new SolidColorBrush(Colors.White);
            h = 2;
            this.Loaded += new_f;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            button2.Foreground = new SolidColorBrush(Colors.Yellow);
            button3.Foreground = new SolidColorBrush(Colors.Yellow);
            button4.Foreground = new SolidColorBrush(Colors.Yellow);
            button5.Foreground = new SolidColorBrush(Colors.White);
            button6.Foreground = new SolidColorBrush(Colors.White);
            h = 3;
            this.Loaded += new_f;
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            button2.Foreground = new SolidColorBrush(Colors.Yellow);
            button3.Foreground = new SolidColorBrush(Colors.Yellow);
            button4.Foreground = new SolidColorBrush(Colors.Yellow);
            button5.Foreground = new SolidColorBrush(Colors.Yellow);
            button6.Foreground = new SolidColorBrush(Colors.White);
            h = 4;
            this.Loaded += new_f;
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            button2.Foreground = new SolidColorBrush(Colors.Yellow);
            button3.Foreground = new SolidColorBrush(Colors.Yellow);
            button4.Foreground = new SolidColorBrush(Colors.Yellow);
            button5.Foreground = new SolidColorBrush(Colors.Yellow);
            button6.Foreground = new SolidColorBrush(Colors.Yellow);
            h = 5;
            this.Loaded += new_f;
        }
    }
}