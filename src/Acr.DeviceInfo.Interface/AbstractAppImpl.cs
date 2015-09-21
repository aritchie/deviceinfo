using System;
using System.Globalization;


namespace Acr.DeviceInfo {

    public abstract class AbstractAppImpl : AbstractNpc, IApp {

        bool background;
        public bool IsBackgrounded {
            get { return this.background; }
            protected set { this.SetProperty(ref this.background, value); }
        }


        CultureInfo locale;
        public CultureInfo Locale {
            get { return this.locale; }
            protected set { this.SetProperty(ref this.locale, value); }
        }


        public string Version { get; protected set; }
    }
}
