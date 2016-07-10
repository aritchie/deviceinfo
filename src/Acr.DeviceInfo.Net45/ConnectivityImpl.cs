using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;


namespace Acr.DeviceInfo
{

    public class ConnectivityImpl : IConnectivity
    {
        public bool IsInternetAvailable => false; // NetworkInterface.GetIsNetworkAvailable()
        public ConnectionStatus InternetReachability => ConnectionStatus.Other;
        public string CellularNetworkCarrier => null;

        public string IpAddress => Dns
                .GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?
                .ToString();

        public string WifiSsid => null;


        public IObservable<ConnectionStatus> WhenStatusChanged()
        {
            return Observable.Empty<ConnectionStatus>();
        }
    }
}
