using System;
using Windows.Networking.Connectivity;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
            this.CellularNetworkCarrier = "TODO";
            NetworkInformation.NetworkStatusChanged += args => this.SetState();
            this.SetState();
        }


        void SetState() {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            //this.IsAppInBackground = (profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
            //profile.IsWlanConnectionProfile;
        }
    }
}
