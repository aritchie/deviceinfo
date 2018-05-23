using System;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Foundation;
using UIKit;

namespace Plugin.DeviceInfo
{
    public partial class AppImpl : IApp
    {
        public bool IsBackgrounded => UIApplication.SharedApplication.ApplicationState != UIApplicationState.Active;
        public IObservable<Unit> WhenEnteringForeground() => Observable.Create<Unit>(ob =>
        {
            NSObject token = null;
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
                token = UIApplication
                    .Notifications
                    .ObserveWillEnterForeground((sender, args) => ob.OnNext(Unit.Default))
            );

            return () => token?.Dispose();
        });


        public IObservable<Unit> WhenEnteringBackground() => Observable.Create<Unit>(ob =>
        {
            NSObject token = null;
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
                token = UIApplication
                    .Notifications
                    .ObserveDidEnterBackground((sender, args) => ob.OnNext(Unit.Default))
            );

            return () => token?.Dispose();
        });


        public IObservable<Unit> EnableIdleTimer(bool enabled) => Observable.Empty<Unit>();
        public bool IsIdleTimerEnabled => true;
    }
}