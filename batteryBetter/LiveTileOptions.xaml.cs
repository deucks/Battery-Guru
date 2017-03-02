using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Core;
using System.Diagnostics;
using Windows.ApplicationModel.Store;

namespace batteryBetter
{
    public class ListItems
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Img { get; set; }
        public string Wide { get; set; }

    }

    public partial class LiveTileOptions : PhoneApplicationPage
    {
        List<ListItems> listItems = new List<ListItems>();

        Settings appSettings = new Settings();

        public LiveTileOptions()
        {
            InitializeComponent();
            addItems();
            listBox1.ItemsSource = listItems;
            
        }

        private void addItems()
        {
            listItems.Add(new ListItems { Name = "Defualt", Img = "http://i.imgur.com/soN91pb.png", Wide = "free" });
            listItems.Add(new ListItems { Name = "Domo", Img = "http://i.imgur.com/s2thPSS.png", Wide = "requires pro" });
            listItems.Add(new ListItems { Name = "Simple", Img = "http://i.imgur.com/yX4IrIU.png", Wide = "requires pro" });
            listItems.Add(new ListItems { Name = "Traditional", Img = "http://i.imgur.com/4pjqy2d.png", Wide = "requires pro" });
            listItems.Add(new ListItems { Name = "Vertical", Img = "http://i.imgur.com/qTpEU58.png", Wide = "requires pro" });
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                bool proEnabled = appSettings.getSettingsBool(appSettings.ProEnabled);

                ListItems lpi = (listBox1.SelectedItems[0]) as ListItems;

                listBox1.SelectedIndex = -1;

                if (lpi.Wide.Contains("pro"))
                {
                    if (!proEnabled)
                    {
                        MessageBoxResult m = MessageBox.Show("This function requires the pro upgrade of the app." + Environment.NewLine + Environment.NewLine + "With the pro version, you will be able to view detailed battery consumption, remove ads, enable a custom live tiles, support the developer and much more." + Environment.NewLine + Environment.NewLine + "Would you like enable the pro version?", "Go pro!", MessageBoxButton.OKCancel);
                        if (m == MessageBoxResult.OK)
                        {
                            upgradePro();
                        }
                        return;
                    }
                    else
                    {
                        appSettings.saveSettingsString(appSettings.TileType, lpi.Name);
                    }
                }
                else
                {
                    appSettings.saveSettingsString(appSettings.TileType, lpi.Name);
                }

                

                MessageBoxResult enable = MessageBox.Show("Would you like to enable this theme?", "Enable tile", MessageBoxButton.OKCancel);
                if (enable == MessageBoxResult.OK)
                {
                    pinTile();
                }
            }
            catch
            {
                Debug.WriteLine("listbox not working");
            }           
        }

        private void pinTile()
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
                        MessageBox.Show("You already own the pro upgrade, the purchase has been restored.", "Restored", MessageBoxButton.OK);
                    }
                    else
                    {
                        string receipt = await CurrentApp.RequestProductPurchaseAsync(superweapon.Value.ProductId, true);

                        appSettings.saveSettingsBool(appSettings.ProEnabled, true);

                        MessageBox.Show("Thank you for purchasing the pro upgrade. You can now use the pro features! :).", "Awesome", MessageBoxButton.OK);
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
    }
}