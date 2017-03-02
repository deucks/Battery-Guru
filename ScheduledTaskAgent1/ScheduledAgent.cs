using System.Diagnostics;
using System.Windows;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using Core;
using Windows.UI.StartScreen;
using Microsoft.Phone.Net.NetworkInformation;
using Windows.Devices.Geolocation;
using Microsoft.Phone.Info;

namespace ScheduledTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(61));

            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                Core.TileOptions to = new Core.TileOptions();
                to.UpdateTile();
            }); 

            giveToast();


            string toastMessage = "Periodic task running.";

            ShellToast toast = new ShellToast();
            toast.Title = "Hey";
            toast.Content = toastMessage;
            //toast.Show();

            addBatteryStatus();

                   
                       
            

            

            NotifyComplete();
        }

        public void giveToast()
        {
            Settings appSettings = new Settings();
            int currentDelay = appSettings.getSettingsInt(appSettings.NotificationDurationKeyName);
            int currentCount = appSettings.getSettingsInt(appSettings.NotificationCount);

            bool batteryLow = appSettings.getSettingsBool(appSettings.BatteryLowNotificationKeyName);
            bool batteryFull = appSettings.getSettingsBool(appSettings.BatteryFullNotificationKeyName);

            Debug.WriteLine("current delay" + currentDelay);
            Debug.WriteLine("current count" + currentCount);

            Debug.WriteLine("batteryLow " + batteryLow.ToString());
            Debug.WriteLine("batteryFull " + batteryFull.ToString());


            if (currentCount > currentDelay)
            {
                if (batteryFull)
                {
                    giveBatteryFullToast();
                }

                if (batteryLow)
                {
                    giveBatteryLowToast();
                }

                appSettings.saveSettingsInt(appSettings.NotificationCount, 0);
            }
            else
            {
                appSettings.saveSettingsInt(appSettings.NotificationCount, (currentCount + 1));
            }
        }

        public void giveBatteryLowToast()
        {
            Settings appSettings = new Settings();
            int lowThreshold = appSettings.getSettingsInt(appSettings.LowBatteryThreshold);

            if (SystemEndPoints.currentBatteryLevel() < lowThreshold)
            {
                ShellToast toast = new ShellToast();
                toast.Title = "BatteryGuru";
                toast.Content = "Low battery, < " + lowThreshold + "% left";
                toast.Show();
            }
        }

        public void giveBatteryFullToast()
        {
            if (SystemEndPoints.currentBatteryLevel() > 99 && SystemEndPoints.isCharging())
            {
                ShellToast toast = new ShellToast();
                toast.Title = "BatteryGuru";
                toast.Content = "You can unplug me.";
                toast.Show();
            }
        }

        public void addBatteryStatus()
        {
            ViewModel batteryModel = new ViewModel();

            batteryModel.add(SystemEndPoints.currentBatteryLevel(), SystemEndPoints.isWifiOn(), SystemEndPoints.isPhoneOn(), SystemEndPoints.isLocationEnabled(), SystemEndPoints.isCharging(), SystemEndPoints.isAirplaneOn(), SystemEndPoints.isInternetOn(), SystemEndPoints.isOverDataLimit(), SystemEndPoints.isRoaming());

            Debug.WriteLine("Background");

        }

        
    }

}