using System;
using System.Globalization;
using App = Android.App.Application;


namespace Acr.DeviceInfo {

    public class AppImpl : AbstractAppImpl {


        public AppImpl() {
            AppStateLifecyle.Register();

            var value = Java.Util.Locale.Default.ToString().Replace("_", "-");
            this.Locale = new CultureInfo(value);

            //TODO: detect changes
            //http://developer.android.com/reference/android/content/Intent.html#ACTION_LOCALE_CHANGED
            //intent filter/broadcast receiver on Intent.ActionLocaleChanged
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
    }
}