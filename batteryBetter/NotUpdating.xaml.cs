using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace batteryBetter
{
    public partial class NotUpdating : PhoneApplicationPage
    {
        public NotUpdating()
        {
            InitializeComponent();
        }

        private async void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-power:"));
        }
    }
}