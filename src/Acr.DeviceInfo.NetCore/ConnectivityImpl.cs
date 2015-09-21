using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        protected override string GetIpAddress() {
            return Dns
                .GetHostEntry(Dns.GetHostName())
                .AddressList
                .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?
                .ToString();
        }
    }
}
