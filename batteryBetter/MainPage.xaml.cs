using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using batteryBetter.Resources;
using Microsoft.Phone.Scheduler;
using System.Diagnostics;
using Core;
using System.Windows.Threading;
using Windows.Phone.Devices.Power;
using Windows.Networking.Connectivity;
using Microsoft.Phone.Tasks;
using Windows.ApplicationModel.Store;

namespace batteryBetter
{
    public partial class MainPage : PhoneApplicationPage
    {
        PeriodicTask periodicTask;

        string periodicTaskName = "PeriodicAgent";

        public bool agentsAreEnabled = true;

        ViewModel vm = new ViewModel();

        string DetailedListType = "minutely";

        string UsageListType = "last charge";

        string GraphType = "last hour";

        GraphModel graphModel = new GraphModel();

        Battery _battery;

        Settings appSettings = new Settings();

        int proCount = 0;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            updateStates();
            StartPeriodicAgent();

            _battery = Battery.GetDefault();
            _battery.RemainingChargePercentChanged += OnRemainingChargePercentChanged;

            preSetup();

            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            checkLastUpdate();
        }

        public void checkLastUpdate()
        {
            if (SystemEndPoints.minutesTillLastUpdate() > 5)
            {
                MessageBoxResult m = MessageBox.Show("The app is not updating properly to log battery data." + Environment.NewLine + Environment.NewLine + "This may be the result of other apps running in the background that are not needed." + Environment.NewLine + Environment.NewLine + "Would you like to see tips on how to fix this?", "BatteryGuru not updating?", MessageBoxButton.OKCancel);
                if (m == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new Uri("/NotUpdating.xaml", UriKind.Relative));
                }
            }
        }

        public void preSetup()
        {


            //Enable app to run in the background detailedList
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;

            //Start the dispatch timer
            DispatcherTimer newTimer = new DispatcherTimer();
            // timer interval specified as 1 second
            newTimer.Interval = TimeSpan.FromSeconds(30);
            // Sub-routine OnTimerTick will be called at every 1 second
            newTimer.Tick += OnTimerTick;
            // starting the timer
            newTimer.Start();

            //set grapgh model
            areachart.DataContext = graphModel;

            //if true, internet is accessible.
            Debug.WriteLine(" internet access " + SystemEndPoints.isInternetOn().ToString());

            Debug.WriteLine(" airplane access " + SystemEndPoints.isAirplaneOn().ToString());

            Debug.WriteLine(" data access " + SystemEndPoints.isOverDataLimit().ToString());

            Debug.WriteLine(" number of  " + SystemEndPoints.countLastChargeFactors("all").ToString());

            if (appSettings.getSettingsBool(appSettings.AppRated))
                rateMe.Text = "rate me";

            if (appSettings.getSettingsBool(appSettings.ProEnabled))
                goProButton.Visibility = System.Windows.Visibility.Collapsed;

            
        }

        #region UI Definitions
        public void updateStates()
        {          
            updateUI();

            bool isCharging = SystemEndPoints.isCharging();
            if (isCharging)
            {
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    DischarginRateTitle.Text = "CHARGING RATE";
                    PowerOffTitle.Text = "FULL CHARGE IN";
                    PowerOffAtTitle.Text = "FULL CHARGE AT";

                    DischargeRateInput.Text = SystemEndPoints.calculateChargeRateString("rate");
                    powerOffInInput.Text = SystemEndPoints.calculateChargeRateString("in");
                    powerOffAtInput.Text = SystemEndPoints.calculateChargeRateString("at");
                    timeSinceLastChargeInput.Text = "currently charging...";
                });
                

                //Debug.WriteLine(SystemEndPoints.calculateChargeRate());
            }
            else
            {
                System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    DischarginRateTitle.Text = "DISCHARGING RATE";
                    PowerOffTitle.Text = "POWER OFF";
                    PowerOffAtTitle.Text = "POWER OFF AT";

                    timeSinceLastChargeInput.Text = SystemEndPoints.timeSinceLastCharge();
                    DischargeRateInput.Text = SystemEndPoints.getTimeRatio();
                    powerOffAtInput.Text = SystemEndPoints.getPowerOffTime();
                    powerOffInInput.Text = SystemEndPoints.getTimeTillPowerOff();
                });
                
            }
        }

        public void updateUI()
        {
            int currentStatus = SystemEndPoints.currentBatteryLevel();

            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                ChargingStatusBar.Value = currentStatus;
                ChargeStatus.Text = currentStatus + "%";
            });

            //updateDetailedListUI();


        }

        public void updateDetailedListUI()
        {
            try
            {
                detailedList.ItemsSource = null;
                detailedList.ItemsSource = vm.getDetailedModel(DetailedListType);
            }
            catch ( Exception ex)
            {
                Debug.WriteLine("Some null error in update detailel list : " + ex.ToString());
            }


            try
            {
                usageList.ItemsSource = null;
                usageList.ItemsSource = vm.getUsageModel(UsageListType);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Some null error in update usage list : " + ex.ToString());
            }
            
            
        }
        #endregion 

        //More Backend stuff here, like dispatch timers and stuff like that  
        #region Front End Communication 
        void OnTimerTick(Object sender, EventArgs args)
        {
            //Gets called every miute or so
            updateStates();
        }
        #endregion 


        #region Various event handlers

        private void OnRemainingChargePercentChanged(object sender, object e)
        {
            updateStates();
        }

        private void UsageSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListPickerItem lpi = (sender as ListPicker).SelectedItem as ListPickerItem;
            
            UsageListType = lpi.Content.ToString();
            updateDetailedListUI();
        }

        private void detailedList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DetailedModel mySelectedItem = detailedList.SelectedItem as DetailedModel;
            //detailedList.SelectedIndex = -1;
            

            bool proEnabled = appSettings.getSettingsBool(appSettings.ProEnabled);
            if (proEnabled)
            {
                updateDetailedListData(mySelectedItem);
                showDetailed.Begin();
            }
            else
            {
                MessageBoxResult m = MessageBox.Show("This function requires the pro upgrade of the app." + Environment.NewLine + Environment.NewLine + "With the pro version, you will be able to view detailed battery consumption, remove ads, enable a custom live tiles, support the developer and much more." + Environment.NewLine + Environment.NewLine + "Would you like enable the pro version?", "Go pro!", MessageBoxButton.OKCancel);
                if (m == MessageBoxResult.OK)
                {
                    upgradePro();
                }
                return;
            }


            //selectedBook.
        }

        private void updateDetailedListData(DetailedModel data)
        {
            detailedTime.Text = data.date; 
            detailedbattery.Text = data.quantity.ToString() + "%";

            detailedChange.Text = data.change; 

            if (data.isCharging)
            {
                onPluggedIn.Visibility = System.Windows.Visibility.Visible;
                onBattery.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                onPluggedIn.Visibility = System.Windows.Visibility.Collapsed;
                onBattery.Visibility = System.Windows.Visibility.Visible;
            }

            if (!data.wifiStatus)
            {
                onWifiOn.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (!data.isAirplaneOn)
            {
                onOffline.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (!data.isInternetOn)
            {
                onInternet.Visibility = System.Windows.Visibility.Collapsed;
            }

            if (!data.isOverDataLimit)
            {
                onDataLimit.Visibility = System.Windows.Visibility.Collapsed;
            }


        }

        private void DetailedSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListPickerItem lpi = (sender as ListPicker).SelectedItem as ListPickerItem;
            DetailedListType = lpi.Content.ToString();
            updateDetailedListUI();
        }

        private void usageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            usageList.SelectedIndex = -1;
        }

        private void GraphSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int executeOnce = 0;
            try
            {
                ListPickerItem lpi = (sender as ListPicker).SelectedItem as ListPickerItem;
                GraphType = lpi.Content.ToString();
                sortText.Text = GraphType;

                if (executeOnce == 0)
                {
                    graphModel.setType(GraphType);
                    executeOnce++;
                    Debug.WriteLine(GraphType.ToString());

                    if (GraphType == "last hour")
                    {
                        graphModel.GenerateLastHour();
                    }
                    else if (GraphType == "last 24 hours")
                    {
                        graphModel.GenerateDaily();
                    }
                    else if (GraphType == "last 7 days")
                    {
                        graphModel.GenerateWeekly();
                    }
                    else if (GraphType == "last charge")
                    {
                        graphModel.GenerateLastCharge();
                        sortText.Text += " (" + timeSinceLastChargeInput.Text + ")";
                    }
                    else if (GraphType == "last 100% charge")
                    {
                        graphModel.GenerateAllTime();
                        sortText.Text += " (" + SystemEndPoints.timeSinceLastHundred() + ")";
                    }
                    areachart.DataContext = graphModel;
                }


            }
            catch
            {
                //selection failed
            }

            

            //updateDetailedListUI();
        }

        private async void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-wifi:"));
        }

        private async void Image_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-cellular:"));
        }

        private async void Image_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-airplanemode:"));
        }

        private async void Image_Tap_3(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-power:"));
        }

        #endregion 

        #region Periodic Agent
        /// <summary>
        /// The Periodic Agent code runner, only needs to be called once when the app starts
        /// </summary>
        private void StartPeriodicAgent()
        {
            // is old task running, remove it
            periodicTask = ScheduledActionService.Find(periodicTaskName) as PeriodicTask;
            if (periodicTask != null)
            {
                try
                {
                    ScheduledActionService.Remove(periodicTaskName);
                }
                catch (Exception)
                {
                }
            }
            // create a new task
            periodicTask = new PeriodicTask(periodicTaskName);
            // load description from localized strings
            periodicTask.Description = "Updates lockscreen for Outset app";
            // set expiration days
            periodicTask.ExpirationTime = DateTime.Now.AddDays(14);
            try
            {
                // add thas to scheduled action service
                ScheduledActionService.Add(periodicTask);

                // debug, so run in every 30 secs
                ScheduledActionService.LaunchForTest(periodicTaskName, TimeSpan.FromSeconds(30));

            }
            catch (InvalidOperationException exception)
            {
                Debug.WriteLine(exception.ToString());

                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    // load error text from localized strings
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }
                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }
        }
        #endregion

        #region settings page event handlers

        private void notifSetting_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Notifications.xaml", UriKind.Relative));
        }

        private void liveTileSetting_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LiveTileOptions.xaml", UriKind.Relative));
        }

        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("Flip".ToString()));
                TileToFind.Delete();

                FlipTileData tileData = new FlipTileData
                {
                    BackgroundImage = new Uri("Icons/runNew.png", UriKind.RelativeOrAbsolute),
                    WideBackgroundImage = new Uri("Icons/wideRun.png", UriKind.RelativeOrAbsolute)
                };

                ShellTile.Create(new Uri("/MainPage.xaml?Flip", UriKind.Relative), tileData, true);
            }
            catch (Exception ex)
            {
                FlipTileData tileData = new FlipTileData
                {
                    BackgroundImage = new Uri("Icons/runNew.png", UriKind.RelativeOrAbsolute),
                    WideBackgroundImage = new Uri("Icons/wideRun.png", UriKind.RelativeOrAbsolute)
                };

                ShellTile.Create(new Uri("/MainPage.xaml?Flip", UriKind.Relative), tileData, true);
            }
        }

        private void TextBlock_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            appSettings.saveSettingsBool(appSettings.AppRated, true);

            rateMe.Text = "rate me";

            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }

        private async void TextBlock_Tap_3(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("fb:pages?id=293494454188542"));
        }

        private void TextBlock_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("Thank you for the interest in the pro upgrade of this app." + Environment.NewLine + Environment.NewLine + "With it you will be able to view detailed battery consumption, remove ads, enable a custom live tiles, support the developer and much more." + Environment.NewLine + Environment.NewLine + "Would you like enable the pro version?", "Go pro!", MessageBoxButton.OKCancel);
            if (m == MessageBoxResult.OK)
            {
                upgradePro();
            }

        }


        private void TextBlock_Tap_4(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            emailComposeTask.Subject = "BatteryGuru";
            emailComposeTask.To = "cskdev@hotmail.com";
            emailComposeTask.Show();
        }

        private async void enablelockScreen_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var op = await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-lock:"));
        }

        private void resetData_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("This change is irreversible. Press Ok to delete data.", "Are you sure you want to reset data?", MessageBoxButton.OKCancel);
            if (m == MessageBoxResult.OK)
            {
                vm.deleteAllData();
                MessageBox.Show("Please restart the app.");
            }
        }

        private void notUpdating_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NotUpdating.xaml", UriKind.Relative));
        }
        #endregion



        #region upgradePro

        private async void upgradePro()
        {
            try
            {
                var listing = await CurrentApp.LoadListingInformationAsync();
                var superweapon =
                listing.ProductListings.FirstOrDefault(p => p.Value.ProductId == "proUpgrade" && p.Value.ProductType == ProductType.Durable);
                try
                {
                    if (CurrentApp.LicenseInformation.ProductLicenses[superweapon.Value.ProductId].IsActive)
                    {
                        appSettings.saveSettingsBool(appSettings.ProEnabled, true);
                        goProButton.Visibility = System.Windows.Visibility.Collapsed;
                        MessageBox.Show("You already own the pro upgrade, the purchase has been restored.", "Restored", MessageBoxButton.OK);
                    }
                    else
                    {
                        string receipt = await CurrentApp.RequestProductPurchaseAsync(superweapon.Value.ProductId, true);

                        appSettings.saveSettingsBool(appSettings.ProEnabled, true);
                        goProButton.Visibility = System.Windows.Visibility.Collapsed;

                        MessageBox.Show("Thank you for purchasing the pro upgrade and supporting the developer. You can now use the pro features! :).", "Awesome", MessageBoxButton.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot purchase at the moment.");
                }
            }
            catch
            {
                Debug.WriteLine("ayyy pro upgrade aint working mate");
            }

        }

        #endregion

        #region detailed hide
        private void grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            hideDetailed.Begin();
        }

        #endregion 

        private void TextBlock_Tap_5(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (proCount == 10)
            {
                appSettings.saveSettingsBool(appSettings.ProEnabled, true);
                MessageBox.Show("Pro enabled");
            }
            proCount++;
        }

        private void TextBlock_Tap_6(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Debug.WriteLine(vm.ReadFile());
        }

























    }
}