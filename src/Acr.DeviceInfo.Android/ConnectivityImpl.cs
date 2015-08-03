using System;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
            ConnectivityBroadcastReceiver.Register();
            ConnectivityBroadcastReceiver.StatusChanged = (sender, args) => {
                this.IsInternetAvailable = ConnectivityBroadcastReceiver.IsInternetAvailable;
                this.IsWifi = ConnectivityBroadcastReceiver.IsInternetWifi;
                this.IsCellular = ConnectivityBroadcastReceiver.IsInternetCellular;
            };
            // TODO: force run?
        }
    }
}