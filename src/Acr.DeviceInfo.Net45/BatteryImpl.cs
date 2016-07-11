using System;
using System.Reactive.Linq;
using System.Windows.Forms;


namespace Acr.DeviceInfo
{

    public class BatteryImpl : IBattery
    {
        public int Percentage => Convert.ToInt32(SystemInformation.PowerStatus.BatteryLifePercent);

        public PowerStatus Status
        {
            get
            {
                switch (SystemInformation.PowerStatus.BatteryChargeStatus)
                {
                    case BatteryChargeStatus.Charging:
                        return PowerStatus.Charging;

                    case BatteryChargeStatus.Unknown:
                        return PowerStatus.Unknown;

                    case BatteryChargeStatus.NoSystemBattery:
                        return PowerStatus.NoBattery;

                    default:
                        return PowerStatus.Discharging;
   ;             }
            }
        }


        public IObservable<int> WhenBatteryPercentageChanged()
        {
            return Observable.Empty<int>();
        }


        public IObservable<PowerStatus> WhenPowerStatusChanged()
        {
            return Observable.Empty<PowerStatus>();
        }
    }
}
