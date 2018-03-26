using System;
using System.Globalization;
using System.Reactive;


namespace Plugin.DeviceInfo
{

    public interface IApp
    {
        string Version { get; }
        string ShortVersion { get; }
        CultureInfo CurrentCulture { get; }
        IObservable<CultureInfo> WhenCultureChanged();

        bool IsBackgrounded { get; }
        IObservable<Unit> WhenEnteringForeground();
        IObservable<Unit> WhenEnteringBackground();
    }
}
