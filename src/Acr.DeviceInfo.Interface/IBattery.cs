using System;
using System.ComponentModel;


namespace Acr.DeviceInfo {

    public interface IBattery : INotifyPropertyChanged {

        int Percentage { get; }
        bool IsCharging { get; }
    }
}
