using System;


namespace Acr.DeviceInfo {

    public abstract class AbstractConnectivityImpl : AbstractNpc, IConnectivity {

        protected abstract string GetIpAddress();
        protected abstract string GetNetworkCarrier();
        protected abstract string GetWifiSsid();


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


        string networkCarrier;

        public string CellularNetworkCarrier
        {
            get { return this.networkCarrier; }
            protected set { this.SetProperty(ref this.networkCarrier, value); }
        }


        string wifiSsid;

        public string WifiSsid
        {
            get { return this.wifiSsid; }
            protected set { this.SetProperty(ref this.wifiSsid, value); }
        }


        ConnectionStatus reach;
        public ConnectionStatus InternetReachability {
            get { return this.reach; }
            protected set {
                if (!this.SetProperty(ref this.reach, value))
                    return;

                this.IsInternetAvailable = (value != ConnectionStatus.NotReachable);
                this.WifiSsid = this.GetWifiSsid();
                this.CellularNetworkCarrier = this.GetNetworkCarrier();
                this.IpAddress = this.GetIpAddress();
            }
        }
    }
}
