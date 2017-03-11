using System;
using System.Reactive.Linq;
using Windows.ApplicationModel;
using Windows.UI.Xaml;


namespace Plugin.DeviceInfo
{
    public class AppInfo : AbstractAppInfo
    {
        public override string Version { get; } = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}.{Package.Current.Id.Version.Build}.{Package.Current.Id.Version.Revision}";
        public override string ShortVersion { get; } = $"{Package.Current.Id.Version.Major}.{Package.Current.Id.Version.Minor}";
        public override bool IsBackgrounded => !Window.Current.Visible;


        public override IObservable<object> WhenEnteringForeground()
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


        public override IObservable<object> WhenEnteringBackground()
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
