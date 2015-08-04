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

            switch (internet) {

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
    }
}