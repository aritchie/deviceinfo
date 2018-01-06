using System;


namespace Plugin.DeviceInfo
{
    public interface IWifiScanResult
    {
        string Ssid { get; }
        bool IsSecure { get; }
        double SignalStrength { get; }
    }
}
