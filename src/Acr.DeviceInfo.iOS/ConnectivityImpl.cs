using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reactive.Linq;
using SystemConfiguration;
using CoreTelephony;
using Foundation;


namespace Acr.DeviceInfo
{
    public class ConnectivityImpl : IConnectivity
    {
        public NetworkReachability InternetReachability
        {
            get
            {
                var internet = Reachability.InternetConnectionStatus();

                switch (internet)
                {
                    case NetworkStatus.NotReachable:
                        return NetworkReachability.NotReachable;

                    case NetworkStatus.ReachableViaCarrierDataNetwork:
                        return NetworkReachability.Cellular;

                    case NetworkStatus.ReachableViaWiFiNetwork:
                        return NetworkReachability.Wifi;

                    default:
                        return NetworkReachability.Other;
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
            .Where(x =>
                x.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                x.NetworkInterfaceType == NetworkInterfaceType.Wireless80211
            )
            .SelectMany(x => x.GetIPProperties().UnicastAddresses)
            .Where(x => x.Address.AddressFamily == AddressFamily.InterNetwork)
            .Select(x => x.Address.ToString())
            .FirstOrDefault();
        //public string IpAddress => Dns
        //    .GetHostEntry(Dns.GetHostName())
        //    .AddressList
        //    .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?
        //    .ToString

        public string WifiSsid
        {
            get
            {
                NSDictionary values;
                var status = CaptiveNetwork.TryCopyCurrentNetworkInfo("en0", out values);
                if (status == StatusCode.NoKey)
                    return null;

                var ssid = values[CaptiveNetwork.NetworkInfoKeySSID];
                return ssid.ToString();
            }
        }


        public IObservable<NetworkReachability> WhenStatusChanged()
        {
            return Observable.Create<NetworkReachability>(ob =>
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