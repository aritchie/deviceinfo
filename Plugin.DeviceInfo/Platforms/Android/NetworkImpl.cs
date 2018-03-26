using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive;
using System.Reactive.Linq;
using Acr;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Net.Wifi;
using Android.Telephony;


namespace Plugin.DeviceInfo
{
    public class NetworkImpl : INetwork
    {
        /*
WifiConfiguration wifiConfig = new WifiConfiguration();
wifiConfig.SSID = String.format("\"%s\"", ssid);
wifiConfig.preSharedKey = String.format("\"%s\"", key);

WifiManager wifiManager = (WifiManager)getSystemService(WIFI_SERVICE);
//remember id
int netId = wifiManager.addNetwork(wifiConfig);
wifiManager.disconnect();
wifiManager.enableNetwork(netId, true);
wifiManager.reconnect();


        conf.wepKeys[0] = "\"" + networkPass + "\"";
        conf.wepTxKeyIndex = 0;
        conf.allowedKeyManagement.set(WifiConfiguration.KeyMgmt.NONE);
        conf.allowedGroupCiphers.set(WifiConfiguration.GroupCipher.WEP40);
        For WPA network you need to add passphrase like this:

        conf.preSharedKey = "\""+ networkPass +"\"";
        For Open network you need to do this:

        conf.allowedKeyManagement.set(WifiConfiguration.KeyMgmt.NONE);
        Then, you need to add it to Android wifi manager settings:

        WifiManager wifiManager = (WifiManager)context.getSystemService(Context.WIFI_SERVICE);
        wifiManager.addNetwork(conf);
        And finally, you might need to enable it, so Android connects to it:

        List<WifiConfiguration> list = wifiManager.getConfiguredNetworks();
        for( WifiConfiguration i : list ) {
            if(i.SSID != null && i.SSID.equals("\"" + networkSSID + "\"")) {
                 wifiManager.disconnect();
                 wifiManager.enableNetwork(i.networkId, true);
                 wifiManager.reconnect();

                 break;
            }
         }
        UPD: In case of WEP, if your password is in hex, you do not need to surround it with quotes.
         */
        //public IObservable<IWifiScanResult> ScanForWifiNetworks() => Observable.Create<IWifiScanResult>(ob =>
        //{
        //    this.Wifi.StartScan();
        //    var sr = this.Wifi.ScanResults[0];
        //    //sr.Ssid;
        //    //sr.Level;
        //    return () => { };
        //});


        //public IObservable<Unit> ConnectToWifi(string ssid, string password)
        //{
        //    var cfg = new WifiConfiguration
        //    {
        //        Ssid = ssid
        //    };
        //    //cfg.WepKeys
        //    this.Wifi.AddNetwork(cfg);
        //    throw new NotImplementedException();
        //}


        public NetworkReachability InternetReachability
        {
            get
            {
                var an = this.Connectivity.ActiveNetworkInfo;
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


        public string CellularNetworkCarrier => this.Tel.NetworkOperatorName;


        public string IpAddress => Dns
            .GetHostEntry(Dns.GetHostName())
            .AddressList
            .FirstOrDefault(x =>
                x.AddressFamily == AddressFamily.InterNetwork ||
                x.AddressFamily == AddressFamily.InterNetworkV6
            )?
            .ToString();


        //<uses-permission android:name="android.permission.ACCESS_WIFI_STATE"></uses-permission>
        public string WifiSsid => this.Wifi.ConnectionInfo?.SSID;

        public IObservable<NetworkReachability> WhenStatusChanged() => AndroidObservables
            .WhenIntentReceived(ConnectivityManager.ConnectivityAction)
            .Select(intent => this.InternetReachability);


        WifiManager wifiMgr;
        WifiManager Wifi
        {
            get
            {
                if (this.wifiMgr == null || this.wifiMgr.Handle == IntPtr.Zero)
                    this.wifiMgr = (WifiManager) Application.Context.GetSystemService(Context.WifiService);

                return this.wifiMgr;
            }
        }


        TelephonyManager telMgr;
        TelephonyManager Tel
        {
            get
            {
                if (this.telMgr == null || this.telMgr.Handle == IntPtr.Zero)
                    this.telMgr = (TelephonyManager) Application.Context.ApplicationContext.GetSystemService(Context.TelephonyService);
                return this.telMgr;
            }
        }


        ConnectivityManager connectivityMgr;
        ConnectivityManager Connectivity
        {
            get
            {
                if (this.connectivityMgr == null || this.connectivityMgr.Handle == IntPtr.Zero)
                    this.connectivityMgr = (ConnectivityManager) Application.Context.GetSystemService(Context.ConnectivityService);

                return this.connectivityMgr;
            }
        }
    }
}