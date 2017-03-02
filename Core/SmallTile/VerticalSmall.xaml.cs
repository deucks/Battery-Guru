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
    public partial class VerticalSmall : UserControl
    {
        public VerticalSmall()
        {
            InitializeComponent();
            setup();
        }

        public void setup()
        {
            bool isCharging = SystemEndPoints.isCharging();
            if (isCharging)
            {
                string chargeString = SystemEndPoints.calculateChargeRateString("in");
                if (chargeString.Contains("calculating"))
                    ChargeStatusDescription.Text = "calculating...";
                else if (chargeString.Contains("fully"))
                    ChargeStatusDescription.Text = "fully charged";
                else
                    ChargeStatusDescription.Text = "charging...";
            }
            else
            {
                string chargeString = SystemEndPoints.getTimeTillPowerOff();
                if (chargeString.Contains("calculating"))
                    ChargeStatusDescription.Text = "calculating...";
                else if (chargeString.Contains("fully"))
                    ChargeStatusDescription.Text = "fully charged";
                else
                    ChargeStatusDescription.Text = SystemEndPoints.getTimeTillPowerOff() + " left";

            }
            ChargeStatus.Text = SystemEndPoints.currentBatteryLevel() + "%";

            double chargeLevel = SystemEndPoints.currentBatteryLevel();
            backgroundStatus.Height = (chargeLevel / 100) * 336;
        }
    }
}
