using System;
using System.Globalization;
using Android.App;
using Android.Content;
using Java.Util;


namespace Acr.DeviceInfo {

    public class LocaleBroadcastReceiver : BroadcastReceiver {

        public static CultureInfo Current { get; private set; }
        public static EventHandler Changed;


        public static void Register() {
            Application.Context.RegisterReceiver(new LocaleBroadcastReceiver(), new IntentFilter(Intent.ActionLocaleChanged));
            SetLocale();
        }


        public override void OnReceive(Context context, Intent intent) {
            SetLocale();
        }


        static void SetLocale() {
            var value = Locale.Default.ToString().Replace("_", "-");
            Current = new CultureInfo(value);
            Changed?.Invoke(null, EventArgs.Empty);
        }
    }
}