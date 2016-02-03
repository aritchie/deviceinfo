using System;
using System.Linq;
using System.Net.NetworkInformation;
using Windows.Networking.Connectivity;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
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
            return NetworkInformation
                .GetHostNames()
                .Last()
                .DisplayName;
        }


        protected override string GetNetworkCarrier()
        {
            return null;
        }


        protected override string GetWifiSsid()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            if (!profile.IsWlanConnectionProfile)
                return null;

            return profile.WlanConnectionProfileDetails.GetConnectedSsid();
        }
    }
}
