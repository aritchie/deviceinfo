using System;
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
    }
}