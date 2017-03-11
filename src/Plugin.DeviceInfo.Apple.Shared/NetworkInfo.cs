using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reactive.Linq;
using SystemConfiguration;
using CoreTelephony;
using Foundation;


namespace Plugin.DeviceInfo
{
    public class ConnectivityImpl : IConnectivity
    {
        public SystemConfiguration.NetworkReachability InternetReachability
        {
            get
            {
                var internet = Reachability.InternetConnectionStatus();

                switch (internet)
                {
                    case NetworkStatus.NotReachable:
                        return SystemConfiguration.NetworkReachability.NotReachable;

                    case NetworkStatus.ReachableViaCarrierDataNetwork:
                        return SystemConfiguration.NetworkReachability.Cellular;

                    case NetworkStatus.ReachableViaWiFiNetwork:
                        return SystemConfiguration.NetworkReachability.Wifi;

                    default:
                        return SystemConfiguration.NetworkReachability.Other;
                }
            }
        }

        public string CellularNetworkCarrier
        {
            get
            {
                using (var info = new CTTelephonyNetworkInfo())
                    return info.SubscriberCellularProvider?.CarrierName;
            }
        }


        public string IpAddress => NetworkInterface
            .GetAllNetworkInterfaces()
            .FirstOrDefault(x => x.Name.Equals("en0", StringComparison.InvariantCultureIgnoreCase))?
            .GetIPProperties()
            .UnicastAddresses
            .FirstOrDefault(x => x.Address.AddressFamily == AddressFamily.InterNetwork)?
            .Address?
            .ToString();
        

        public string WifiSsid
        {
            get
            {
                NSDictionary values;
                var status = CaptiveNetwork.TryCopyCurrentNetworkInfo("en0", out values);
                if (status == StatusCode.NoKey || values == null || !values.ContainsKey(CaptiveNetwork.NetworkInfoKeySSID))
                    return null;

                var ssid = values[CaptiveNetwork.NetworkInfoKeySSID];
                return ssid?.ToString();
            }
        }


        public IObservable<SystemConfiguration.NetworkReachability> WhenStatusChanged()
        {
            return Observable.Create<SystemConfiguration.NetworkReachability>(ob =>
            {
                var handler = new EventHandler((sender, args) =>
                    ob.OnNext(this.InternetReachability)
                );
                Reachability.ReachabilityChanged += handler;
                return () => Reachability.ReachabilityChanged -= handler;
            });
        }
    }
}