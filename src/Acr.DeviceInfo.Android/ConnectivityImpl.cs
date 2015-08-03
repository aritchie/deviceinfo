using System;
using Android.App;
using Android.Content;
using Android.Telephony;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
            var tel = Application.Context.ApplicationContext.GetSystemService(Context.TelephonyService) as TelephonyManager;
            this.CellularNetworkCarrier = tel?.NetworkOperatorName;

            ConnectivityBroadcastReceiver.Register();
            ConnectivityBroadcastReceiver.StatusChanged = (sender, args) => {
                this.IsInternetAvailable = ConnectivityBroadcastReceiver.IsInternetAvailable;
                this.IsWifi = ConnectivityBroadcastReceiver.IsInternetWifi;
                this.IsCellular = ConnectivityBroadcastReceiver.IsInternetCellular;
            };
            // TODO: force run?
        }
    }
}