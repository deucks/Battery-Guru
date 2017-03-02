using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.IsolatedStorage;
using System.Windows;

namespace Core
{
    public class Settings
    {
        private readonly object _sync = new object();

        public string BatteryLowNotificationKeyName = "batteryLow";
        public string BatteryFullNotificationKeyName = "batteryFull";
        public string NotificationDurationKeyName = "notificationDuration";
        public string NotificationCount = "notificationCount";
        public string ProEnabled = "proEnabled";
        public string TileType = "tileName";
        public string LowBatteryThreshold= "lowThreshold";
        public string AppRated = "appRated";
        


        public Settings()
        {
            checkIfExists();
        }

        private void checkIfExists()
        {
            if (!IsolatedStorageSettings.ApplicationSettings.Contains(BatteryLowNotificationKeyName))
            {
                IsolatedStorageSettings.ApplicationSettings[BatteryLowNotificationKeyName] = true;
                SaveSettings();
            }

            if (!IsolatedStorageSettings.ApplicationSettings.Contains(NotificationDurationKeyName))
            {
                IsolatedStorageSettings.ApplicationSettings[NotificationDurationKeyName] = 5;
                SaveSettings();
            }

            if (!IsolatedStorageSettings.ApplicationSettings.Contains(BatteryFullNotificationKeyName))
            {
                IsolatedStorageSettings.ApplicationSettings[BatteryFullNotificationKeyName] = true;
                SaveSettings();
            }

            if (!IsolatedStorageSettings.ApplicationSettings.Contains(NotificationCount))
            {
                IsolatedStorageSettings.ApplicationSettings[NotificationCount] = 0;
                SaveSettings();
            }

            if (!IsolatedStorageSettings.ApplicationSettings.Contains(ProEnabled))
            {
                IsolatedStorageSettings.ApplicationSettings[ProEnabled] = false;
                SaveSettings();
            }

            if (!IsolatedStorageSettings.ApplicationSettings.Contains(TileType))
            {
                IsolatedStorageSettings.ApplicationSettings[TileType] = "Defualt";
                SaveSettings();
            }

            if (!IsolatedStorageSettings.ApplicationSettings.Contains(LowBatteryThreshold))
            {
                IsolatedStorageSettings.ApplicationSettings[LowBatteryThreshold] = 10;
                SaveSettings();
            }

            if (!IsolatedStorageSettings.ApplicationSettings.Contains(AppRated))
            {
                IsolatedStorageSettings.ApplicationSettings[AppRated] = false;
                SaveSettings();
            }
        }

        public bool getSettingsBool(string keyName)
        {
            return (bool) IsolatedStorageSettings.ApplicationSettings[keyName];
        }

        public int getSettingsInt(string keyName)
        {
            return (int)IsolatedStorageSettings.ApplicationSettings[keyName];
        }

        public string getSettingsString(string keyName)
        {
            return IsolatedStorageSettings.ApplicationSettings[keyName].ToString();
        }

        public void saveSettingsBool(string keyName, bool keyValue)
        {
            IsolatedStorageSettings.ApplicationSettings[keyName] = keyValue;
            SaveSettings();
        }

        public void saveSettingsInt(string keyName, int keyValue)
        {
            IsolatedStorageSettings.ApplicationSettings[keyName] = keyValue;
            SaveSettings();
        }

        public void saveSettingsString(string keyName, string keyValue)
        {
            IsolatedStorageSettings.ApplicationSettings[keyName] = keyValue;
            SaveSettings();
        }


        private void SaveSettings()
        {
            lock (_sync)
            {
                IsolatedStorageSettings.ApplicationSettings.Save();
            }
        }

    }
    
    

}
