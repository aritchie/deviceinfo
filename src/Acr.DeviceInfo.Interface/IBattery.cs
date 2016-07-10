using System;


namespace Acr.DeviceInfo
{

    public interface IBattery
    {
        int Percentage { get; }
        PowerStatus Status { get; }

        IObservable<int> WhenBatteryPercentageChanged();
        IObservable<PowerStatus> WhenPowerStatusChanged();
    }
}
