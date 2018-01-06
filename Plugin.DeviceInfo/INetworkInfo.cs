using System;
using System.Reactive;


namespace Plugin.DeviceInfo
{

    public interface INetworkInfo
    {
        IObservable<IWifiScanResult> ScanForWifiNetworks();
        IObservable<Unit> ConnectToWifi(string ssid, string password);

        NetworkReachability InternetReachability { get; }
        string CellularNetworkCarrier { get; }
        string IpAddress { get; }
        string WifiSsid { get; }

        IObservable<NetworkReachability> WhenStatusChanged();
    }
}
