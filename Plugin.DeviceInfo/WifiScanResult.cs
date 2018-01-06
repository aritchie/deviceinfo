using System;


namespace Plugin.DeviceInfo
{
    internal class WifiScanResult : IWifiScanResult
    {
        public string Ssid { get; set; }
        public bool IsSecure { get; set; }
        public double SignalStrength { get; set; }
    }
}
