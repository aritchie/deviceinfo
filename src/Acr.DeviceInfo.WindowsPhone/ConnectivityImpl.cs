using System;
using Windows.Networking.Connectivity;
using Microsoft.Phone.Net.NetworkInformation;


namespace Acr.DeviceInfo {

    public class ConnectivityImpl : AbstractConnectivityImpl {

        public ConnectivityImpl() {
            NetworkInformation.NetworkStatusChanged += sender => this.SetState();
            this.SetState();
            this.CellularNetworkCarrier = DeviceNetworkInformation.CellularMobileOperator;
        }


        void SetState() {
        }
    }
}
