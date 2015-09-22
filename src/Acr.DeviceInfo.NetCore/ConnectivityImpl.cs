using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Timers;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {
        readonly Timer timer;


        public ConnectivityImpl() {
            this.timer = new Timer(3000);
            this.timer.Elapsed += (sender, args) => {
                if (NetworkInterface.GetIsNetworkAvailable())
                    this.InternetReachability = ConnectionStatus.ReachableViaOther;
            };
            this.timer.Start();
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
