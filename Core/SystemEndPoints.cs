using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Devices.Geolocation;
using Windows.Networking.Connectivity;
using Windows.Phone.Devices.Power;

namespace Core
{
    public class SystemEndPoints
    {
        private static Battery _battery;

        private static ViewModel vm = new ViewModel();

        public static int currentBatteryLevel()
        {
            return Windows.Phone.Devices.Power.Battery.GetDefault().RemainingChargePercent;
        }

        public static bool isCharging()
        {
            return DeviceStatus.PowerSource == PowerSource.External;
        }

        public static bool isWifiOn()
        {
            try
            {
                if (DeviceNetworkInformation.IsWiFiEnabled)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                Debug.WriteLine("Cant wifi on information");
            }

            return false;
            
        }


        public static bool isOverDataLimit()
        {
            try
            {
                ConnectionProfile internetConnectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();

                if (internetConnectionProfile.NetworkAdapter.IanaInterfaceType != 71)// Connection is not a Wi-Fi connection. 
                {
                    return internetConnectionProfile.GetConnectionCost().OverDataLimit;
                }
            }
            catch
            {
                Debug.WriteLine("Cant recieve data on information");
            }
            
            return false;
        }

        public static bool isRoaming()
        {
            try
            {
                ConnectionProfile internetConnectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();

                if (internetConnectionProfile.NetworkAdapter.IanaInterfaceType != 71)// Connection is not a Wi-Fi connection. 
                {
                    return internetConnectionProfile.GetConnectionCost().Roaming;
                }
            }
            catch
            {
                Debug.WriteLine("Cant recieve roaming on information");
            }

            return false;
        }

        public static bool isAirplaneOn()
        {
            try
            {
                ConnectionProfile internetConnectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
                if (internetConnectionProfile == null)
                {
                    return true;
                }
            }
            catch
            {
                Debug.WriteLine("Cant recieve airplane mode on information");
            }
            
            return false;
        }

        public static bool isInternetOn()
        {
            try
            {
                ConnectionProfile internetConnectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
                //if true, internet is accessible.
                return (internetConnectionProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
            }
            catch
            {
                Debug.WriteLine("Cant recieve internet on information");
            }
            return false;
        }

        public static bool isPhoneOn()
        {
            try
            {
                return DeviceNetworkInformation.IsCellularDataEnabled;
            }
            catch
            {
                Debug.WriteLine("Cant recieve cellular information");
            }
            return false;
        }

        public static DateTime currentDate()
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second); 
        }

        public static DateTime emptyDate()
        {
            return new DateTime(2014, 1, 1, 1, 0, 0);
        }

        public static bool isLocationEnabled()
        {
            Geolocator locator = new Geolocator();
            if (locator.LocationStatus == PositionStatus.Disabled)
            {
                return false;
            }
            return true;
        }

        public static void updateLockTile(int quantity)
        {
            ShellTile tile = ShellTile.ActiveTiles.First();
            if (null != tile)
            {
                // create a new data for tile
                StandardTileData data = new StandardTileData();
                // tile foreground data
                data.Count = quantity;
                // to make tile flip add data to background also
                // update tile
                tile.Update(data);
            }
        }

        #region usage calculations

        public static int countLastChargeFactors(string type)
        {
            vm.getModel().Reverse();
            int count = 0;
            foreach (Core.Model i in vm.getModel())
            {                
                if (type == "all")
                {
                    count++;
                }
                else if (type == "wifi")
                {
                    if (i.wifiStatus)
                    {
                        count++;
                    }
                }
                else if (type == "phone")
                {
                    if (i.phoneStatus)
                    {
                        count++;
                    }
                }
                else if (type == "airplane")
                {
                    if (i.isAirplaneOn)
                    {
                        count++;
                    }
                }
                else if (type == "internet")
                {
                    if (i.isInternetOn)
                    {
                        count++;
                    }
                }
                else if (type == "roaming")
                {
                    if (i.isRoaming)
                    {
                        count++;
                    }
                }
                else if (type == "overData")
                {
                    if (i.isOverDataLimit)
                    {
                        count++;
                    }
                }

                if (i.isCharging)
                {
                    vm.getModel().Reverse();
                    return count;
                }
                
            }

            vm.getModel().Reverse();

            return count;
        }

        public static int countHundredChargeFactors(string type)
        {
            vm.getModel().Reverse();
            int count = 0;
            foreach (Core.Model i in vm.getModel())
            {
                if (type == "all")
                {
                    count++;
                }
                else if (type == "wifi")
                {
                    if (i.wifiStatus)
                    {
                        count++;
                    }
                }
                else if (type == "phone")
                {
                    if (i.phoneStatus)
                    {
                        count++;
                    }
                }
                else if (type == "airplane")
                {
                    if (i.isAirplaneOn)
                    {
                        count++;
                    }
                }
                else if (type == "internet")
                {
                    if (i.isInternetOn)
                    {
                        count++;
                    }
                }
                else if (type == "roaming")
                {
                    if (i.isRoaming)
                    {
                        count++;
                    }
                }
                else if (type == "overData")
                {
                    if (i.isOverDataLimit)
                    {
                        count++;
                    }
                }

                if (i.quantity == 100)
                {
                    vm.getModel().Reverse();
                    return count;
                }

            }

            vm.getModel().Reverse();

            return count;
        }

