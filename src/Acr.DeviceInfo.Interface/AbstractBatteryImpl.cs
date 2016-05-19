using System;


namespace Acr.DeviceInfo
{
    public abstract class AbstractBatteryImpl : IBattery
    {
        public int Percentage { get; protected set; }
        public bool IsCharging { get; protected set; }
        public event EventHandler StateChanged;
    }
}
