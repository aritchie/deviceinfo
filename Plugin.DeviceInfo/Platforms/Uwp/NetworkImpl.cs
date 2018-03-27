using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reactive.Linq;
using Windows.Networking.Connectivity;


namespace Plugin.DeviceInfo
{
    public class NetworkImpl : INetwork
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
                if (profile == null)
                    return NetworkReachability.NotReachable;

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
                if (profile == null || !profile.IsWlanConnectionProfile)
                    return null;

                return profile.WlanConnectionProfileDetails.GetConnectedSsid();
            }
        }


        //<DeviceCapability Name="wifiControl" />
        //public IObservable<Unit> ConnectToWifi(string ssid, string password) => Observable.FromAsync(async ct =>
        //{
        //    var wifiAdapter = await this.GetWifiAdapter();
        //    PasswordCredential credentials = null;
        //    if (password != null)
        //        credentials = new PasswordCredential { Password = password };

        //    var network = await this.ScanAvailableNetworks()
        //        .Where(x => x.Ssid.Equals(ssid, StringComparison.CurrentCultureIgnoreCase))
        //        .Take(1)
        //        .ToTask(ct);

        //    await wifiAdapter.ConnectAsync(network, WiFiReconnectionKind.Automatic, credentials);
        //});


        //public IObservable<IWifiScanResult> ScanForWifiNetworks() => this
        //    .ScanAvailableNetworks()
        //    .Select(x => new WifiScanResult
        //    {
        //        Ssid = x.Ssid,
        //        SignalStrength = x.SignalBars,
        //        IsSecure = false
            //});


        public IObservable<NetworkReachability> WhenStatusChanged() => Observable.Create<NetworkReachability>(ob =>
        {
            var handler = new NetworkStatusChangedEventHandler(sender => ob.OnNext(this.InternetReachability));
            NetworkInformation.NetworkStatusChanged += handler;
            return () => NetworkInformation.NetworkStatusChanged -= handler;
        });


        //IObservable<WiFiAvailableNetwork> wifiOb;


        //protected virtual IObservable<WiFiAvailableNetwork> ScanAvailableNetworks()
        //{
        //    this.wifiOb = this.wifiOb ?? Observable.Create<WiFiAvailableNetwork>(async ob =>
        //    {
        //        var wifiAdapter = await this.GetWifiAdapter();
        //        wifiAdapter.AvailableNetworksChanged += (sender, args) =>
        //        {
        //            // TODO
        //            //wifiAdapter.NetworkReport.AvailableNetworks
        //        };
        //        await wifiAdapter.ScanAsync();
        //    })
        //    .Publish();

        //    return this.wifiOb;
        //}


        //protected virtual async Task<WiFiAdapter> GetWifiAdapter()
        //{
        //    var access = await WiFiAdapter.RequestAccessAsync();
        //    if (access != WiFiAccessStatus.Allowed)
        //        throw new Exception("WiFiAccessStatus not allowed");

        //    var results = await DeviceInformation.FindAllAsync(WiFiAdapter.GetDeviceSelector());
        //    if (!results.Any())
        //        return null;

        //    var wifiAdapter = await WiFiAdapter.FromIdAsync(results[0].Id);
        //    return wifiAdapter;
        //}
    }
}
