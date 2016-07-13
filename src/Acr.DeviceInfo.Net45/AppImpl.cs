using System;
using System.Globalization;
using System.Reactive.Linq;
using System.Reflection;


namespace Acr.DeviceInfo
{

    public class AppImpl : IApp
    {
        public string Version => Assembly
                .GetEntryAssembly()
                .GetName()
                .Version
                .ToString();

        public string ShortVersion => this.Version;
        public bool IsBackgrounded { get; } = false;
        public CultureInfo CurrentCulture => CultureInfo.DefaultThreadCurrentCulture;


        public IObservable<CultureInfo> WhenCultureChanged()
        {
            return Observable.Empty<CultureInfo>();
        }


        public IObservable<object> WhenEnteringForeground()
        {
            return Observable.Empty<object>();
        }


        public IObservable<object> WhenEnteringBackground()
        {
            return Observable.Empty<object>();
        }
    }
}
