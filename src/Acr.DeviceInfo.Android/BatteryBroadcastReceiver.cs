using System;
using Android;
using Android.App;
using Android.Content;
using Android.OS;


namespace Acr.DeviceInfo {

    [BroadcastReceiver(Enabled = true)]
    public class BatteryBroadcastReceiver : BroadcastReceiver {

        public static EventHandler StatusChanged;
        public static bool IsCharging { get; private set; }
        public static int Percentage { get; private set; }


        public override void OnReceive(Context context, Intent intent) {
            ProcessIntent(intent);
        }


        static void ProcessIntent(Intent intent) {
            switch (intent.Action) {
                case Intent.ActionPowerConnected:

                    break;

                case Intent.ActionBatteryChanged:
                    var level = intent.GetIntExtra(BatteryManager.ExtraLevel, -1);
                    var scale = intent.GetIntExtra(BatteryManager.ExtraScale, -1);
                    var status = intent.GetIntExtra(BatteryManager.ExtraStatus, -1);

                    IsCharging = (status == (int)BatteryStatus.Charging || status == (int)BatteryStatus.Full);
                    Percentage = (int)Math.Floor(level * 100D / scale);
                    break;
            }
            StatusChanged?.Invoke(null, EventArgs.Empty);
        }


        public static void Register() {
            // this permission does not seem to set!
            //Manifest.Permission.DevicePower
            //if (!Utils.CheckPermission(Manifest.Permission.BatteryStats))
            //    return;
            try {
                using (var filter = CreateFilter())
                    using (var intent = Application.Context.RegisterReceiver(null, filter))
                        ProcessIntent(intent);

                Application.Context.RegisterReceiver(new BatteryBroadcastReceiver(), CreateFilter());
            }
            catch (Exception ex) {
                Console.WriteLine($"Could not register for battery events.  Ensure you have {Manifest.Permission.BatteryStats} and {Manifest.Permission.DevicePower} to your android application. Exception: {ex}");
            }
        }


        static IntentFilter CreateFilter() {
            var filter = new IntentFilter(Intent.ActionBatteryChanged);
            filter.AddAction(Intent.ActionPowerConnected);
            filter.AddAction(Intent.ActionPowerDisconnected);
            return filter;
        }
    }
}