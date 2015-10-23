using System;


namespace Acr.DeviceInfo {

    public abstract class AbstractConnectivityImpl : AbstractNpc, IConnectivity {

        protected abstract string GetIpAddress();


        bool internetAvail;
        public bool IsInternetAvailable {
            get { return this.internetAvail; }
            private set { this.SetProperty(ref this.internetAvail, value); }
        }


        string ipAddress;
        public string IpAddress {
            get { return this.ipAddress; }
            protected set { this.SetProperty(ref this.ipAddress, value); }
        }


        ConnectionStatus reach;
        public ConnectionStatus InternetReachability {
            get { return this.reach; }
            protected set {
                if (!this.SetProperty(ref this.reach, value))
                    return;

                this.IsInternetAvailable = (value != ConnectionStatus.NotReachable);
                if (!this.IsInternetAvailable)
                    this.IpAddress = String.Empty;
                else {
                    try {
                        this.IpAddress = this.IsInternetAvailable ? this.GetIpAddress() : String.Empty;
                    }
                    catch {
                        this.IpAddress = String.Empty;
                    }
                }
            }
        }


        public string CellularNetworkCarrier { get; protected set; }
    }
}
