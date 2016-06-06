using System;
using System.Globalization;
using Android.App;
using Android.Content;
using Java.Util;


namespace Acr.DeviceInfo
{

    public class LocaleBroadcastReceiver : BroadcastReceiver
    {

        public static CultureInfo Current { get; private set; }
        public static EventHandler Changed;

        public static LocaleBroadcastReceiver Instance { get; } = new LocaleBroadcastReceiver();
        public static bool IsRegistered { get; private set; }

        public static void Register()
        {
            if (IsRegistered)
                return;

            IsRegistered = true;
            Application.Context.RegisterReceiver(Instance, new IntentFilter(Intent.ActionLocaleChanged));
            SetLocale();
        }


        public static void UnRegister()
        {
            if (IsRegistered)
            {
                Application.Context.UnregisterReceiver(Instance);
                IsRegistered = false;
            }
        }


        public override void OnReceive(Context context, Intent intent)
        {
            SetLocale();
        }


        static void SetLocale()
        {
            var value = Locale.Default.ToString().Replace("_", "-");
            Current = new CultureInfo(value);
            Changed?.Invoke(null, EventArgs.Empty);
        }
    }
}