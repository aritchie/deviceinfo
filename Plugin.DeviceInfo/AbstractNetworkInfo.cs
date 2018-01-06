using System;
using System.Reactive;
using System.Reactive.Linq;


namespace Plugin.DeviceInfo
{
    public abstract class AbstractNetworkInfo : INetworkInfo
    {
        public virtual IObservable<IWifiScanResult> ScanForWifiNetworks() => Observable.Empty<IWifiScanResult>();
        public virtual IObservable<Unit> ConnectToWifi(string ssid, string password) => Observable.Empty<Unit>();

        public virtual NetworkReachability InternetReachability => NetworkReachability.Unknown;
        public virtual string CellularNetworkCarrier => null;
        public virtual string IpAddress => null;
        public virtual string WifiSsid => null;

        public virtual IObservable<NetworkReachability> WhenStatusChanged() => Observable.Empty<NetworkReachability>();
    }
}
