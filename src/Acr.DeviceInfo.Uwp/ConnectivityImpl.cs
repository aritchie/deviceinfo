using System;
using System.Net.NetworkInformation;
using Windows.Networking.Connectivity;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
            this.CellularNetworkCarrier = String.Empty;
            NetworkInformation.NetworkStatusChanged += args => this.SetState();
            this.SetState();
        }


        void SetState() {
            var avail = NetworkInterface.GetIsNetworkAvailable();

            if (avail)
                this.InternetReachability = ConnectionStatus.NotReachable;
            else {
                var profile = NetworkInformation.GetInternetConnectionProfile();

                switch (profile.NetworkAdapter.IanaInterfaceType) {
                    case 71:
                        this.InternetReachability = ConnectionStatus.ReachableViaWifi;
                        break;

                    case 243:
                    case 244:
                        this.InternetReachability = ConnectionStatus.ReachableViaCellular;
                        break;

                    default:
                        this.InternetReachability = ConnectionStatus.ReachableViaOther;
                        break;
                }
                //switch (NetworkInterface.NetworkInterfaceType) {
                //    case NetworkInterfaceType.Wireless80211:
                //        this.InternetReachability = ConnectionStatus.ReachableViaWifi;
                //        break;

                //    case NetworkInterfaceType.MobileBroadbandCdma:
                //    case NetworkInterfaceType.MobileBroadbandGsm:
                //        this.InternetReachability = ConnectionStatus.ReachableViaCellular;
                //        break;
                //}
            }
        }


        protected override string GetIpAddress() {
            return Dns
                .GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?
                .ToString();
        }
    }
}
