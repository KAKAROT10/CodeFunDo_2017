﻿#pragma checksum "C:\Users\ROHIT\Documents\Visual Studio 2015\Projects\QRCodeReader\QRCodeReader\login.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E3FE60BEC8EDB90938F4C90874918271"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace QRCodeReader {
    
    
    public partial class login : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.PasswordBox password;
        
        internal System.Windows.Controls.PasswordBox conpassword;
        
        internal System.Windows.Controls.HyperlinkButton button;
        
        internal System.Windows.Controls.TextBox sleep;
        
        internal System.Windows.Controls.TextBox wake;
        
        internal System.Windows.Controls.TextBlock header_wt;
        
        internal System.Windows.Controls.TextBlock header_sp;
        
        internal System.Windows.Controls.TextBlock header_cp;
        
        internal System.Windows.Controls.TextBlock header_p;
        
        internal System.Windows.Controls.TextBox mobile;
        
        internal System.Windows.Controls.TextBlock header_m;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/QRCodeReader;component/login.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.password = ((System.Windows.Controls.PasswordBox)(this.FindName("password")));
            this.conpassword = ((System.Windows.Controls.PasswordBox)(this.FindName("conpassword")));
            this.button = ((System.Windows.Controls.HyperlinkButton)(this.FindName("button")));
            this.sleep = ((System.Windows.Controls.TextBox)(this.FindName("sleep")));
            this.wake = ((System.Windows.Controls.TextBox)(this.FindName("wake")));
            this.header_wt = ((System.Windows.Controls.TextBlock)(this.FindName("header_wt")));
            this.header_sp = ((System.Windows.Controls.TextBlock)(this.FindName("header_sp")));
            this.header_cp = ((System.Windows.Controls.TextBlock)(this.FindName("header_cp")));
            this.header_p = ((System.Windows.Controls.TextBlock)(this.FindName("header_p")));
            this.mobile = ((System.Windows.Controls.TextBox)(this.FindName("mobile")));
            this.header_m = ((System.Windows.Controls.TextBlock)(this.FindName("header_m")));
        }
    }
}

