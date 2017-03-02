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
    public partial class TraditionalWide : UserControl
    {
        public TraditionalWide()
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
                {
                    ChargeStatusDescription.Text = "Calculating...";
                    Discharging.Text = "please wait";
                    PowerOffAt.Text = "";
                }
                else if (chargeString.Contains("fully"))
                {
                    ChargeStatusDescription.Text = "Fully charged";
                    Discharging.Text = "you can unplug me";
                    PowerOffAt.Text = "";
                }
                else
                {
                    ChargeStatusDescription.Text = "Charging...";
                    Discharging.Text = "fully charged in " + SystemEndPoints.calculateChargeRateString("in") + ", ";
                    PowerOffAt.Text = "at " + SystemEndPoints.calculateChargeRateString("at"); ;
                }
            }
            else
            {
                string chargeString = SystemEndPoints.getTimeTillPowerOff();
                if (chargeString.Contains("calculating"))
                {
                    ChargeStatusDescription.Text = "Calculating...";
                    Discharging.Text = "please wait";
                    PowerOffAt.Text = "";
                }
                else if (chargeString.Contains("fully"))
                {
                    ChargeStatusDescription.Text = "Fully charged";
                    Discharging.Text = "you can unplug me";
                    PowerOffAt.Text = "";
                }
                else
                {
                    ChargeStatusDescription.Text = SystemEndPoints.getTimeTillPowerOff() + " left";
                    Discharging.Text = "discharging at " + SystemEndPoints.getTimeRatio();
                    PowerOffAt.Text = "power off at " + SystemEndPoints.getPowerOffTime();
                }

            }

            ChargeStatus.Text = SystemEndPoints.currentBatteryLevel().ToString();

            double chargeLevel = SystemEndPoints.currentBatteryLevel();
            backgroundStatus.Width = (chargeLevel / 100) * 70;
        }
    }
}
