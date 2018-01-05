using System;
using System.Diagnostics;
using System.Globalization;
using System.Reactive;
using System.Reactive.Linq;
using Android.App;
using Android.Content;
using Java.Util;
using Acr;
using App = Android.App.Application;
using Observable = System.Reactive.Linq.Observable;


namespace Plugin.DeviceInfo
{
    public class AppInfo : IAppInfo
    {
        readonly AppStateLifecyle appState;


        public AppInfo()
        {
            this.appState = new AppStateLifecyle();
            var app = Application.Context.ApplicationContext as Application;
            if (app == null)
                throw new ApplicationException("Invalid application context");

            app.RegisterActivityLifecycleCallbacks(this.appState);
            app.RegisterComponentCallbacks(this.appState);
        }


        public CultureInfo CurrentCulture => this.GetCurrentCulture();


        public IObservable<CultureInfo> WhenCultureChanged() => AndroidObservables
            .WhenIntentReceived(Intent.ActionLocaleChanged)
            .Select(x => this.GetCurrentCulture());


        public IObservable<Unit> WhenEnteringForeground() => Observable.Create<Unit>(ob =>
        {
            var handler = new EventHandler((sender, args) =>
            {
                if (this.appState.IsActive)
                {
                    Debug.WriteLine("Firing WhenEnteringForeground Observable");
                    ob.OnNext(Unit.Default);
                }
            });
            this.appState.StatusChanged += handler;

            return () => this.appState.StatusChanged -= handler;
        });


        public IObservable<Unit> WhenEnteringBackground() => Observable.Create<Unit>(ob =>
        {
            var handler = new EventHandler((sender, args) =>
            {
                Debug.WriteLine("Firing 1 WhenEnteringBackground Observable");

                if (!this.appState.IsActive)
                {
                    Debug.WriteLine("Firing WhenEnteringBackground Observable");
                    ob.OnNext(Unit.Default);
                }
            });
            this.appState.StatusChanged += handler;

            return () => this.appState.StatusChanged -= handler;
        });


        public string Version => App
            .Context
            .ApplicationContext
            .PackageManager
            .GetPackageInfo(App.Context.PackageName, 0)
            .VersionName;


        public string ShortVersion => App
            .Context
            .ApplicationContext
            .PackageManager
            .GetPackageInfo(App.Context.PackageName, 0)
            .VersionCode
            .ToString();


        public bool IsBackgrounded => !this.appState.IsActive;
        //var mgr = (ActivityManager) Application.Context.GetSystemService(Context.ActivityService);
        //var tasks = mgr.GetRunningTasks(Int16.MaxValue);
        //var result = tasks.Any(x => x.TopActivity.PackageName.Equals(App.Context.PackageName));
        //return !result;


        protected virtual CultureInfo GetCurrentCulture()
        {
            var value = Locale.Default.ToString().Replace("_", "-");
            return new CultureInfo(value);
        }
    }
}