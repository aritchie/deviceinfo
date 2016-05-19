using System;


namespace Acr.DeviceInfo
{

    public interface IBattery
    {
        int Percentage { get; }
        bool IsCharging { get; }

        event EventHandler StateChanged;
    }
}