        public static int countAllTimeFactors(string type)
        {
            vm.getModel().Reverse();
            int count = 0;
            foreach (Core.Model i in vm.getModel())
            {
                if (type == "all")
                {
                    count++;
                }
                else if (type == "wifi")
                {
                    if (i.wifiStatus)
                    {
                        count++;
                    }
                }
                else if (type == "phone")
                {
                    if (i.phoneStatus)
                    {
                        count++;
                    }
                }
                else if (type == "airplane")
                {
                    if (i.isAirplaneOn)
                    {
                        count++;
                    }
                }
                else if (type == "internet")
                {
                    if (i.isInternetOn)
                    {
                        count++;
                    }
                }
                else if (type == "roaming")
                {
                    if (i.isRoaming)
                    {
                        count++;
                    }
                }
                else if (type == "overData")
                {
                    if (i.isOverDataLimit)
                    {
                        count++;
                    }
                }

            }

            vm.getModel().Reverse();

            return count;
        }

        #endregion

        public static int minutesTillLastUpdate()
        {
            DateTime currentTime = currentDate();

            DateTime lastUpdate = lastUpdateTime();

            int timeDifference = (int)currentTime.Subtract(lastUpdate).TotalMinutes;

            return timeDifference;

        }

        public static DateTime lastUpdateTime()
        {
            vm.loadData();
            vm.getModel().Reverse();
            foreach (Core.Model i in vm.getModel())
            {
                vm.getModel().Reverse();
                return i.date;
            }

            vm.getModel().Reverse();
            return currentDate();            
        }

        public static string calculateChargeRateString(string type)
        {
            int firstCharge = countMinMaxPercentage();
            int currentCharge = currentBatteryLevel();

            DateTime firstChargeDate = countMinMaxDateTime();
            DateTime currentChargeDate = currentDate();

            double timeDifference = currentChargeDate.Subtract(firstChargeDate).TotalMinutes;
            double chargeDifference = currentCharge - firstCharge;

            double chargeRate = ((chargeDifference / timeDifference) * 60);
            string chargeRateString = ((chargeDifference / timeDifference) * 60).ToString("0.00");

            if (currentBatteryLevel() > 99)
            {
                return "fully charged...";
            }

            try
            {
                //GETTING CHARGE RATR   
                if (type == "rate")
                {
                    if (timeDifference < 5 || countInputs() < 6)
                        return "calculating...";
                    else
                        return chargeRateString + " percent/hour";
                }
                else if (type == "in")
                {   //GETTING THE CHARGED IN TIME
                    if (timeDifference < 5 || countInputs() < 6)
                    {
                        return "calculating...";
                    }
                    else
                    {
                        double durationLeftHours = (100 - currentCharge) / chargeRate;
                        DateTime inTime = emptyDate();
                        inTime = inTime.AddHours(durationLeftHours);
                        inTime = inTime.AddHours(-1);

                        if (durationLeftHours < 1)
                        {
                            return inTime.ToString("mm") + " minutes";
                        }
                        else
                        {
                            return inTime.ToString("h ") + "hours " + inTime.ToString("mm") + " minutes";
                        }


                    }
                }
                else if (type == "at")
                {   //getting the charged at time
                    if (timeDifference < 5 || countInputs() < 6)
                    {
                        return "calculating...";
                    }
                    else
                    {
                        double durationLeftHours = (100 - currentCharge) / chargeRate;
                        DateTime inTime = currentDate();
                        inTime = inTime.AddHours(durationLeftHours);

                        if (DateTime.Now.DayOfWeek.ToString() == inTime.DayOfWeek.ToString())
                        {
                            return "around " + inTime.ToString("h:mm tt") + " today";
                        }
                        else if ((DateTime.Now.Day + 1) == inTime.Day)
                        {
                            return "around " + inTime.ToString("h:mm tt") + " tomorrow";
                        }
                        else
                        {
                            return "around " + inTime.ToString("h:mm tt") + " on " + inTime.DayOfWeek.ToString();
                        }

                        
                    }
                }
            }
            catch
            {
                Debug.WriteLine("Error when getting charging information");          
            }
            

            return "calculating...";

            
        }

        private static int countMinMaxPercentage()
        {
            //calculating charge rate method - gets the first time that phone started charging
            vm.getModel().Reverse();
            int count = 0;
            foreach (Core.Model i in vm.getModel())
            {
                if (!i.isCharging)
                {
                    vm.getModel().Reverse();
                    return i.quantity;
                }
                count++;
            }

            vm.getModel().Reverse();

            return 0;
        }

