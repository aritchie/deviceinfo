using System;
using System.Globalization;


namespace Acr.DeviceInfo
{

    public interface IApp
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
