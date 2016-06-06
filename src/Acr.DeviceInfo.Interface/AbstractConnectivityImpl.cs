using System;


namespace Acr.DeviceInfo
{
    public abstract class AbstractConnectivityImpl : IConnectivity
    {
        public abstract bool IsInternetAvailable { get; }
        public abstract ConnectionStatus InternetReachability { get; }
        public abstract string CellularNetworkCarrier { get; }
        public abstract string IpAddress { get; }
        public abstract string WifiSsid { get; }


        EventHandler stateHandler;
        public event EventHandler StateChanged
        {
            add
            {
                if (this.stateHandler == null)
                    this.StartMonitoringConnection();

                this.stateHandler += value;
            }
            remove
            {
                this.stateHandler -= value;

                if (this.stateHandler == null)
                    this.StopMonitoringConnection();
            }
        }


        protected abstract void StartMonitoringConnection();
        protected abstract void StopMonitoringConnection();


        protected virtual void OnStateChanged()
        {
            this.stateHandler?.Invoke(this, EventArgs.Empty);
        }
    }
}
