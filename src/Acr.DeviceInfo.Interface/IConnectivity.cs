using System;


namespace Acr.DeviceInfo
{

    public interface IConnectivity
    {
        NetworkReachability InternetReachability { get; }
        string CellularNetworkCarrier { get; }
        string IpAddress { get; }
        string WifiSsid { get; }

        IObservable<NetworkReachability> WhenStatusChanged();
    }
}
