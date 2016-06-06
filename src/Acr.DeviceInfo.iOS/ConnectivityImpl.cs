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

        public override bool IsInternetAvailable => this.InternetReachability != ConnectionStatus.NotReachable;


        ConnectionStatus status;
        public override ConnectionStatus InternetReachability { get; }


        public override string CellularNetworkCarrier
        {
            get
            {
                using (var info = new CTTelephonyNetworkInfo())
                    return info.SubscriberCellularProvider?.CarrierName;
            }
        }


        public override string IpAddress
        {
            get
            {
                return Dns
                    .GetHostEntry(Dns.GetHostName())
                    .AddressList
                    .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?
                    .ToString();
            }
        }


        public override string WifiSsid
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


        protected override void StartMonitoringConnection()
        {
            Reachability.ReachabilityChanged += this.SetConnectivityState;
        }


        protected override void StopMonitoringConnection()
        {
            Reachability.ReachabilityChanged -= this.SetConnectivityState;
        }


        protected virtual void SetConnectivityState(object sender, EventArgs args)
        {
            var internet = Reachability.InternetConnectionStatus();

            switch (internet)
            {

                case NetworkStatus.NotReachable:
                    this.status = ConnectionStatus.NotReachable;
                    break;

                case NetworkStatus.ReachableViaCarrierDataNetwork:
                    this.status = ConnectionStatus.ReachableViaCellular;
                    break;

                case NetworkStatus.ReachableViaWiFiNetwork:
                    this.status = ConnectionStatus.ReachableViaWifi;
                    break;
            }
        }
    }
}