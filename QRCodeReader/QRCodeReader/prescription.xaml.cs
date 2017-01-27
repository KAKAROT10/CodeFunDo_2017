using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ZXing.Mobile;

namespace QRCodeReader
{
    public partial class prescription : PhoneApplicationPage
    {
        private MobileBarcodeScanner _scanner;
        public prescription()
        {
            InitializeComponent();
            this.Loaded += prescription_Loaded;
        }
        String uid, name, gender, yob, address, xml;
        TextBox tb05 = new TextBox();
        StackPanel stap1 = new StackPanel();
        StackPanel stap2 = new StackPanel();
        StackPanel stap3 = new StackPanel();
        StackPanel stap4 = new StackPanel();
        string symptoms="", diagnosis="", test="", prescription1="", comment="";
        private async void prescription_Loaded(object sender, RoutedEventArgs e)
        {
            _scanner = new MobileBarcodeScanner(this.Dispatcher);
            _scanner.UseCustomOverlay = false;
            _scanner.TopText = "Hold camera up to QR code";
            _scanner.BottomText = "Camera will automatically scan QR code\r\n\rPress the 'Back' button to cancel";

            var result = await _scanner.Scan();
            //String uid, name, gender, yob, address, xml;
            xml = result.Text;
            
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(adhar));
                adhar ad = (adhar)serializer.Deserialize(sr);
                uid = ad.uid;
                name = ad.name;
                gender = ad.gender;
                yob = ad.yob;
                address = ad.house + ", " + ad.street + ", " + ad.dist + ", " + ad.state + " - " + ad.pc;
            }                  

            
            Patient_Name.Text = name.ToString();
            date_field.Text = DateTime.Now.Date.ToString();
            ///create an instance on our azure server
            


            StackPanel s_panel = new StackPanel();
            scrollvi.Content = s_panel;
            TextBlock tb1 = new TextBlock();
            tb1.VerticalAlignment = VerticalAlignment.Top;
            tb1.Text = "Symptoms :";
            s_panel.Children.Add(tb1);
            TextBox tb01 = new TextBox();
            tb01.VerticalAlignment = VerticalAlignment.Top;
            ///tb1.Text = "Symptoms :";
            s_panel.Children.Add(tb01);
            Button b1 = new Button();
            b1.Content = "Add";
            b1.VerticalAlignment = VerticalAlignment.Top;
            b1.Click += new RoutedEventHandler((s, f) => method1(s, f, tb01,1));
            s_panel.Children.Add(b1);
            s_panel.Children.Add(stap1);

            TextBlock tb2 = new TextBlock();
            tb2.VerticalAlignment = VerticalAlignment.Top;
            tb2.Text = "Diseases :";
            s_panel.Children.Add(tb2);
            TextBox tb02 = new TextBox();
            tb02.VerticalAlignment = VerticalAlignment.Top;
            ///tb1.Text = "Symptoms :";
            s_panel.Children.Add(tb02);
            Button b2 = new Button();
            b2.Content = "Add";
            b2.VerticalAlignment = VerticalAlignment.Top;
            b2.Click += new RoutedEventHandler((s, f) => method1(s, f, tb02, 2));
            s_panel.Children.Add(b2);
            s_panel.Children.Add(stap2);

            TextBlock tb30 = new TextBlock();
            tb30.VerticalAlignment = VerticalAlignment.Top;
            tb30.Text = "Medicine";
            TextBlock tb31 = new TextBlock();
            tb31.VerticalAlignment = VerticalAlignment.Top;
            tb31.Text = "Frequency";
            TextBlock tb32 = new TextBlock();
            tb32.VerticalAlignment = VerticalAlignment.Top;
            tb32.Text = "Days";

            TextBox tb030 = new TextBox();
            tb030.VerticalAlignment = VerticalAlignment.Top;
            TextBox tb031 = new TextBox();
            tb031.VerticalAlignment = VerticalAlignment.Top;
            TextBox tb032 = new TextBox();
            tb032.VerticalAlignment = VerticalAlignment.Top;

