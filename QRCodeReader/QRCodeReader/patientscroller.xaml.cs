﻿using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows.Media;
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
    public partial class patientscroller : PhoneApplicationPage
    {
        public patientscroller()
        {
            InitializeComponent();
            this.Loaded += Patientscroller_Loaded;
        }
        string query = "";
        string sort = "0";
        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            sort = "0";
            Patientscroller_Loaded(this, new RoutedEventArgs());
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            sort = "1";
            Patientscroller_Loaded(this, new RoutedEventArgs());
        }
        
        private async void Patientscroller_Loaded(object sender, RoutedEventArgs e)
        {

            string uri2 = "http://prescribecfd.azurewebsites.net/phistoryd.php?";
            uri2 = uri2 + "puid=" + App.puid + "&duid=" + App.duid + "&pass=" + App.password + "&query=" + query + "&sort=" + sort;
                        
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri2.Replace(" ","%20"));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                dynamic stuffy = JsonConvert.DeserializeObject(responseBody);
                
                App.verma = stuffy;
            }
            catch (HttpRequestException f)
            {
                App.err = f.Message;
            }
            client.Dispose();
            
            n1.Visibility = Visibility.Collapsed;
            dynamic stuff = App.verma;
            ///check how to determine the size of array
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
                    textBlock[i].FontSize = 20;
                    name[i].FontSize = 20;
                    date[i].FontSize = 30;
                    rating[i].FontSize = 36;

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
                    b.Click += new RoutedEventHandler((s, f) => next(s, f, b.Name));
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

        private async void next(object sender, RoutedEventArgs f, string s)
        {
            string uri2 = "http://prescribecfd.azurewebsites.net/presp.php?";
            uri2 = uri2 + "pid=" + s + "&duid=" + App.duid + "&pass=" + App.password;
            
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
            App.navi = 2;
            NavigationService.Navigate(new Uri("/patientview.xaml", UriKind.Relative));

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/doctormain.xaml", UriKind.Relative));
        }
        int zi = 0;
        private void button1_Click(object sender, RoutedEventArgs e)
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
                    else query += " AND diagnosis LIKE '" + diagnosis.Text + "'";
                }
                if (symptoms.Text == "")
                {

                }
                else
                {
                    if (symptoms.Text == "" && diagnosis.Text == "") query += " AND dname LIKE '" + doctor_name.Text + "' ";
                    else query += " AND dname LIKE '" + doctor_name.Text + "' ";                    
                }
                Patientscroller_Loaded(this, new RoutedEventArgs());
            }

            
        }
    }
}