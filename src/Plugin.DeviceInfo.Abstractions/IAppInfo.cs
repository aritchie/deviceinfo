using System;
using System.Globalization;


namespace Plugin.DeviceInfo
{

    public interface IAppInfo
    {
        string Version { get; }
        string ShortVersion { get; }
        bool IsBackgrounded { get; }
        CultureInfo CurrentCulture { get; }

        IObservable<CultureInfo> WhenCultureChanged();
        IObservable<object> WhenEnteringForeground();
        IObservable<object> WhenEnteringBackground();
    }
}
