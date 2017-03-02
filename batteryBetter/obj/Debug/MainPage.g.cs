﻿#pragma checksum "C:\Users\Raajit\documents\visual studio 2013\Projects\batteryBetter\batteryBetter\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "27053654D442D5ADDBD336F597E703C1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
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
using batteryBetter;


namespace batteryBetter {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard showDetailed;
        
        internal System.Windows.Media.Animation.Storyboard hideDetailed;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock PowerOffTitle;
        
        internal System.Windows.Controls.TextBlock DischarginRateTitle;
        
        internal System.Windows.Controls.TextBlock PowerOffAtTitle;
        
        internal System.Windows.Controls.TextBlock DischargeRateInput;
        
        internal System.Windows.Controls.TextBlock powerOffInInput;
        
        internal System.Windows.Controls.TextBlock powerOffAtInput;
        
        internal System.Windows.Controls.TextBlock LastChargeTitle;
        
        internal System.Windows.Controls.TextBlock timeSinceLastChargeInput;
        
        internal System.Windows.Controls.ListBox detailedList;
        
        internal Microsoft.Phone.Controls.ListPicker DetailedSort;
        
        internal Microsoft.Phone.Controls.ListPickerItem detailedMinutely;
        
        internal Microsoft.Phone.Controls.ListPickerItem detailedHourly;
        
        internal batteryBetter.BatteryChart areachart;
        
        internal Microsoft.Phone.Controls.ListPicker GraphSort;
        
        internal Microsoft.Phone.Controls.ListPickerItem graphHourly;
        
        internal Microsoft.Phone.Controls.ListPickerItem graphdaily;
        
        internal Microsoft.Phone.Controls.ListPickerItem graphweekly;
        
        internal Microsoft.Phone.Controls.ListPickerItem graphlastcharge;
        
        internal Microsoft.Phone.Controls.ListPickerItem graphalltime;
        
        internal System.Windows.Controls.TextBlock sortText;
        
        internal System.Windows.Controls.ListBox usageList;
        
        internal Microsoft.Phone.Controls.ListPicker UsageSort;
        
        internal Microsoft.Phone.Controls.ListPickerItem usageCharge;
        
        internal Microsoft.Phone.Controls.ListPickerItem usageHundred;
        
        internal Microsoft.Phone.Controls.ListPickerItem usageAlltime;
        
        internal System.Windows.Controls.ScrollViewer ContentGrid;
        
        internal System.Windows.Controls.TextBlock rateMe;
        
        internal System.Windows.Controls.TextBlock goProButton;
        
        internal System.Windows.Controls.TextBlock notifSetting;
        
        internal System.Windows.Controls.TextBlock liveTileSetting;
        
        internal System.Windows.Controls.TextBlock enablelockScreen;
        
        internal System.Windows.Controls.TextBlock resetData;
        
        internal System.Windows.Controls.TextBlock notUpdating;
        
        internal System.Windows.Controls.Grid grid;
        
        internal System.Windows.Controls.TextBlock detailedTime;
        
        internal System.Windows.Controls.TextBlock detailedbattery;
        
        internal System.Windows.Controls.TextBlock detailedChange;
        
        internal System.Windows.Controls.StackPanel onBattery;
        
        internal System.Windows.Controls.StackPanel onPluggedIn;
        
        internal System.Windows.Controls.StackPanel onWifiOn;
        
        internal System.Windows.Controls.StackPanel onOffline;
        
        internal System.Windows.Controls.StackPanel onDataLimit;
        
        internal System.Windows.Controls.StackPanel onInternet;
        
        internal System.Windows.Controls.ProgressBar ChargingStatusBar;
        
