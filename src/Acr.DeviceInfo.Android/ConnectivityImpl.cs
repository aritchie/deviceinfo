using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using Acr.DeviceInfo.Internals;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.Telephony;


namespace Acr.DeviceInfo
{

    public class ConnectivityImpl : IConnectivity
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
        public bool IsInternetAvailable { get; }
        public ConnectionStatus InternetReachability { get; }
        public string CellularNetworkCarrier { get; }
        public string IpAddress { get; }
        public string WifiSsid { get; }
        public IObservable<ConnectionStatus> WhenStatusChanged()
        {
            return AndroidObservables
                .WhenIntentReceived(ConnectivityManager.ConnectivityAction)
                .Select(intent =>
                {
                    return ConnectionStatus.Offline;
                    //var mgr = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
                    //if (mgr.ActiveNetworkInfo == null || !mgr.ActiveNetworkInfo.IsConnected)
                    //    ReachableStatus = ConnectionStatus.NotReachable;

                    //else
                    //{
                    //    switch (mgr.ActiveNetworkInfo.Type)
                    //    {

                    //        case ConnectivityType.Wimax:
                    //        case ConnectivityType.Wifi:
                    //            ReachableStatus = ConnectionStatus.ReachableViaWifi;
                    //            break;

                    //        case ConnectivityType.Mobile:
                    //            ReachableStatus = ConnectionStatus.ReachableViaCellular;
                    //            break;

                    //        default:
                    //            ReachableStatus = ConnectionStatus.ReachableViaOther;
                    //            break;
                    //    }
                    //}
                });
        }
    }
}