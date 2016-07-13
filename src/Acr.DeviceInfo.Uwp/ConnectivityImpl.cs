using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reactive.Linq;
using Windows.Networking.Connectivity;


namespace Acr.DeviceInfo
{
    public class ConnectivityImpl : IConnectivity
    {
        public string CellularNetworkCarrier { get; } = null;


        public string IpAddress => NetworkInformation
                .GetHostNames()
                .Last()
                .DisplayName;

        public NetworkReachability InternetReachability
        {
            get
            {
                var avail = NetworkInterface.GetIsNetworkAvailable();

                if (!avail)
                    return NetworkReachability.NotReachable;

                var profile = NetworkInformation.GetInternetConnectionProfile();
                switch (profile.NetworkAdapter.IanaInterfaceType)
                {
                    case 71:
                        return NetworkReachability.Wifi;

                    case 243:
                    case 244:
                        return NetworkReachability.Cellular;

                    default:
                        return NetworkReachability.Other;
                }
            }
        }


        public string WifiSsid
        {
            get
            {
                var profile = NetworkInformation.GetInternetConnectionProfile();
                if (!profile.IsWlanConnectionProfile)
                    return null;

                return profile.WlanConnectionProfileDetails.GetConnectedSsid();
            }
        }


        public IObservable<NetworkReachability> WhenStatusChanged()
        {
            return Observable.Create<NetworkReachability>(ob =>
            {
                var handler = new NetworkStatusChangedEventHandler(sender => ob.OnNext(this.InternetReachability));
                NetworkInformation.NetworkStatusChanged += handler;
                return () => NetworkInformation.NetworkStatusChanged -= handler;
            });
        }
    }
}
