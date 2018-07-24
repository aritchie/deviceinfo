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
        public IObservable<AppState> WhenStateChanged() => Observable.Create<AppState>(ob =>
        {
            NSObject token1 = null;
            NSObject token2 = null;
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                token1 = UIApplication
                    .Notifications
                    .ObserveWillEnterForeground((sender, args) => ob.OnNext(AppState.Foreground));

                token2 = UIApplication
                    .Notifications
                    .ObserveDidEnterBackground((sender, args) => ob.OnNext(AppState.Background));
            });

            return () =>
            {
                token1?.Dispose();
                token2?.Dispose();
            };
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