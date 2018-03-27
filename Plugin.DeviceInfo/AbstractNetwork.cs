using System;
using System.Reactive.Linq;


namespace Plugin.DeviceInfo
{
    public abstract class AbstractNetwork : INetwork
    {
        //public virtual IObservable<IWifiScanResult> ScanForWifiNetworks() => Observable.Empty<IWifiScanResult>();
        //public virtual IObservable<Unit> ConnectToWifi(string ssid, string password) => Observable.Empty<Unit>();

        public virtual NetworkType InternetNetworkType => NetworkType.Unknown;
        public virtual string CellularNetworkCarrier => null;
        public virtual string IpAddress => null;
        public virtual string WifiSsid => null;

        public virtual IObservable<NetworkType> WhenNetworkTypeChanged() => Observable.Empty<NetworkType>();
    }
}
