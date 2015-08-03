using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
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
            var level = intent.GetIntExtra(BatteryManager.ExtraLevel, -1);
            var scale = intent.GetIntExtra(BatteryManager.ExtraScale, -1);
            var status = intent.GetIntExtra(BatteryManager.ExtraStatus, -1);

            IsCharging = (status == (int)BatteryStatus.Charging || status == (int)BatteryStatus.Full);
            Percentage = (int)Math.Floor(level * 100D / scale);
        }


        public static bool Register() {
            var result = Application.Context.CheckCallingOrSelfPermission("android.permission.BATTERY_STATS");
            if (result != Permission.Granted) {
                Console.WriteLine("android.permission.BATTERY_STATS was not granted in your manifest");
                return false;
            }
            Application.Context.RegisterReceiver(new BatteryBroadcastReceiver(), new IntentFilter(Intent.ActionBatteryChanged));
            using (var filter = new IntentFilter(Intent.ActionBatteryChanged))
                using (var intent = Application.Context.RegisterReceiver(null, filter))
                    ProcessIntent(intent);

            return true;
        }
    }
}