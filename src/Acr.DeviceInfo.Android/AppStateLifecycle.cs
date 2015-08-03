using System;
using Android.App;
using Android.OS;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;


namespace Acr.DeviceInfo {

    public class AppStateLifecyle : Java.Lang.Object, Application.IActivityLifecycleCallbacks, IComponentCallbacks2 {
        public static EventHandler StatusChanged;
        public static bool IsActive { get; private set; } = true;


        public static void Register() {
            var appstate = new AppStateLifecyle();
            ((Application)Application.Context.ApplicationContext).RegisterActivityLifecycleCallbacks(appstate);
            Application.Context.RegisterComponentCallbacks(appstate);
        }


        public void OnActivityResumed(Activity activity) {
            // TODO: if an activity is resuming, your app has moved into the foreground
            if (!IsActive) {
                IsActive = true;
                StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public void OnTrimMemory([GeneratedEnum] TrimMemory level) {
            if (level == TrimMemory.UiHidden) {
                IsActive = false;
                StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        // useless methods
        public void OnActivityCreated(Activity activity, Bundle savedInstanceState) {}
        public void OnActivityDestroyed(Activity activity) {}
        public void OnActivityPaused(Activity activity) {}
        public void OnActivitySaveInstanceState(Activity activity, Bundle outState) {}
        public void OnActivityStarted(Activity activity) {}
        public void OnActivityStopped(Activity activity) {}
        public void OnConfigurationChanged(Configuration newConfig) {}
        public void OnLowMemory() {}
    }
}