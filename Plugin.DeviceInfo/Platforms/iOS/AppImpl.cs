using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
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


        public IObservable<Unit> EnableIdleTimer(bool enabled) => Observable.FromAsync(async _ =>
        {
            var tcs = new TaskCompletionSource<object>();
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                try
                {
                    UIApplication.SharedApplication.IdleTimerDisabled = !enabled;
                    tcs.SetResult(true);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });

            await tcs.Task;
        });


        public bool IsIdleTimerEnabled => !UIApplication.SharedApplication.IdleTimerDisabled;
    }
}