using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using Acr;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.Telephony;


namespace Plugin.DeviceInfo
{
    public class NetworkInfo : INetworkInfo
    {
        public NetworkReachability InternetReachability
        {
            get
            {
                var an = this.Connectivity().ActiveNetworkInfo;
                if (an == null || !an.IsConnected)
                    return NetworkReachability.NotReachable;

                switch (an.Type)
                {

                    case ConnectivityType.Wimax:
                    case ConnectivityType.Wifi:
                        return NetworkReachability.Wifi;

                    case ConnectivityType.Mobile:
                        return NetworkReachability.Cellular;

                    default:
                        return NetworkReachability.Other;
                }
            }
        }


        public string CellularNetworkCarrier => this.Tel().NetworkOperatorName;


        public string IpAddress => Dns
            .GetHostEntry(Dns.GetHostName())
            .AddressList
            .FirstOrDefault(x =>
                x.AddressFamily == AddressFamily.InterNetwork ||
                x.AddressFamily == AddressFamily.InterNetworkV6
            )?
            .ToString();


        //<uses-permission android:name="android.permission.ACCESS_WIFI_STATE"></uses-permission>
        public string WifiSsid => this.Wifi().ConnectionInfo?.SSID;

        public IObservable<NetworkReachability> WhenStatusChanged() => AndroidObservables
            .WhenIntentReceived(ConnectivityManager.ConnectivityAction)
            .Select(intent => this.InternetReachability);


        WifiManager wifiMgr;
        WifiManager Wifi()
        {
            if (this.wifiMgr == null || this.wifiMgr.Handle == IntPtr.Zero)
                this.wifiMgr = (WifiManager)Application.Context.GetSystemService(Context.WifiService);

            return this.wifiMgr;
        }



        TelephonyManager telMgr;
        TelephonyManager Tel()
        {
            if (this.telMgr == null || this.telMgr.Handle == IntPtr.Zero)
                this.telMgr = (TelephonyManager)Application.Context.ApplicationContext.GetSystemService(Context.TelephonyService);
            return this.telMgr;
        }


        ConnectivityManager connectivityMgr;
        ConnectivityManager Connectivity()
        {
            if (this.connectivityMgr == null || this.connectivityMgr.Handle == IntPtr.Zero)
                this.connectivityMgr = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);

            return this.connectivityMgr;
        }
    }
}