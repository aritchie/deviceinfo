using System;
using Android;
using Android.App;
using Android.Content;
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
        }


        static void SetState() {
            var mgr = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            if (mgr.ActiveNetworkInfo == null || !mgr.ActiveNetworkInfo.IsConnected)
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
            StatusChanged?.Invoke(null, EventArgs.Empty);
        }


        public static void Register() {
            //if (!Utils.CheckPermission(Manifest.Permission.Internet))
            //    return;

            if (!Utils.CheckPermission(Manifest.Permission.AccessNetworkState))
                return;

            Application.Context.RegisterReceiver(new ConnectivityBroadcastReceiver(), new IntentFilter(ConnectivityManager.ConnectivityAction));
            SetState();
        }
    }
}