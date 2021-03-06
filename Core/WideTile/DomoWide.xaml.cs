﻿using System;
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
    public partial class DomoWide : UserControl
    {
        public DomoWide()
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
                    ChargeStatusDescription.Text = "fully charged, you can unplug me";
                else
                    ChargeStatusDescription.Text = "fully charged in " + chargeString;
            }
            else
            {
                string chargeString = SystemEndPoints.getTimeTillPowerOff();
                if (chargeString.Contains("calculating"))
                    ChargeStatusDescription.Text = "calculating...";
                else if (chargeString.Contains("fully"))
                    ChargeStatusDescription.Text = "fully charged, you can unplug me";
                else
                    ChargeStatusDescription.Text = "power off in " + SystemEndPoints.getTimeTillPowerOff();

            }
            hour.Text = SystemEndPoints.currentBatteryLevel() + "%";
        }

    }
}
