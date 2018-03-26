using System;


namespace Plugin.DeviceInfo
{
    public interface IPowerState
    {
        IObservable<int> WhenBatteryPercentageChanged();
        IObservable<PowerStatus> WhenPowerStatusChanged();
    }
}
