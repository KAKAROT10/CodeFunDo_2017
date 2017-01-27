using System;
using Microsoft.Phone.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;
using Newtonsoft.Json;
using System.Windows.Media;
using System.Threading;
using Windows;

namespace QRCodeReader
{
    public partial class pub_user : PhoneApplicationPage
    {
        public pub_user()
        {
            InitializeComponent();
            this.Loaded += Pub_user_Loaded;
        }

        private void Pub_user_Loaded(object sender, RoutedEventArgs e)
        {
            n1.Visibility = Visibility.Collapsed;
            Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
            thread.Start();
        }
        string query = "";
        string sort = "0";
        public async void WorkThreadFunction()
        {
            await this.Dispatcher.InvokeAsync(() =>
            {
                MessageBox.Show("Thread is running!");
            });

            try
            {
                ShellToast toast = new ShellToast();
                toast.Title = "Prescribe";
                toast.Content = "It's time to take your medicine!";
                while(true)
                {
                    TimeSpan t2 = new TimeSpan(22, 0, 0);
                    TimeSpan t1 = new TimeSpan(07, 0, 0);
                    TimeSpan t3 = new TimeSpan(14, 0, 0);
                    TimeSpan now = DateTime.Now.TimeOfDay;

                    if (now == t1 || now == t2 || now == t3)
                    {
                        toast.Show();
                        int millisecond = 60000;
                        Thread.Sleep(millisecond);
                    }
                }
            }
            catch(Exception ex)
            {
                App.err = ex.ToString();
            }
        }

        private async void button2_Click(object sender, RoutedEventArgs e)
        {
            string uri2 = "http://prescribecfd.azurewebsites.net/regp.php?";
            uri2 = uri2 + "&puid=" + App.puid.ToString();
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri2);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic stuff1 = JsonConvert.DeserializeObject(responseBody);
                App.verma = stuff1;
            }
            catch (HttpRequestException f)
            {
                App.err = f.Message;
            }
            client.Dispose();

