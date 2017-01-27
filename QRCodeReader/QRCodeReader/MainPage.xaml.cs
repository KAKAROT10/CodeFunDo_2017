using System;
using Microsoft.Phone.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace QRCodeReader
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            InitializeComponent();

            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Scanner.xaml", UriKind.Relative));
            App.pass = 1;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Scanner.xaml", UriKind.Relative));
            App.pass = 2;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ShellToast toast = new ShellToast();
            toast.Title = "Prescribe";
            toast.Content = "It's time to take your medicine!";
            toast.Show();
        }
    }
}
