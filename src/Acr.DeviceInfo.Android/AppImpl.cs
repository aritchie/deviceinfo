using System;
using System.Globalization;
using System.Linq;
using Android.App;
using Android.Content;
using App = Android.App.Application;


namespace Acr.DeviceInfo
{

    public class AppImpl : AbstractAppImpl
    {
        public override CultureInfo CurrentCulture { get; }

        public override string Version => App
                .Context
                .ApplicationContext
                .PackageManager
                .GetPackageInfo(App.Context.PackageName, 0)
                .VersionName;


        public override bool IsForegrounded
        {
            get
            {
                var mgr = (ActivityManager)Application.Context.GetSystemService(Context.ActivityService);
                var tasks = mgr.GetRunningTasks(Int16.MaxValue);
                var result = tasks.Any(x => x.TopActivity.PackageName.Equals(App.Context.PackageName));
                return result;
            }
        }


        protected virtual void OnLocaleBroadcast(object sender, EventArgs args)
        {
            //this.Locale = LocaleBroadcastReceiver.Current;
            this.OnLocaleChanged();
        }


        protected override void StartMonitoringLocaleUpdates()
        {
            LocaleBroadcastReceiver.Changed += this.OnLocaleBroadcast;
            LocaleBroadcastReceiver.Register();
        }


        protected override void StopMonitoringLocaleUpdates()
        {
            LocaleBroadcastReceiver.Changed -= this.OnLocaleBroadcast;
            LocaleBroadcastReceiver.UnRegister();
        }


        protected override void StartMonitoringAppState()
        {
            //AppStateLifecyle.StatusChanged += (sender, args) => this.IsBackgrounded = !AppStateLifecyle.IsActive;
            AppStateLifecyle.Register();
        }


        protected override void StopMonitoringAppState()
        {
            //AppStateLifecyle.StatusChanged -= (sender, args) => this.IsBackgrounded = !AppStateLifecyle.IsActive;
            AppStateLifecyle.UnRegister();
        }
    }
}