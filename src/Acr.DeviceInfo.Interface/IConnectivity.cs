using System;


namespace Acr.DeviceInfo
{

    public interface IConnectivity
    {
        bool IsInternetAvailable { get; }
        ConnectionStatus InternetReachability { get; }
        string CellularNetworkCarrier { get; }
        string IpAddress { get; }
        string WifiSsid { get; }

        IObservable<ConnectionStatus> WhenStatusChanged();
    }
}
