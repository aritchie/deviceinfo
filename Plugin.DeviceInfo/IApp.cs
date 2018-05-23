using System;
using System.Globalization;
using System.Reactive;


namespace Plugin.DeviceInfo
{

    public interface IApp
    {
        string BundleName { get; }
        string Version { get; }
        string ShortVersion { get; }
        CultureInfo CurrentCulture { get; }
        IObservable<CultureInfo> WhenCultureChanged();

        bool IsBackgrounded { get; }
        IObservable<Unit> WhenEnteringForeground();
        IObservable<Unit> WhenEnteringBackground();

        /// <summary>
        /// Setting this to false, forces the screen to remain on
        /// </summary>
        IObservable<Unit> EnableIdleTimer(bool enabled);

        bool IsIdleTimerEnabled { get; }
    }
}
