using System;
using Android.App;
using Android.Content;
using Android.Net;


namespace Acr.DeviceInfo {

    [BroadcastReceiver(Enabled = true)]
    public class ConnectivityBroadcastReceiver : BroadcastReceiver {

        public static bool IsInternetAvailable { get; private set; }
        public static bool IsInternetWifi { get; private set; }
        public static bool IsInternetCellular { get; private set; }
        public static EventHandler StatusChanged;


        public override void OnReceive(Context context, Intent intent) {
            if (intent.Action != ConnectivityManager.ConnectivityAction)
                return;

            IsInternetAvailable = !intent.GetBooleanExtra(ConnectivityManager.ExtraNoConnectivity, false);
            //if (IsInternetAvailable) {
            //    IsInternetWifi = (active.Type == ConnectivityType.Wifi || active.Type == ConnectivityType.Wimax);
            //    IsInternetCellular = (active.Type == ConnectivityType.Mobile);
            //}
            //else {
            //    IsInternetWifi = false;
            //    IsInternetCellular = false;
            //}
            StatusChanged?.Invoke(this, null);
        }


        public static void Register() {
            Application.Context.RegisterReceiver(new ConnectivityBroadcastReceiver(), new IntentFilter(ConnectivityManager.ConnectivityAction));
        }
    }
}