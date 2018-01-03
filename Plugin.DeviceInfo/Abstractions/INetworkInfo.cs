using System;


namespace Plugin.DeviceInfo
{

    public interface INetworkInfo
    {
        NetworkReachability InternetReachability { get; }
        string CellularNetworkCarrier { get; }
        string IpAddress { get; }
        string WifiSsid { get; }

        IObservable<NetworkReachability> WhenStatusChanged();
    }
}
