using System;
using System.Windows.Forms;


namespace Acr.DeviceInfo
{

    public class BatteryImpl : AbstractBatteryImpl
    {
        //readonly Timer timer;


        //public BatteryImpl()
        //{
        //    this.timer = new Timer { Interval = (int)TimeSpan.FromSeconds(10).TotalMilliseconds };
        //    this.timer.Tick += (sender, args) => this.UpdateState();
        //    this.timer.Start();
        //}


        //void UpdateState()
        //{
        //    this.IsCharging = (SystemInformation.PowerStatus.BatteryChargeStatus == BatteryChargeStatus.Charging);
        //    this.Percentage = Convert.ToInt32(SystemInformation.PowerStatus.BatteryLifePercent);
        //}
    }
}
