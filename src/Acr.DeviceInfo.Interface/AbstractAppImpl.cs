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
            protected set {
                if (this.SetProperty(ref this.locale, value))
                    this.RaisePropertyChanged("LocaleString");
            }
        }


        public string LocaleString => this.locale.ToString();

        public string Version { get; protected set; }
    }
}
