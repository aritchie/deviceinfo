using System;
using System.ComponentModel;


namespace Acr.DeviceInfo {

    public interface IConnectivity : INotifyPropertyChanged {

        bool IsInternetAvailable { get; }
        ConnectionStatus InternetReachability { get; }
        string CellularNetworkCarrier { get; }
        string IpAddress { get; }
    }
}
