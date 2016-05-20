using System;


namespace Acr.DeviceInfo
{
    public abstract class AbstractConnectivityImpl : IConnectivity
    {
        public bool IsInternetAvailable { get; protected set; }
        public ConnectionStatus InternetReachability { get; protected set; }
        public string CellularNetworkCarrier { get; protected set; }
        public string IpAddress { get; protected set; }
        public string WifiSsid { get; protected set; }
        public event EventHandler StateChanged;

        protected abstract void StartMonitoringConnection();
        protected abstract void StopMonitoringConnection();
    }
}