        internal System.Windows.Controls.TextBlock ChargeStatus;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/batteryBetter;component/MainPage.xaml", System.UriKind.Relative));
            this.showDetailed = ((System.Windows.Media.Animation.Storyboard)(this.FindName("showDetailed")));
            this.hideDetailed = ((System.Windows.Media.Animation.Storyboard)(this.FindName("hideDetailed")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.PowerOffTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PowerOffTitle")));
            this.DischarginRateTitle = ((System.Windows.Controls.TextBlock)(this.FindName("DischarginRateTitle")));
            this.PowerOffAtTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PowerOffAtTitle")));
            this.DischargeRateInput = ((System.Windows.Controls.TextBlock)(this.FindName("DischargeRateInput")));
            this.powerOffInInput = ((System.Windows.Controls.TextBlock)(this.FindName("powerOffInInput")));
            this.powerOffAtInput = ((System.Windows.Controls.TextBlock)(this.FindName("powerOffAtInput")));
            this.LastChargeTitle = ((System.Windows.Controls.TextBlock)(this.FindName("LastChargeTitle")));
            this.timeSinceLastChargeInput = ((System.Windows.Controls.TextBlock)(this.FindName("timeSinceLastChargeInput")));
            this.detailedList = ((System.Windows.Controls.ListBox)(this.FindName("detailedList")));
            this.DetailedSort = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("DetailedSort")));
            this.detailedMinutely = ((Microsoft.Phone.Controls.ListPickerItem)(this.FindName("detailedMinutely")));
            this.detailedHourly = ((Microsoft.Phone.Controls.ListPickerItem)(this.FindName("detailedHourly")));
            this.areachart = ((batteryBetter.BatteryChart)(this.FindName("areachart")));
            this.GraphSort = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("GraphSort")));
            this.graphHourly = ((Microsoft.Phone.Controls.ListPickerItem)(this.FindName("graphHourly")));
            this.graphdaily = ((Microsoft.Phone.Controls.ListPickerItem)(this.FindName("graphdaily")));
            this.graphweekly = ((Microsoft.Phone.Controls.ListPickerItem)(this.FindName("graphweekly")));
            this.graphlastcharge = ((Microsoft.Phone.Controls.ListPickerItem)(this.FindName("graphlastcharge")));
            this.graphalltime = ((Microsoft.Phone.Controls.ListPickerItem)(this.FindName("graphalltime")));
            this.sortText = ((System.Windows.Controls.TextBlock)(this.FindName("sortText")));
            this.usageList = ((System.Windows.Controls.ListBox)(this.FindName("usageList")));
            this.UsageSort = ((Microsoft.Phone.Controls.ListPicker)(this.FindName("UsageSort")));
            this.usageCharge = ((Microsoft.Phone.Controls.ListPickerItem)(this.FindName("usageCharge")));
            this.usageHundred = ((Microsoft.Phone.Controls.ListPickerItem)(this.FindName("usageHundred")));
            this.usageAlltime = ((Microsoft.Phone.Controls.ListPickerItem)(this.FindName("usageAlltime")));
            this.ContentGrid = ((System.Windows.Controls.ScrollViewer)(this.FindName("ContentGrid")));
            this.rateMe = ((System.Windows.Controls.TextBlock)(this.FindName("rateMe")));
            this.goProButton = ((System.Windows.Controls.TextBlock)(this.FindName("goProButton")));
            this.notifSetting = ((System.Windows.Controls.TextBlock)(this.FindName("notifSetting")));
            this.liveTileSetting = ((System.Windows.Controls.TextBlock)(this.FindName("liveTileSetting")));
            this.enablelockScreen = ((System.Windows.Controls.TextBlock)(this.FindName("enablelockScreen")));
            this.resetData = ((System.Windows.Controls.TextBlock)(this.FindName("resetData")));
            this.notUpdating = ((System.Windows.Controls.TextBlock)(this.FindName("notUpdating")));
            this.grid = ((System.Windows.Controls.Grid)(this.FindName("grid")));
            this.detailedTime = ((System.Windows.Controls.TextBlock)(this.FindName("detailedTime")));
            this.detailedbattery = ((System.Windows.Controls.TextBlock)(this.FindName("detailedbattery")));
            this.detailedChange = ((System.Windows.Controls.TextBlock)(this.FindName("detailedChange")));
            this.onBattery = ((System.Windows.Controls.StackPanel)(this.FindName("onBattery")));
            this.onPluggedIn = ((System.Windows.Controls.StackPanel)(this.FindName("onPluggedIn")));
            this.onWifiOn = ((System.Windows.Controls.StackPanel)(this.FindName("onWifiOn")));
            this.onOffline = ((System.Windows.Controls.StackPanel)(this.FindName("onOffline")));
            this.onDataLimit = ((System.Windows.Controls.StackPanel)(this.FindName("onDataLimit")));
            this.onInternet = ((System.Windows.Controls.StackPanel)(this.FindName("onInternet")));
            this.ChargingStatusBar = ((System.Windows.Controls.ProgressBar)(this.FindName("ChargingStatusBar")));
            this.ChargeStatus = ((System.Windows.Controls.TextBlock)(this.FindName("ChargeStatus")));
        }
    }
}

