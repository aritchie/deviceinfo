using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using SystemConfiguration;
using CoreTelephony;
using Foundation;


namespace Acr.DeviceInfo
{

    public class ConnectivityImpl : AbstractConnectivityImpl
    {

        public ConnectivityImpl()
        {
            Reachability.ReachabilityChanged += (sender, args) => this.SetConnectivityState();
            this.SetConnectivityState();
        }


        void SetConnectivityState()
        {
            var internet = Reachability.InternetConnectionStatus();

            switch (internet)
            {

                case NetworkStatus.NotReachable:
                    this.InternetReachability = ConnectionStatus.NotReachable;
                    break;

                case NetworkStatus.ReachableViaCarrierDataNetwork:
                    this.InternetReachability = ConnectionStatus.ReachableViaCellular;
                    break;

                case NetworkStatus.ReachableViaWiFiNetwork:
                    this.InternetReachability = ConnectionStatus.ReachableViaWifi;
                    break;
            }
        }


        protected override string GetIpAddress()
        {
            return Dns
                .GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?
                .ToString();
        }


        protected override string GetNetworkCarrier()
        {
            using (var info = new CTTelephonyNetworkInfo())
                return info.SubscriberCellularProvider?.CarrierName;
        }


        protected override string GetWifiSsid()
        {
            NSDictionary values;
            var status = CaptiveNetwork.TryCopyCurrentNetworkInfo("en0", out values);
            if (status == StatusCode.NoKey)
                return null;

            var ssid = values[CaptiveNetwork.NetworkInfoKeySSID];
            return ssid.ToString();
        }
    }
}