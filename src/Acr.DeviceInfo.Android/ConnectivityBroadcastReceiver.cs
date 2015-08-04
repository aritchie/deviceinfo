using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;


namespace Acr.DeviceInfo {

    [BroadcastReceiver(Enabled = true)]
    public class ConnectivityBroadcastReceiver : BroadcastReceiver {

        public static ConnectionStatus ReachableStatus { get; private set; }
        public static EventHandler StatusChanged;


        public override void OnReceive(Context context, Intent intent) {
            if (intent.Action != ConnectivityManager.ConnectivityAction)
                return;

            //var flag = !intent.GetBooleanExtra(ConnectivityManager.ExtraNoConnectivity, false);
            SetState();
            StatusChanged?.Invoke(this, null);
        }


        static void SetState() {
            var mgr = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            if (mgr.ActiveNetworkInfo.IsConnected)
                ReachableStatus = ConnectionStatus.NotReachable;

            else {
                switch (mgr.ActiveNetworkInfo.Type) {

                    case ConnectivityType.Wimax:
                    case ConnectivityType.Wifi:
                        ReachableStatus = ConnectionStatus.ReachableViaWifi;
                        break;

                    case ConnectivityType.Mobile:
                        ReachableStatus = ConnectionStatus.ReachableViaCellular;
                        break;

                    default:
                        ReachableStatus = ConnectionStatus.ReachableViaOther;
                        break;
                }
            }
        }


        public static void Register() {
            var result = Application.Context.CheckCallingOrSelfPermission("android.permission.BATTERY_STATS");
            if (result != Permission.Granted) {
                Console.WriteLine("android.permission.ACCESS_NETWORK_STATE was not granted in your manifest");
                return;
            }
            Application.Context.RegisterReceiver(new ConnectivityBroadcastReceiver(), new IntentFilter(ConnectivityManager.ConnectivityAction));
            SetState();
        }
    }
}