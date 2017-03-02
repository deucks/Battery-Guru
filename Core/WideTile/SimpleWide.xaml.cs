using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Core.WideTile
{
    public partial class SimpleWide : UserControl
    {
        public SimpleWide()
        {
            InitializeComponent();
            hour.Text = SystemEndPoints.currentBatteryLevel().ToString() + "%";
        }
    }
}
