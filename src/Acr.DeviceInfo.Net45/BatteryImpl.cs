using System;
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
            throw new NotImplementedException();
        }

        public IObservable<PowerStatus> WhenPowerStatusChanged()
        {
            throw new NotImplementedException();
        }
    }
}
