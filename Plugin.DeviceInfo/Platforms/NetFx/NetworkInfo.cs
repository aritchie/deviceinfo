using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;


namespace Plugin.DeviceInfo
{

    public class NetworkInfo : INetworkInfo
    {
        public bool IsInternetAvailable => false; // NetworkInterface.GetIsNetworkAvailable()
        public IObservable<IWifiScanResult> ScanForWifiNetworks() => Observable.Empty<IWifiScanResult>();
        public IObservable<Unit> ConnectToWifi(string ssid, string password) => Observable.Empty<Unit>();


        public NetworkReachability InternetReachability => NetworkReachability.Other;
        public string CellularNetworkCarrier => null;

        public string IpAddress => Dns
                .GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(x =>
                    x.AddressFamily == AddressFamily.InterNetwork ||
                    x.AddressFamily == AddressFamily.InterNetworkV6
                )?
                .ToString();


        public string WifiSsid => null;


        IObservable<NetworkReachability> statusOb;
        public IObservable<NetworkReachability> WhenStatusChanged()
        {
            this.statusOb = this.statusOb ?? Observable.Create<NetworkReachability>(async ob =>
            {
                var current = await this.IsReachable();
                ob.OnNext(current
                    ? NetworkReachability.Other
                    : NetworkReachability.NotReachable);

                var handler = new NetworkAddressChangedEventHandler(async (sender, args) =>
                {
                    var reachable = await this.IsReachable();
                    if (current != reachable)
                    {
                        current = reachable;
                        ob.OnNext(current
                            ? NetworkReachability.Other
                            : NetworkReachability.NotReachable);
                    }
                });
                NetworkChange.NetworkAddressChanged += handler;
                return () => NetworkChange.NetworkAddressChanged -= handler;
            })
            .Replay(1)
            .RefCount();

            return this.statusOb;
        }


        async Task<bool> IsReachable()
        {
            try
            {
                var ping = new Ping();

                var result = await ping.SendPingAsync("google.com", 3000);
                return (result.Status == IPStatus.Success);
            }
            catch
            {
                return false;
            }
        }
    }
}
