using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.Telephony;


namespace Acr.DeviceInfo
{

    public class ConnectivityImpl : AbstractConnectivityImpl
    {

        //public ConnectivityImpl()
        //{
        //    ConnectivityBroadcastReceiver.StatusChanged = (sender, args) => this.InternetReachability = ConnectivityBroadcastReceiver.ReachableStatus;
        //    ConnectivityBroadcastReceiver.Register();
        //}


        //protected override string GetIpAddress()
        //{
        //    return Dns
        //        .GetHostEntry(Dns.GetHostName())
        //        .AddressList
        //        .FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?
        //        .ToString();
        //}


        //protected override string GetNetworkCarrier()
        //{
        //    var tel = Application.Context.ApplicationContext.GetSystemService(Context.TelephonyService) as TelephonyManager;
        //    var opName = tel?.NetworkOperatorName;
        //    return opName;
        //}


        //protected override string GetWifiSsid()
        //{
        //    //<uses-permission android:name="android.permission.ACCESS_WIFI_STATE"></uses-permission>
        //    var wifiManager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
        //    var ssid = wifiManager.ConnectionInfo?.SSID;
        //    return ssid;
        //}
        public override bool IsInternetAvailable { get; }
        public override ConnectionStatus InternetReachability { get; }
        public override string CellularNetworkCarrier { get; }
        public override string IpAddress { get; }
        public override string WifiSsid { get; }
        protected override void StartMonitoringConnection()
        {
            throw new NotImplementedException();
        }

        protected override void StopMonitoringConnection()
        {
            throw new NotImplementedException();
        }
    }
}