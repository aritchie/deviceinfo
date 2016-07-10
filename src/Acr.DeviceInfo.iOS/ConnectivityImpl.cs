using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using SystemConfiguration;
using CoreTelephony;
using Foundation;


namespace Acr.DeviceInfo
{
    public class ConnectivityImpl : IConnectivity
    {

        //void SetConnectivityState() {
        //    var internet = Reachability.InternetConnectionStatus();

        //    switch (internet) {

        //        case NetworkStatus.NotReachable:
        //            this.InternetReachability = ConnectionStatus.NotReachable;
        //            break;

        //        case NetworkStatus.ReachableViaCarrierDataNetwork:
        //            this.InternetReachability = ConnectionStatus.ReachableViaCellular;
        //            break;

        //        case NetworkStatus.ReachableViaWiFiNetwork:
        //            this.InternetReachability = ConnectionStatus.ReachableViaWifi;
        //            break;
        //    }
        //}


        //protected override string GetNetworkCarrier()
        //{
        //    using (var info = new CTTelephonyNetworkInfo())
        //        return info.SubscriberCellularProvider?.CarrierName;
        //}


        //protected override string GetWifiSsid()
        //{
        //    NSDictionary values;
        //    var status = CaptiveNetwork.TryCopyCurrentNetworkInfo("en0", out values);
        //    if (status == StatusCode.NoKey)
        //        return null;

        //    var ssid = values[CaptiveNetwork.NetworkInfoKeySSID];
        //    return ssid.ToString();
        //}
        public bool IsInternetAvailable { get; }
        public ConnectionStatus InternetReachability { get; }
        public string CellularNetworkCarrier { get; }


        public string IpAddress => Dns
            .GetHostEntry(Dns.GetHostName())
            .AddressList
            .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?
            .ToString();


        public string WifiSsid { get; }


        public IObservable<ConnectionStatus> WhenStatusChanged()
        {
            return Observable.Create<ConnectionStatus>(ob =>
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