using Microsoft.Phone.Controls;
using System.Windows;
using ZXing.Mobile;
using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;

namespace QRCodeReader
{
    public partial class Scanner : PhoneApplicationPage
    {
        private MobileBarcodeScanner _scanner;

        public Scanner()
        {
            InitializeComponent();
            this.Loaded += Scanner_Loaded;
        }

        private async void Scanner_Loaded(object sender, RoutedEventArgs e)
        {
            _scanner = new MobileBarcodeScanner(this.Dispatcher);
            _scanner.UseCustomOverlay = false;
            _scanner.TopText = "Hold camera up to QR code";
            _scanner.BottomText = "Camera will automatically scan QR code\r\n\rPress the 'Back' button to cancel";

            var result = await _scanner.Scan();
            ProcessScanResult(result);
        }

        private async void ProcessScanResult(ZXing.Result result)
        ///private async void ProcessScanResult(object sender, RoutedEventArgs e)
        {
            String uid, name, gender, yob, address,xml;
            String message = string.Empty;
            ///message = result.Text;
            xml = result.Text;
            ///xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><PrintLetterBarcodeData uid=\"414361857594\" name=\"Akash Verma\" gender=\"M\" yob=\"1996\" house=\"2/H/2, 1ST FLOOR\" street=\"TALTALA LANE\" dist=\"Kolkata\" state=\"WEST BENGAL\" PC=\"700014\" />";
            message = xml;
            using (StringReader sr = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(adhar));
                adhar ad = (adhar)serializer.Deserialize(sr);
                uid = ad.uid;
                name = ad.name;
                gender = ad.gender;
                yob = ad.yob;
                address = ad.house+", "+ad.street+", "+ad.dist+", "+ad.state+" - "+ad.pc;
            }
            
            string uri = "http://prescribecfd.azurewebsites.net/regd.php?";
            uri = uri + "duid=" + uid.ToString();
            
            if (App.pass == 1)
            {
                App.duid = uid.ToString();
                App.dname = name.ToString();
                App.dgender = gender.ToString();
                App.dyob = yob.ToString();
                App.daddress = address.ToString();
                HttpClient client = new HttpClient();
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic stuff = JsonConvert.DeserializeObject(responseBody);
                    App.login_index = stuff.id;
                }
                catch (HttpRequestException f)
                {
                    App.err = f.Message;
                }
                client.Dispose();
                
                App.doctor_scan = message;
                ///if usr doesn't exist in data base
                ///send only uid
                ///else login_index = 2;
                NavigationService.Navigate(new Uri("/login.xaml", UriKind.Relative));
            }
            else if(App.pass == 2)
            {
                App.patient_scan = message;
                string uri2 = "http://prescribecfd.azurewebsites.net/regp.php?";
                uri2 = uri2 + "puid=" + uid.ToString();
                HttpClient client = new HttpClient();
                try
                {
                    HttpResponseMessage response = await client.GetAsync(uri2);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    dynamic stuff = JsonConvert.DeserializeObject(responseBody);
                    App.login_index = stuff.id;
                }
                catch (HttpRequestException f)
                {
                    App.err = f.Message;
                }
                client.Dispose();
                ///3 if user doesn't exist in data base
                ///else login_index =4 go direct to files
                App.puid = uid.ToString();
                App.pname = name.ToString();
                App.pgender = gender.ToString();
                App.pyob = yob.ToString();
                App.paddress = address.ToString();

                if (App.login_index == "3")
                {
                    NavigationService.Navigate(new Uri("/login.xaml", UriKind.Relative));
                }
                else if(App.login_index == "4")
                {
                    NavigationService.Navigate(new Uri("/pub_user.xaml", UriKind.Relative));
                }
            }
            else if(App.pass ==3)
            {
                App.patient_scan = message;
                App.puid = uid.ToString();                
                NavigationService.Navigate(new Uri("/patientscroller.xaml", UriKind.Relative));
            }
            else if(App.pass == 4)
            {
                App.patient_scan = message;
                NavigationService.Navigate(new Uri("/prescription.xaml", UriKind.Relative));
            }            
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