using System;

namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
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