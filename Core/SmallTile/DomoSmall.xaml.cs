using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Core.SmallTile
{
    public partial class DomoSmall : UserControl
    {
        public DomoSmall()
        {
            InitializeComponent();
            setup();
        }

        public void setup()
        {
            hour.Text = SystemEndPoints.currentBatteryLevel() + "%";
        }

    }
}
