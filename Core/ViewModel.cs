using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace Core
{
    public class UsageModel
    {
        public double quantity { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

    public class DetailedModel
    {
        public int quantity { get; set; }
        public string date { get; set; }
        public string description { get; set; }
        public string change { get; set; }


        public bool wifiStatus { get; set; }
        public bool phoneStatus { get; set; }
        public bool locationStatus { get; set; }
        public bool isCharging { get; set; }
        public bool isInternetOn { get; set; }
        public bool isAirplaneOn { get; set; }
        public bool isRoaming { get; set; }
        public bool isOverDataLimit { get; set; }
        
    }

    public class Model
    {
        public DateTime date { get; set; }
        public int quantity { get; set; }
        public bool wifiStatus { get; set; }
        public bool phoneStatus { get; set; }
        public bool locationStatus { get; set; }
        public bool isCharging { get; set; }
        public bool isInternetOn { get; set; }
        public bool isAirplaneOn { get; set; }
        public bool isRoaming { get; set; }
        public bool isOverDataLimit { get; set; }

    }

    public class ViewModel
    {
        List<Model> batteryModel = new List<Model>();

        public ViewModel()
        {
            loadData();
        }

        public List<Model> getModel()
        {
            return batteryModel;
        }


        public List<UsageModel> getUsageModel(string type)
        {
            List<UsageModel> usageModelList = new List<UsageModel>();

            batteryModel.Reverse();

            if (type == "last charge")
            {
                int totalCharge = SystemEndPoints.countLastChargeFactors("all");
                string description = "";
                //wifi stuff here

                int wifiCharges = SystemEndPoints.countLastChargeFactors("wifi");
                double wifiPerentage = ((double)wifiCharges / (double)totalCharge) * 100;
                description = "used " + wifiPerentage.ToString("0.00") + "% of the time, recorded " + wifiCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Wifi", quantity = (int)wifiPerentage, description = description });

                //mobile stuff here

                int phoneCharges = SystemEndPoints.countLastChargeFactors("phone");
                double phonePerentage = ((double)phoneCharges / (double)totalCharge) * 100;
                description = "used " + phonePerentage.ToString("0.00") + "% of the time, recorded " + phoneCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Mobile Data", quantity = (int)phonePerentage, description = description });
                
                //airplane stuff here

                int airplaneCharges = SystemEndPoints.countLastChargeFactors("airplane");
                double airplanePerentage = ((double)airplaneCharges / (double)totalCharge) * 100;
                description = "offline " + airplanePerentage.ToString("0.00") + "% of the time, recorded " + airplaneCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Airplane Mode", quantity = (int)airplanePerentage, description = description });

                //internet stuff here

                int internetCharges = SystemEndPoints.countLastChargeFactors("internet");
                double internetPerentage = ((double)internetCharges / (double)totalCharge) * 100;
                description = "used " + internetPerentage.ToString("0.00") + "% of the time, recorded " + internetCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Internet Access", quantity = (int)internetPerentage, description = description });

                //roaming stuff here

                int roamingCharges = SystemEndPoints.countLastChargeFactors("roaming");
                double roamingPerentage = ((double)roamingCharges / (double)totalCharge) * 100;
                description = "used " + roamingPerentage.ToString("0.00") + "% of the time, recorded " + roamingCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Roaming Access", quantity = (int)roamingPerentage, description = description });
                
                //over data limit stuff here

                int overDataCharges = SystemEndPoints.countLastChargeFactors("overData");
                double overDataPerentage = ((double)overDataCharges / (double)totalCharge) * 100;
                description = "went over " + overDataPerentage.ToString("0.00") + "% of the time, recorded " + overDataCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Data Limit", quantity = (int)overDataPerentage, description = description });

            }
            else if (type == "last 100% charge")
            {
                int totalCharge = SystemEndPoints.countHundredChargeFactors("all");
                string description = "";
                //wifi stuff here

                int wifiCharges = SystemEndPoints.countHundredChargeFactors("wifi");
                double wifiPerentage = ((double)wifiCharges / (double)totalCharge) * 100;
                description = "used " + wifiPerentage.ToString("0.00") + "% of the time, recorded " + wifiCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Wifi", quantity = (int)wifiPerentage, description = description });

                //mobile stuff here

                int phoneCharges = SystemEndPoints.countHundredChargeFactors("phone");
                double phonePerentage = ((double)phoneCharges / (double)totalCharge) * 100;
                description = "used " + phonePerentage.ToString("0.00") + "% of the time, recorded " + phoneCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Mobile Data", quantity = (int)phonePerentage, description = description });

                //airplane stuff here

                int airplaneCharges = SystemEndPoints.countHundredChargeFactors("airplane");
                double airplanePerentage = ((double)airplaneCharges / (double)totalCharge) * 100;
                description = "offline " + airplanePerentage.ToString("0.00") + "% of the time, recorded " + airplaneCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Airplane Mode", quantity = (int)airplanePerentage, description = description });

                //internet stuff here

                int internetCharges = SystemEndPoints.countHundredChargeFactors("internet");
                double internetPerentage = ((double)internetCharges / (double)totalCharge) * 100;
                description = "used " + internetPerentage.ToString("0.00") + "% of the time, recorded " + internetCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Internet Access", quantity = (int)internetPerentage, description = description });

                //roaming stuff here

                int roamingCharges = SystemEndPoints.countHundredChargeFactors("roaming");
                double roamingPerentage = ((double)roamingCharges / (double)totalCharge) * 100;
                description = "used " + roamingPerentage.ToString("0.00") + "% of the time, recorded " + roamingCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Roaming Access", quantity = (int)roamingPerentage, description = description });

                //over data limit stuff here

                int overDataCharges = SystemEndPoints.countHundredChargeFactors("overData");
                double overDataPerentage = ((double)overDataCharges / (double)totalCharge) * 100;
                description = "went over " + overDataPerentage.ToString("0.00") + "% of the time, recorded " + overDataCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Data Limit", quantity = (int)overDataPerentage, description = description });
            }
            else
            {
                int totalCharge = SystemEndPoints.countAllTimeFactors("all");
                string description = "";
                //wifi stuff here

                int wifiCharges = SystemEndPoints.countAllTimeFactors("wifi");
                double wifiPerentage = ((double)wifiCharges / (double)totalCharge) * 100;
                description = "used " + wifiPerentage.ToString("0.00") + "% of the time, recorded " + wifiCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Wifi", quantity = (int)wifiPerentage, description = description });

                //mobile stuff here

                int phoneCharges = SystemEndPoints.countAllTimeFactors("phone");
                double phonePerentage = ((double)phoneCharges / (double)totalCharge) * 100;
                description = "used " + phonePerentage.ToString("0.00") + "% of the time, recorded " + phoneCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Mobile Data", quantity = (int)phonePerentage, description = description });

                //airplane stuff here

                int airplaneCharges = SystemEndPoints.countAllTimeFactors("airplane");
                double airplanePerentage = ((double)airplaneCharges / (double)totalCharge) * 100;
                description = "offline " + airplanePerentage.ToString("0.00") + "% of the time, recorded " + airplaneCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Airplane Mode", quantity = (int)airplanePerentage, description = description });

                //internet stuff here

                int internetCharges = SystemEndPoints.countAllTimeFactors("internet");
                double internetPerentage = ((double)internetCharges / (double)totalCharge) * 100;
                description = "used " + internetPerentage.ToString("0.00") + "% of the time, recorded " + internetCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Internet Access", quantity = (int)internetPerentage, description = description });

                //roaming stuff here

                int roamingCharges = SystemEndPoints.countAllTimeFactors("roaming");
                double roamingPerentage = ((double)roamingCharges / (double)totalCharge) * 100;
                description = "used " + roamingPerentage.ToString("0.00") + "% of the time, recorded " + roamingCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Roaming Access", quantity = (int)roamingPerentage, description = description });

                //over data limit stuff here

                int overDataCharges = SystemEndPoints.countAllTimeFactors("overData");
                double overDataPerentage = ((double)overDataCharges / (double)totalCharge) * 100;
                description = "went over " + overDataPerentage.ToString("0.00") + "% of the time, recorded " + overDataCharges.ToString() + " times";
                usageModelList.Add(new UsageModel { name = "Data Limit", quantity = (int)overDataPerentage, description = description });
            }

            batteryModel.Reverse();
            //detailedModelList.Reverse();

            return usageModelList;
        }


        public List<DetailedModel> getDetailedModel(string type)
        {
            List<DetailedModel> detailedModelList = new List<DetailedModel>();

            batteryModel.Reverse();
            string change = "not enough data";
            if (type == "minutely")
            {
                
                int limit = 0;
                foreach (Model vm in batteryModel)
                {
                    string listDescription = vm.quantity.ToString() + "%";
                    //if (vm.wifiStatus)
                    //    listDescription += " - Wifi On";
                    //if (vm.phoneStatus)
                    //    listDescription += " - Data On";
                    //if (vm.isCharging)
                    //    listDescription += " - Plugged In";

                    listDescription += " - Tap to get more info";
                    if (limit > 15) //getting usage over 15 minutes
                    {
                        try
                        {
                            change = (vm.quantity - batteryModel[limit + 5].quantity).ToString() + "% in the last 5 minutes";
                        }
                        catch
                        {
                            //index out of range
                            change = (vm.quantity - batteryModel[limit].quantity).ToString() + "% in the last 5 minutes";
                        }

                    }

                    if (limit < 241)
                        detailedModelList.Add(new DetailedModel { quantity = vm.quantity, change = change, date = vm.date.ToString("hh:mm tt, ") + vm.date.DayOfWeek.ToString(), description = listDescription, isAirplaneOn = vm.isAirplaneOn, isCharging = vm.isCharging, isInternetOn = vm.isInternetOn, isOverDataLimit = vm.isOverDataLimit, isRoaming = vm.isRoaming, locationStatus = vm.locationStatus, phoneStatus = vm.phoneStatus, wifiStatus = vm.wifiStatus });
                    limit++;
                }
            }
            else
            {
                int hourLimit = 60;
                foreach (Model vm in batteryModel)
                {
                    string listDescription = vm.quantity.ToString() + "%";
                    if (vm.wifiStatus)
                        //listDescription += " - Wifi On";
                    if (vm.phoneStatus)
                        //listDescription += " - Data On";
                    if (vm.isCharging)
                        //listDescription += " - Plugged In";

                    listDescription += " - Tap to get more info";

                    if (hourLimit == 60)
                    {
                        detailedModelList.Add(new DetailedModel { quantity = vm.quantity, date = vm.date.ToString("hh:mm tt, ") + vm.date.DayOfWeek.ToString(), description = listDescription, isAirplaneOn = vm.isAirplaneOn, isCharging = vm.isCharging, isInternetOn = vm.isInternetOn, isOverDataLimit = vm.isOverDataLimit, isRoaming = vm.isRoaming, locationStatus = vm.locationStatus, phoneStatus = vm.phoneStatus, wifiStatus = vm.wifiStatus });
                        hourLimit = 0;
                    }
                    else
                    {
                        hourLimit++;
                    }
                        
                }
            }

            batteryModel.Reverse();
            //detailedModelList.Reverse();

            return detailedModelList;

        }

        public int numberOfItems()
        {
            int count = 0;
            foreach (Model vm in batteryModel)
            {
                count++;
            }
            return count;
        }



        public void add(int batteryAmount, bool wifi, bool phone, bool location, bool isCharging, bool airplane, bool isInternetOn, bool isOverDataLimit, bool isRoaming)
        {
            SystemEndPoints.updateLockTile(batteryAmount);
            loadData();
            batteryModel.Add(new Model { date = DateTime.Now, quantity = batteryAmount, locationStatus = location, phoneStatus = phone, wifiStatus = wifi, isCharging = isCharging, isAirplaneOn = airplane, isInternetOn = isInternetOn, isOverDataLimit = isOverDataLimit, isRoaming = isRoaming });
            saveData();
        }

        public void deleteAllData()
        {
            var isoStore = IsolatedStorageFile.GetUserStoreForApplication();

            if (isoStore.FileExists("battery.xml"))
            {
                isoStore.DeleteFile("battery.xml");
            }
        }

        public string ReadFile()
        {
            IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
            StreamReader Reader = null;
            try
            {
                Reader = new StreamReader(new IsolatedStorageFileStream("battery.xml", FileMode.Open, fileStorage));
                string textFile = Reader.ReadToEnd();
                return textFile;
            }
            catch
            {
                return "file not created";
            }
        }

        public void loadData()
        {
            try
            {
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("battery.xml", FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Model>));
                        batteryModel = (List<Model>)serializer.Deserialize(stream);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Loading data failed");
            }
        }

        public void saveData()
        {
            try
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;                

                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile("battery.xml", FileMode.Create))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Model>));
                        using (XmlWriter xmlWriter = XmlWriter.Create(stream, xmlWriterSettings))
                        {
                            serializer.Serialize(xmlWriter, batteryModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + "save data method");
            }

        }
    }
}
