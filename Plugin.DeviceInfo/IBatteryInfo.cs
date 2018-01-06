using System;


namespace Plugin.DeviceInfo
{
    public interface IBatteryInfo
    {
        IObservable<int> WhenBatteryPercentageChanged();
        IObservable<PowerStatus> WhenPowerStatusChanged();
    }
}
