using System;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using Acr.DeviceInfo.Internals;
using Android.App;
using Android.Content;
using Java.Util;
using App = Android.App.Application;
using Observable = System.Reactive.Linq.Observable;

namespace Acr.DeviceInfo
{

    public class AppImpl : IApp
    {
        readonly AppStateLifecyle appState;


        public AppImpl()
        {
            this.appState = new AppStateLifecyle();
            ((Application)Application.Context.ApplicationContext).RegisterActivityLifecycleCallbacks(this.appState);
        }


        public CultureInfo CurrentCulture => this.GetCurrentCulture();


        public IObservable<CultureInfo> WhenCultureChanged()
        {
            return AndroidObservables
                .WhenIntentReceived(Intent.ActionLocaleChanged)
                .Select(x => this.GetCurrentCulture());
        }


        public IObservable<object> WhenEnteringForeground()
        {
            return Observable.Create<object>(ob =>
            {
                var handler = new EventHandler((sender, args) =>
                {
                    if (this.appState.IsActive)
                        ob.OnNext(null);
                });
                this.appState.StatusChanged += handler;

                return () => this.appState.StatusChanged -= handler;
            });
        }


        public IObservable<object> WhenEnteringBackground()
        {
            return Observable.Create<object>(ob =>
            {
                var handler = new EventHandler((sender, args) =>
                {
                    if (!this.appState.IsActive)
                        ob.OnNext(null);
                });

                return () => this.appState.StatusChanged -= handler;
            });
        }


        public string Version => App
            .Context
            .ApplicationContext
            .PackageManager
            .GetPackageInfo(App.Context.PackageName, 0)
            .VersionName;


        public bool IsBackgrounded
        {
            get
            {

                var mgr = (ActivityManager) Application.Context.GetSystemService(Context.ActivityService);
                var tasks = mgr.GetRunningTasks(Int16.MaxValue);
                var result = tasks.Any(x => x.TopActivity.PackageName.Equals(App.Context.PackageName));
                return !result;
            }
        }


        protected virtual CultureInfo GetCurrentCulture()
        {
            var value = Locale.Default.ToString().Replace("_", "-");
            return new CultureInfo(value);
        }
    }
}