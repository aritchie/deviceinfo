using System;


namespace Plugin.DeviceInfo
{

    public interface IBattery
    {
        int Percentage { get; }
        PowerStatus Status { get; }

        IObservable<int> WhenBatteryPercentageChanged();
        IObservable<PowerStatus> WhenPowerStatusChanged();
    }
}