            dynamic stuff = App.verma;
            int n = stuff.Length;
            TextBlock[] duid = new TextBlock[n];
            TextBlock[] mobile = new TextBlock[n];
            TextBlock[] name = new TextBlock[n];
            ///TextBlock[] rating = new TextBlock[n];
            ///int topMargin = 20;
            ///int leftMargin = 20;
            ///int right = 0;
            ///int bottom = 0;
            ///pan.AutoScroll = true;           
            StackPanel father = new StackPanel();
            StackPanel[] pa = new StackPanel[n];
            scrollvi.Content = father;
            for (int i = 0; i < n; i++)
            {
                Button b = new Button();
                duid[i] = new TextBlock();
                name[i] = new TextBlock();
                ///rating[i] = new TextBlock();
                mobile[i] = new TextBlock();
                duid[i].VerticalAlignment = VerticalAlignment.Top;
                name[i].VerticalAlignment = VerticalAlignment.Top;
                mobile[i].VerticalAlignment = VerticalAlignment.Top;
                ///rating[i].VerticalAlignment = VerticalAlignment.Top;
                duid[i].Text = stuff[i].duid                                                               ;
                name[i].Text = stuff[i].name;
                mobile[i].Text = stuff[i].mobile;
                ///rating[i].Text = stuff[i].ratingdata;
                duid[i].FontSize = 20;
                name[i].FontSize = 20;
                mobile[i].FontSize = 20;
                ///rating[i].FontSize = 56;

                pa[i] = new StackPanel();
                ///pa[i].Orientation = Orientation.Vertical;
                Grid DJ = new Grid();

                DJ.Background = new SolidColorBrush(Colors.Black);
                DJ.Width = 400;
                DJ.HorizontalAlignment = HorizontalAlignment.Left;
                DJ.VerticalAlignment = VerticalAlignment.Top;

                ///Create Colums
                ColumnDefinition col1 = new ColumnDefinition();
                ///ColumnDefinition col2 = new ColumnDefinition();
                ///ColumnDefinition col3 = new ColumnDefinition();
                DJ.ColumnDefinitions.Add(col1);
                ///DJ.ColumnDefinitions.Add(col2);
                ///DJ.ColumnDefinitions.Add(col3);

                ///Create Row
                RowDefinition row1 = new RowDefinition();
                row1.Height = new GridLength(50);
                RowDefinition row2 = new RowDefinition();
                row2.Height = new GridLength(50);

                ///RowDefinition row_middle2 = new RowDefinition();
                DJ.RowDefinitions.Add(row1);
                DJ.RowDefinitions.Add(row2);

                ///Grid.SetRow(rating[i], 0);
                ///Grid.SetColumn(rating[i], 0);
                Grid.SetRow(name[i], 0);
                Grid.SetColumn(name[i], 0);
                ///Grid.SetRow(date[i], 0);
                ///Grid.SetColumn(date[i], 2);
                Grid.SetRow(mobile[i], 1);
                Grid.SetColumn(mobile[i], 0);
                DJ.Children.Add(mobile[i]);
                DJ.Children.Add(name[i]);
                ///DJ.Children.Add(date[i]);
                ///DJ.Children.Add(textBlock[i]);
                ///date[i].Margin = new Thickness(topMargin, leftMargin, right, bottom);
                ///name[i].Margin = new Thickness(topMargin, leftMargin, right, bottom);
                ///rating[i].Margin = new Thickness(topMargin, leftMargin, right, bottom);                
                ///textBlock[i].Margin = new Thickness(topMargin, leftMargin, right, bottom);
                ///topMargin += 20;            
                ///textBlock[i].Foreground = new SolidColorBrush(Colors.Red);
                pa[i].Children.Add(DJ);
                b.Content = pa[i];
                b.Name = stuff[i].mobile;
                b.Click += new RoutedEventHandler((s, f) => next(s, f, b.Name));
                ///if (b.IsPressed == true)
                ///{
                ///b.Click += new RoutedEventHandler(next);
                ///b.Click += work(b.Name);
                ///    App.p = b.Name;
                ///}                               
                father.Children.Add(b);
            }
        }

        private async void next(object sender, RoutedEventArgs f, string s)
        {
            ///call doctor  
            ///Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI("9431669899", "Rohit");
            ///Windows.Phone
            PhoneCallTask t1 = new PhoneCallTask();

            t1.PhoneNumber = s;
            t1.DisplayName = "Doctor";

            t1.Show();            
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            n1.Visibility = Visibility.Collapsed;
            string uri2 = "http://prescribecfd.azurewebsites.net/history.php?";
            uri2 = uri2 + "puid=" + App.puid.ToString() + "&query=" + query + "&sort=" + sort;

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
                dynamic stuff1 = JsonConvert.DeserializeObject(responseBody);
                App.verma = stuff1;
                await this.Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show(App.verma.ToString());
                });
            }
            catch (HttpRequestException f)
            {
                App.err = f.Message;
                await this.Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show(App.err);
                });
            }
            client.Dispose();
                       

            dynamic stuff = App.verma;
            int n = 1000;
            TextBlock[] textBlock = new TextBlock[n];
            TextBlock[] date = new TextBlock[n];
            TextBlock[] name = new TextBlock[n];
            TextBlock[] rating = new TextBlock[n];
            ///int topMargin = 20;
            ///int leftMargin = 20;
            ///int right = 0;
            ///int bottom = 0;
            ///pan.AutoScroll = true;           
            StackPanel father = new StackPanel();
            StackPanel[] pa = new StackPanel[n];
            scrollvi.Content = father;
            for (int i = 0; ; i++)
            {
                try
                {
                    Button b = new Button();
                    textBlock[i] = new TextBlock();
                    name[i] = new TextBlock();
                    rating[i] = new TextBlock();
                    date[i] = new TextBlock();
                    textBlock[i].VerticalAlignment = VerticalAlignment.Top;
                    name[i].VerticalAlignment = VerticalAlignment.Top;
                    date[i].VerticalAlignment = VerticalAlignment.Top;
                    rating[i].VerticalAlignment = VerticalAlignment.Top;
                    textBlock[i].Text = stuff[i].diagnosis;
                    name[i].Text = stuff[i].dname;
                    date[i].Text = stuff[i].pdate;
                    rating[i].Text = stuff[i].rating;
                    textBlock[i].FontSize = 24;
                    name[i].FontSize = 24;
                    date[i].FontSize = 40;
                    rating[i].FontSize = 56;

                    pa[i] = new StackPanel();
                    ///pa[i].Orientation = Orientation.Vertical;
                    Grid DJ = new Grid();

                    DJ.Background = new SolidColorBrush(Colors.Black);
                    DJ.Width = 400;
                    DJ.HorizontalAlignment = HorizontalAlignment.Left;
                    DJ.VerticalAlignment = VerticalAlignment.Top;

                    ///Create Colums
                    ColumnDefinition col1 = new ColumnDefinition();
                    ColumnDefinition col2 = new ColumnDefinition();
                    ColumnDefinition col3 = new ColumnDefinition();
                    DJ.ColumnDefinitions.Add(col1);
                    DJ.ColumnDefinitions.Add(col2);
                    DJ.ColumnDefinitions.Add(col3);

                    ///Create Row
                    RowDefinition row1 = new RowDefinition();
                    row1.Height = new GridLength(50);
                    RowDefinition row2 = new RowDefinition();
                    row2.Height = new GridLength(50);
                    ///RowDefinition row_middle2 = new RowDefinition();
                    DJ.RowDefinitions.Add(row1);
                    DJ.RowDefinitions.Add(row2);

                    Grid.SetRow(rating[i], 0);
                    Grid.SetColumn(rating[i], 0);
                    Grid.SetRow(name[i], 0);
                    Grid.SetColumn(name[i], 1);
                    Grid.SetRow(date[i], 0);
                    Grid.SetColumn(date[i], 2);
                    Grid.SetRow(textBlock[i], 1);
                    Grid.SetColumn(textBlock[i], 1);
                    DJ.Children.Add(rating[i]);
                    DJ.Children.Add(name[i]);
                    DJ.Children.Add(date[i]);
                    DJ.Children.Add(textBlock[i]);
                    ///date[i].Margin = new Thickness(topMargin, leftMargin, right, bottom);
                    ///name[i].Margin = new Thickness(topMargin, leftMargin, right, bottom);
                    ///rating[i].Margin = new Thickness(topMargin, leftMargin, right, bottom);                
                    ///textBlock[i].Margin = new Thickness(topMargin, leftMargin, right, bottom);
                    ///topMargin += 20;            
                    ///textBlock[i].Foreground = new SolidColorBrush(Colors.Red);
                    pa[i].Children.Add(DJ);
                    b.Content = pa[i];
                    b.Name = stuff[i].pid;
                    b.Click += new RoutedEventHandler((s, f) => next2(s, f, b.Name));
                    ///if (b.IsPressed == true)
                    ///{
                    ///b.Click += new RoutedEventHandler(next);
                    ///b.Click += work(b.Name);
                    ///    App.p = b.Name;
                    ///}                               
                    father.Children.Add(b);
                }
                catch
                {
                    break;
                }
            }
        }

        private async void next2(object sender, RoutedEventArgs f, string s)
        {
            string uri2 = "http://prescribecfd.azurewebsites.net/prescriptionp.php?";
            uri2 = uri2 + "pid=" + s;
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri2);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic stuff = JsonConvert.DeserializeObject(responseBody);
                App.bhagat = stuff;
            }
            catch (HttpRequestException g)
            {
                App.err = g.Message;
            }
            client.Dispose();
            App.navi = 1;
            NavigationService.Navigate(new Uri("/patientview.xaml", UriKind.Relative));

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {            
            sort = "0";
            button1_Click(this, new RoutedEventArgs());
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            sort = "1";
            button1_Click(this, new RoutedEventArgs());
        }
        int zi = 0;
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (zi == 0)
            {
                n1.Visibility = Visibility.Visible;
                zi++;
            }
            else
            {
                if (symptoms.Text == "")
                {

                }
                else
                {
                    query += " AND symptoms LIKE '" + symptoms.Text + "'";
                }
                if (diagnosis.Text == "")
                {

                }
                else
                {
                    if (symptoms.Text == "") query += " AND diagnosis LIKE '" + diagnosis.Text + "'";
                    else query += "AND diagnosis LIKE '" + diagnosis.Text + "'";
                }
                if (symptoms.Text == "")
                {

                }
                else
                {
                    if (symptoms.Text == "" && diagnosis.Text == "") query += " AND dname LIKE '" + patient_name.Text + "' ";
                    else query += " AND dname LIKE '" + patient_name.Text + "' ";
                }
                zi = 0;

                button1_Click(this, new RoutedEventArgs());

            }                        
        }
    }
}