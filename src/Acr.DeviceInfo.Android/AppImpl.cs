using System;
using App = Android.App.Application;


namespace Acr.DeviceInfo
{

    public class AppImpl : AbstractAppImpl
    {


        public AppImpl()
        {
            this.Version = App
                .Context
                .ApplicationContext
                .PackageManager
                .GetPackageInfo(App.Context.PackageName, 0)
                .VersionName;
        }


        //public bool IsAppInBackground {
        //    get {
        //        var mgr = (ActivityManager)Application.Context.GetSystemService(Context.ActivityService);
        //        var tasks = mgr.GetRunningTasks(Int16.MaxValue);
        //        var result = tasks.Any(x => x.TopActivity.PackageName.Equals(Application.Context.PackageName));
        //        return !result;
        //    }
        //}

        protected override void StartMonitoringLocaleUpdates()
        {
            LocaleBroadcastReceiver.Changed += (sender, args) => this.Locale = LocaleBroadcastReceiver.Current;
            LocaleBroadcastReceiver.Register();
        }


        protected override void StopMonitoringLocaleUpdates()
        {
            //LocaleBroadcastReceiver.Changed -= (sender, args) => this.Locale = LocaleBroadcastReceiver.Current;
            //LocaleBroadcastReceiver.UnRegister();
        }


        protected override void StartMonitoringAppState()
        {
            AppStateLifecyle.StatusChanged += (sender, args) => this.IsBackgrounded = !AppStateLifecyle.IsActive;
            AppStateLifecyle.Register();
        }


        protected override void StopMonitoringAppState()
        {
            //AppStateLifecyle.StatusChanged -= (sender, args) => this.IsBackgrounded = !AppStateLifecyle.IsActive;
            //AppStateLifecyle.UnRegister();
        }
    }
}