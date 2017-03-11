using System;


namespace Plugin.DeviceInfo
{
    public interface IBatteryInfo
    {
        int Percentage { get; }
        PowerStatus Status { get; }

        IObservable<int> WhenBatteryPercentageChanged();
        IObservable<PowerStatus> WhenPowerStatusChanged();
    }
}
