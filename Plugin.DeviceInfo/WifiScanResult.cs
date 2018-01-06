using System;


namespace Plugin.DeviceInfo
{
    public class WifiScanResult : IWifiScanResult
    {
        public string Ssid { get; set; }
        public bool IsSecure { get; set; }
        public double SignalStrength { get; set; }
    }
}
