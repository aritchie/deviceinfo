using System;
using CoreTelephony;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
            using (var info = new CTTelephonyNetworkInfo())
                this.CellularNetworkCarrier = info.SubscriberCellularProvider?.CarrierName;

            Reachability.ReachabilityChanged += (sender, args) => this.SetConnectivityState();
            this.SetConnectivityState();
        }


        void SetConnectivityState() {
            var internet = Reachability.InternetConnectionStatus();
            this.IsInternetAvailable = (internet != NetworkStatus.NotReachable);
            this.IsWifi = (internet == NetworkStatus.ReachableViaWiFiNetwork);
            this.IsCellular = (internet == NetworkStatus.ReachableViaCarrierDataNetwork);
        }
    }
}