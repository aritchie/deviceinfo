using System;


namespace Acr.DeviceInfo
{
    public abstract class AbstractBatteryImpl : IBattery
    {
        public int Percentage { get; protected set; }
        public bool IsCharging { get; protected set; }
        public event EventHandler StateChanged;


        protected abstract void StartMonitoringState();
        protected abstract void StopMonitoringState();


        protected virtual void OnStateChanged()
        {
            this.StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
