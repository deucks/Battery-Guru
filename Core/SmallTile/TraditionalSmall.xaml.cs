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
    public partial class TraditionalSmall : UserControl
    {
        public TraditionalSmall()
        {
            InitializeComponent();
            setup();
        }

        public void setup()
        {
            bool isCharging = SystemEndPoints.isCharging();
            ChargeStatus.Text = SystemEndPoints.currentBatteryLevel() + "%";

            double chargeLevel = SystemEndPoints.currentBatteryLevel();
            backgroundStatus.Width = (chargeLevel / 100) * 241;
        }
    }
}
