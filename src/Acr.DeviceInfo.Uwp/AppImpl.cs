using System;
using System.Globalization;
using System.Reactive.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;


namespace Acr.DeviceInfo
{

    public class AppImpl : IApp
    {
        public string Version { get; } = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
        public bool IsBackgrounded => !Window.Current.Visible;
        public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;


        public IObservable<CultureInfo> WhenCultureChanged()
        {
            return Observable.Empty<CultureInfo>();
        }


        public IObservable<object> WhenEnteringForeground()
        {
            return Observable.Create<object>(ob =>
            {
                var handler = new WindowVisibilityChangedEventHandler((sender, args) =>
                {
                    if (args.Visible)
                        ob.OnNext(null);
                });
                Window.Current.VisibilityChanged += handler;
                return () => Window.Current.VisibilityChanged -= handler;
            });
        }


        public IObservable<object> WhenEnteringBackground()
        {
            return Observable.Create<object>(ob =>
            {
                var handler = new WindowVisibilityChangedEventHandler((sender, args) =>
                {
                    if (!args.Visible)
                        ob.OnNext(null);
                });
                Window.Current.VisibilityChanged += handler;
                return () => Window.Current.VisibilityChanged -= handler;
            });
        }
    }
}
