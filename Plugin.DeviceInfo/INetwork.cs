using System;


namespace Plugin.DeviceInfo
{

    public interface INetwork
    {
        //IObservable<IWifiScanResult> ScanForWifiNetworks();
        //IObservable<Unit> ConnectToWifi(string ssid, string password);

        NetworkType InternetNetworkType { get; }
        string CellularNetworkCarrier { get; }
        string IpAddress { get; }
        string WifiSsid { get; }

        IObservable<NetworkType> WhenNetworkTypeChanged();
    }
}
