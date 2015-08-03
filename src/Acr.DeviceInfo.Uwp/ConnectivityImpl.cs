using System;
using Windows.Networking.Connectivity;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
            NetworkInformation.NetworkStatusChanged += args => {
                var profile = NetworkInformation.GetInternetConnectionProfile();
                //this.IsAppInBackground = (profile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess);
                //profile.IsWlanConnectionProfile;
            };
        }
    }
}