            Grid gd1 = new Grid();
            gd1.Width = 400;
            gd1.HorizontalAlignment = HorizontalAlignment.Left;
            gd1.VerticalAlignment = VerticalAlignment.Top;
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            ColumnDefinition col3 = new ColumnDefinition();
            ColumnDefinition col4 = new ColumnDefinition();
            gd1.ColumnDefinitions.Add(col1);
            gd1.ColumnDefinitions.Add(col2);
            gd1.ColumnDefinitions.Add(col3);
            gd1.ColumnDefinitions.Add(col4);

            RowDefinition row0 = new RowDefinition();
            row0.Height = new GridLength(80);
            gd1.RowDefinitions.Add(row0);
            RowDefinition row1 = new RowDefinition();
            row1.Height = new GridLength(80);
            gd1.RowDefinitions.Add(row1);

            Grid.SetRow(tb30, 0);
            Grid.SetColumn(tb30, 0);
            Grid.SetRow(tb31, 0);
            Grid.SetColumn(tb31, 1);
            Grid.SetRow(tb32, 0);
            Grid.SetColumn(tb32, 2);

            Grid.SetRow(tb030, 1);
            Grid.SetColumn(tb030, 0);
            Grid.SetRow(tb031, 1);
            Grid.SetColumn(tb031, 1);
            Grid.SetRow(tb032, 1);
            Grid.SetColumn(tb032, 2);
            ///TextBox tb03 = new TextBox();
            ///tb01.VerticalAlignment = VerticalAlignment.Top;
            ///tb1.Text = "Symptoms :";
            ///s_panel.Children.Add(tb03);
            Button b3 = new Button();
            b3.Content = "Add";
            b3.VerticalAlignment = VerticalAlignment.Top;
            b3.Click += new RoutedEventHandler((s, f) => method2(s, f, tb030,tb031,tb032,stap3));
            Grid.SetColumn(b3, 3);
            Grid.SetRow(b3, 1);

            gd1.Children.Add(tb30);
            gd1.Children.Add(tb31);
            gd1.Children.Add(tb32);
            gd1.Children.Add(tb030);
            gd1.Children.Add(tb031);
            gd1.Children.Add(tb032);
            gd1.Children.Add(b3);
            ///s_panel.Children.Add(b3);
            s_panel.Children.Add(gd1);
            s_panel.Children.Add(stap3);

            TextBlock tb4 = new TextBlock();
            tb4.VerticalAlignment = VerticalAlignment.Top;
            tb4.Text = "Tests :";
            s_panel.Children.Add(tb4);
            TextBox tb04 = new TextBox();
            tb04.VerticalAlignment = VerticalAlignment.Top;
            ///tb1.Text = "Symptoms :";
            s_panel.Children.Add(tb04);
            Button b4 = new Button();
            b4.Content = "Add";
            b4.VerticalAlignment = VerticalAlignment.Top;
            b4.Click += new RoutedEventHandler((s, f) => method1(s, f, tb04, 4));
            s_panel.Children.Add(b4);
            s_panel.Children.Add(stap4);

            TextBlock tb5 = new TextBlock();
            tb5.VerticalAlignment = VerticalAlignment.Top;
            tb5.Text = "Comments :";
            s_panel.Children.Add(tb5);
            
