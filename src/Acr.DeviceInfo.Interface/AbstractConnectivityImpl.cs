using System;


namespace Acr.DeviceInfo {

    public class AbstractConnectivityImpl : AbstractNpc, IConnectivity {

        bool internetAvail;
        public bool IsInternetAvailable {
            get { return this.internetAvail; }
            private set { this.SetProperty(ref this.internetAvail, value); }
        }


        ConnectionStatus reach;
        public ConnectionStatus InternetReachability {
            get { return this.reach; }
            protected set {
                if (this.SetProperty(ref this.reach, value))
                    this.IsInternetAvailable = (value != ConnectionStatus.NotReachable);
            }
        }


        public string CellularNetworkCarrier { get; protected set; }
    }
}
