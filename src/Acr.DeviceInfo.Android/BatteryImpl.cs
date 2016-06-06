using System;


namespace Acr.DeviceInfo
{

    public class BatteryImpl : AbstractBatteryImpl
    {

        //public BatteryImpl()
        //{
        //    BatteryBroadcastReceiver.StatusChanged += (sender, args) => this.SetState();
        //    BatteryBroadcastReceiver.Register();
        //}


        //void SetState()
        //{
        //    this.Percentage = BatteryBroadcastReceiver.Percentage;
        //    this.IsCharging = BatteryBroadcastReceiver.IsCharging;
        //}
        public override int Percentage { get; }
        public override bool IsCharging { get; }
        protected override void StartMonitoringState()
        {
            throw new NotImplementedException();
        }

        protected override void StopMonitoringState()
        {
            throw new NotImplementedException();
        }
    }
}
