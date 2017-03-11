using System;
using System.Globalization;


namespace Plugin.DeviceInfo
{

    public interface IAppInfo
    {
        string Version { get; }
        string ShortVersion { get; }
        CultureInfo CurrentCulture { get; }
        IObservable<CultureInfo> WhenCultureChanged();

        bool IsBackgrounded { get; }
        IObservable<object> WhenEnteringForeground();
        IObservable<object> WhenEnteringBackground();
    }
}