            tb05.VerticalAlignment = VerticalAlignment.Top;
            ///tb1.Text = "Symptoms :";
            s_panel.Children.Add(tb05);
        }

        private void method1(object sender, RoutedEventArgs f, TextBox tb,int stap)
        {
            TextBlock tbl = new TextBlock();
            tbl.Text = tb.Text;
            tbl.VerticalAlignment = VerticalAlignment.Top;
            if(stap==1)
            {
                symptoms += tb.Text+" , ";                
                stap1.Children.Add(tbl);
                tb.Text = "";
            }
            else if(stap==2)
            {
                diagnosis += tb.Text+" , ";
                stap2.Children.Add(tbl);
                tb.Text = "";
            }
            else if(stap==4)
            {
                test += tb.Text+" , ";
                stap4.Children.Add(tbl);
                tb.Text = "";
            }
        }

        int w = 0;
        List<String> list1 = new List<String>();
        List<String> list2 = new List<String>();
        List<String> list3 = new List<String>();

        private void method2(object sender, RoutedEventArgs f, TextBox tb1, TextBox tb2, TextBox tb3, StackPanel stap)
        {
            w++;
            list1.Add(tb1.Text);
            list2.Add(tb2.Text);
            list3.Add(tb3.Text);

            Grid gd = new Grid();
            gd.Width = 400;
            gd.HorizontalAlignment = HorizontalAlignment.Left;
            gd.VerticalAlignment = VerticalAlignment.Top;

            RowDefinition row1 = new RowDefinition();
            row1.Height=new GridLength(40);
            gd.RowDefinitions.Add(row1);

            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            ColumnDefinition col3 = new ColumnDefinition();
            //ColumnDefinition col4 = new ColumnDefinition();
            gd.ColumnDefinitions.Add(col1);
            gd.ColumnDefinitions.Add(col2);
            gd.ColumnDefinitions.Add(col3);
            //gd.ColumnDefinitions.Add(col4);
            TextBlock tb30 = new TextBlock();
            tb30.VerticalAlignment = VerticalAlignment.Top;
            tb30.Text = tb1.Text;
            TextBlock tb31 = new TextBlock();
            tb31.VerticalAlignment = VerticalAlignment.Top;
            tb31.Text = tb2.Text;
            TextBlock tb32 = new TextBlock();
            tb32.VerticalAlignment = VerticalAlignment.Top;
            tb32.Text = tb3.Text;

            Grid.SetRow(tb30, 0);
            Grid.SetRow(tb31, 0);
            Grid.SetRow(tb32, 0);

            Grid.SetColumn(tb30, 0);
            Grid.SetColumn(tb31, 1);
            Grid.SetColumn(tb32, 2);

            gd.Children.Add(tb30);
            gd.Children.Add(tb31);
            gd.Children.Add(tb32);

            stap3.Children.Add(gd);
            Debug.WriteLine("hello!");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/doctormain.xaml"));

        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("hello!");
            //Debug.WriteLine("hello!");
            comment = comment + tb05.Text;


            string uri2 = "http://prescribecfd.azurewebsites.net/prescribed.php?";            
            uri2 = uri2 + "duid=" + App.duid + "&pass=" + App.password + "&nod=10" + "&puid=" + uid.ToString() + "&pdate=" + date_field.Text + "&symptoms="+symptoms+"&diagnosis="+diagnosis+"&test="+test+"&comments="+comment;

            
            HttpClient client = new HttpClient();
            try
            {
                
                HttpResponseMessage response = await client.GetAsync(uri2.Replace(" ", "%20"));
                
                response.EnsureSuccessStatusCode();
                
                string responseBody = await response.Content.ReadAsStringAsync();
                
                dynamic stuff = JsonConvert.DeserializeObject(responseBody);
                await this.Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show(stuff.ToString());
                });
                App.verma = stuff;
                

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
                        
            String PID = App.verma.pid;
            for(int j=0;j<w;j++)
            {
                string uri3 = "http://prescribecfd.azurewebsites.net/addmed.php?";
                uri3 = uri3 + "duid=" + App.duid + "&pass=" + App.password + "&pid=" + PID + "&medicine=" + list1[j] + "&days=" + list2[j] + "&frequency=" + list3[j];

                await this.Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show(uri3);
                });

                HttpClient clientx = new HttpClient();
                try
                {                    
                    HttpResponseMessage response = await clientx.GetAsync(uri3);
                    
                    response.EnsureSuccessStatusCode();
                    
                    string responseBody = await response.Content.ReadAsStringAsync();
                    
                    await this.Dispatcher.InvokeAsync(() =>
                    {
                        MessageBox.Show(responseBody);
                    });
                }
                catch (HttpRequestException f)
                {
                    App.err = f.Message;                    
                }
                clientx.Dispose();
            }

            NavigationService.Navigate(new Uri("/doctormain.xaml"));
        }

        [XmlRoot("PrintLetterBarcodeData")]
        public class adhar
        {
            [XmlAttribute]
            public string uid { get; set; }
            [XmlAttribute]
            public string name { get; set; }
            [XmlAttribute]
            public string gender { get; set; }
            [XmlAttribute]
            public string yob { get; set; }
            [XmlAttribute]
            public string gname { get; set; }
            [XmlAttribute]
            public string house { get; set; }
            [XmlAttribute]
            public string street { get; set; }
            [XmlAttribute]
            public string dist { get; set; }
            [XmlAttribute]
            public string vtc { get; set; }
            [XmlAttribute]
            public string state { get; set; }
            [XmlAttribute]
            public string pc { get; set; }
        }
    }
}