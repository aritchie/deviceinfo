using System;


namespace Plugin.DeviceInfo
{

    public interface INetwork
    {
        //IObservable<IWifiScanResult> ScanForWifiNetworks();
        //IObservable<Unit> ConnectToWifi(string ssid, string password);

        NetworkReachability InternetReachability { get; }
        string CellularNetworkCarrier { get; }
        string IpAddress { get; }
        string WifiSsid { get; }

        IObservable<NetworkReachability> WhenStatusChanged();
    }
}
