using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Android.App;
using Android.Content;
using Android.Telephony;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
            var tel = Application.Context.ApplicationContext.GetSystemService(Context.TelephonyService) as TelephonyManager;
            this.CellularNetworkCarrier = tel?.NetworkOperatorName;

            ConnectivityBroadcastReceiver.StatusChanged = (sender, args) => this.InternetReachability = ConnectivityBroadcastReceiver.ReachableStatus;
            ConnectivityBroadcastReceiver.Register();
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