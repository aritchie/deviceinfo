using System;
using System.ComponentModel;
using System.Globalization;


namespace Acr.DeviceInfo {

    public interface IApp : INotifyPropertyChanged {

        string Version { get; }
        bool IsBackgrounded { get; }
        CultureInfo Locale { get; }
        string LocaleString { get; }
    }
}
