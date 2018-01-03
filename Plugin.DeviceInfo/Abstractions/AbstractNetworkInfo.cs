using System;
using System.Reactive.Linq;


namespace Plugin.DeviceInfo
{
    public abstract class AbstractNetworkInfo : INetworkInfo
    {
        public virtual NetworkReachability InternetReachability => NetworkReachability.Unknown;
        public virtual string CellularNetworkCarrier => null;
        public virtual string IpAddress => null;
        public virtual string WifiSsid => null;

        public virtual IObservable<NetworkReachability> WhenStatusChanged() => Observable.Return(NetworkReachability.Unknown);
    }
}
