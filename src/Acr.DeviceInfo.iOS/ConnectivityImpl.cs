using System;
using CoreTelephony;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
            using (var info = new CTTelephonyNetworkInfo())
                this.CellularNetworkCarrier = info.SubscriberCellularProvider.CarrierName;

            Reachability.ReachabilityChanged += (sender, args) => this.SetConnectivityState();
            this.SetConnectivityState();
        }

        void SetConnectivityState() {
            var internet = Reachability.InternetConnectionStatus();
            var wifi = Reachability.LocalWifiConnectionStatus();

            //internet != NetworkStatus.NotReachable
            //wifi != NetworkStatus.NotReachable
        }
    }
}