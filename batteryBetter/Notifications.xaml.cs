using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using Core;

namespace batteryBetter
{
    public partial class Notifications : PhoneApplicationPage
    {
        Settings appSettings = new Settings();

        public Notifications()
        {
            InitializeComponent();

            preSetup();
        }

        private void preSetup()
        {
            BatteryFullSwitch.IsChecked = appSettings.getSettingsBool(appSettings.BatteryFullNotificationKeyName);
            BatteryLowSwitch.IsChecked = appSettings.getSettingsBool(appSettings.BatteryLowNotificationKeyName);
            slider1.Value = appSettings.getSettingsInt(appSettings.NotificationDurationKeyName);
            slider2.Value = appSettings.getSettingsInt(appSettings.LowBatteryThreshold);
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                int sliderValue = (int)slider1.Value;
                pushNotifications.Text = "push notifications every, " + sliderValue.ToString() + " minutes";
                appSettings.saveSettingsInt(appSettings.NotificationDurationKeyName, sliderValue);
            }
            catch
            {
                Debug.WriteLine("slider in notification setting not working...");
            }            
            
        }

        private void BatteryLowSwitch_Checked(object sender, RoutedEventArgs e)
        {
            appSettings.saveSettingsBool(appSettings.BatteryLowNotificationKeyName, true);
        }

        private void BatteryLowSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            appSettings.saveSettingsBool(appSettings.BatteryLowNotificationKeyName, false);
        }

        private void BatteryFullSwitch_Checked(object sender, RoutedEventArgs e)
        {
            appSettings.saveSettingsBool(appSettings.BatteryFullNotificationKeyName, true);
        }

        private void BatteryFullSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            appSettings.saveSettingsBool(appSettings.BatteryFullNotificationKeyName, false);
        }

        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                int sliderValue = (int)slider2.Value;
                lowBattery.Text = "low battery threshold, " + sliderValue.ToString() + "%";
                appSettings.saveSettingsInt(appSettings.LowBatteryThreshold, sliderValue);
            }
            catch
            {
                Debug.WriteLine("slider in notification setting not working...");
            } 
        }
    }
}