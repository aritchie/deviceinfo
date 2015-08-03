using System;
using System.ComponentModel;


namespace Acr.DeviceInfo {

    public interface IConnectivity : INotifyPropertyChanged {

        bool IsInternetAvailable { get; }
        bool IsWifi { get; }
        bool IsCellular { get; }
    }
}
