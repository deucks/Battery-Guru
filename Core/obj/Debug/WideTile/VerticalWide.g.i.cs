﻿#pragma checksum "C:\Users\Raajit\documents\visual studio 2013\Projects\batteryBetter\Core\WideTile\VerticalWide.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8B32D637A629CEBF3982A5387730C370"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Core.WideTile {
    
    
    public partial class VerticalWide : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid backgroundStatus;
        
        internal System.Windows.Controls.TextBlock ChargeStatus;
        
        internal System.Windows.Controls.TextBlock ChargeStatusDescription;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/Core;component/WideTile/VerticalWide.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.backgroundStatus = ((System.Windows.Controls.Grid)(this.FindName("backgroundStatus")));
            this.ChargeStatus = ((System.Windows.Controls.TextBlock)(this.FindName("ChargeStatus")));
            this.ChargeStatusDescription = ((System.Windows.Controls.TextBlock)(this.FindName("ChargeStatusDescription")));
        }
    }
}

