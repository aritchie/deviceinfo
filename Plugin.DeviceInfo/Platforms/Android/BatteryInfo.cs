using System;
using System.Reactive.Linq;
using Acr;
using Android.Content;
using Android.OS;


namespace Plugin.DeviceInfo
{
    public class BatteryInfo : IBatteryInfo
    {
        public IObservable<int> WhenBatteryPercentageChanged() => AndroidObservables
            .WhenIntentReceived(Intent.ActionBatteryChanged)
            .Select(intent =>
            {
                var level = intent.GetIntExtra(BatteryManager.ExtraLevel, -1);
                var scale = intent.GetIntExtra(BatteryManager.ExtraScale, -1);
                var value = (int)Math.Floor(level * 100D / scale);
                return value;
            });


        public IObservable<PowerStatus> WhenPowerStatusChanged() => AndroidObservables
            .WhenIntentReceived(Intent.ActionBatteryChanged)
            //.WhenIntentReceived(Intent.ActionPowerConnected, Intent.ActionPowerDisconnected)
            .Select(intent =>
            {
                var status = (BatteryStatus)intent.GetIntExtra(BatteryManager.ExtraStatus, -1);
                switch (status)
                {
                    case BatteryStatus.Discharging:
                    case BatteryStatus.NotCharging:
                        return PowerStatus.Discharging;

                    case BatteryStatus.Charging:
                        return PowerStatus.Charging;

                    case BatteryStatus.Full:
                        return PowerStatus.Charged;

                    default:
                        return PowerStatus.Unknown;
                }
            });
    }
}