        private static DateTime countMinMaxDateTime()
        {
            vm.getModel().Reverse();
            int count = 0;
            foreach (Core.Model i in vm.getModel())
            {
                if (!i.isCharging)
                {
                    vm.getModel().Reverse();
                    return i.date;
                }
                count++;
            }

            vm.getModel().Reverse();

            return new DateTime();
        }

        private static int countInputs()
        {
            int count = 0;
            foreach (Core.Model i in vm.getModel())
            {
                count++;
            }

            return count;
        }

        public static string timeSinceLastHundred()
        {
            DateTime lastCharge = countLastHundredDateTime();
            DateTime currentCharge = currentDate();

            double difference = currentCharge.Subtract(lastCharge).TotalMinutes;

            if (difference > 59)
            {
                return ((int)(difference / 60)).ToString() + " hours ago";
            }
            else
            {
                return ((int)difference).ToString() + " minutes ago";
            }


        }

        private static DateTime countLastHundredDateTime()
        {
            vm.getModel().Reverse();
            DateTime lastTime = new DateTime();
            foreach (Core.Model i in vm.getModel())
            {
                if (i.quantity == 100)
                {
                    vm.getModel().Reverse();
                    return i.date;
                }
                lastTime = i.date;
            }

            vm.getModel().Reverse();

            return lastTime;
        }

        public static string timeSinceLastCharge()
        {
            DateTime lastCharge = countLastChargeDateTime();
            DateTime currentCharge = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            double difference = currentCharge.Subtract(lastCharge).TotalMinutes;

            if (difference > 59)
            {
                return ((int)(difference / 60)).ToString() + " hours ago";
            }
            else
            {
                return ((int)difference).ToString() + " minutes ago";
            }

            
        }

        private static DateTime countLastChargeDateTime()
        {
            vm.getModel().Reverse();
            foreach (Core.Model i in vm.getModel())
            {
                if (i.isCharging)
                {
                    vm.getModel().Reverse();
                    return i.date;
                }
            }

            vm.getModel().Reverse();

            return currentDate();
        }


        public static string getTimeRatio()
        {
            //Get Dischargin Rate
            _battery = Battery.GetDefault();
            double batteryRemaining = _battery.RemainingDischargeTime.TotalHours;
            string returnMessage = (currentBatteryLevel() / batteryRemaining).ToString("0.00") + "% percent/hour";
            return returnMessage;
        }

        public static string getTimeTillPowerOff()
        {
            //get the time/hours/minutes till power off in the phone

            _battery = Battery.GetDefault();

            DateTime currentTime = currentDate();

            DateTime additionTime = currentDate();
            additionTime = additionTime.AddMinutes(_battery.RemainingDischargeTime.TotalMinutes);

            double timeDifference = additionTime.Subtract(currentTime).TotalMinutes;

            DateTime timeTillPowerOff = emptyDate();
            timeTillPowerOff = timeTillPowerOff.AddMinutes(timeDifference);
            timeTillPowerOff = timeTillPowerOff.AddHours(-1);

            if (timeDifference < 1)
            {
                return timeTillPowerOff.ToString("mm") + " minutes";
            }
            else
            {
                return timeTillPowerOff.ToString("h ") + "hours " + timeTillPowerOff.ToString("mm") + " minutes";
            }

                    

            


        }

        public static string getPowerOffTime() 
        {
            _battery = Battery.GetDefault();
            DateTime tomorrow;
            string powerOffTime;
            try
            {
                tomorrow = DateTime.Now.AddMinutes(_battery.RemainingDischargeTime.TotalMinutes);
            }
            catch
            {
                tomorrow = DateTime.Now.AddHours(1);
            }

            if (DateTime.Now.DayOfWeek.ToString() == tomorrow.DayOfWeek.ToString())
            {
                return powerOffTime = "around " + tomorrow.ToString("h:mm tt") + " today";
            }
            else if ((DateTime.Now.Day + 1) == tomorrow.Day)
            {
                return powerOffTime = "around " + tomorrow.ToString("h:mm tt") + " tomorrow";
            }
            else
            {
                return powerOffTime = "around " + tomorrow.ToString("h:mm tt") + " on " + tomorrow.DayOfWeek.ToString();
            }

        }

        public static string fixTimeLeft(string time)
        {
            //parses the time left variable taken from the os
            int hours;
            try
            {
                int currentPostition = 0;
                for (int i = 0; i < time.Length; i++)
                {
                    if (time[i] == '.')
                    {
                        try
                        {
                            hours = Int32.Parse(time.Substring(0, currentPostition));
                            return time.Substring(0, currentPostition);
                        }
                        catch
                        {
                            hours = 1;
                            return "1";
                        }
                    }
                    else
                    {
                        currentPostition++;
                    }

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Could not parse the timeleftdate :" + ex.ToString());
            }

            return "~";
        }

        

    }
}
