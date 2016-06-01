using System;


namespace Acr.DeviceInfo
{
    public abstract class AbstractBatteryImpl : IBattery
    {
        public int Percentage { get; protected set; }
        public bool IsCharging { get; protected set; }


        EventHandler stateHandler;
        public event EventHandler StateChanged
        {
            add
            {
                if (this.stateHandler == null)
                    this.StartMonitoringState();

                this.stateHandler += value;
            }
            remove
            {
                this.stateHandler -= value;

                if (this.stateHandler == null)
                    this.StopMonitoringState();
            }
        }


        protected abstract void StartMonitoringState();
        protected abstract void StopMonitoringState();


        protected virtual void OnStateChanged()
        {
            this.stateHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
