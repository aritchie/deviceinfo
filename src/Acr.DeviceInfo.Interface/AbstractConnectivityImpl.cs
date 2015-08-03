using System;


namespace Acr.DeviceInfo {

    public class AbstractConnectivityImpl : AbstractNpc, IConnectivity {

        bool internetAvail;
        public bool IsInternetAvailable {
            get { return this.internetAvail; }
            protected set { this.SetProperty(ref this.internetAvail, value); }
        }


        bool wifi;
        public bool IsWifi {
            get { return this.wifi; }
            protected set { this.SetProperty(ref this.wifi, value); }
        }


        bool cellular;
        public bool IsCellular {
            get { return this.cellular; }
            protected set { this.SetProperty(ref this.cellular, value); }
        }
    }
}
