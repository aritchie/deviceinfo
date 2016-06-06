using System;
using Android.App;
using Android.OS;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Debug = System.Diagnostics.Debug;


namespace Acr.DeviceInfo
{

    public class AppStateLifecyle : Java.Lang.Object, Application.IActivityLifecycleCallbacks, IComponentCallbacks2
    {
        public static EventHandler StatusChanged;
        public static bool IsActive { get; private set; } = true;
        public static AppStateLifecyle Instance { get; } = new AppStateLifecyle();

        public static void Register()
        {
            ((Application)Application.Context.ApplicationContext).RegisterActivityLifecycleCallbacks(appstate);
            Application.Context.RegisterComponentCallbacks(Instance);
        }


        public static void UnRegister()
        {
            Application.Context.UnregisterComponentCallbacks(Instance);
        }


        public void OnActivityResumed(Activity activity)
        {
            // if an activity is resuming, your app has moved into the foreground
            if (IsActive)
                Debug.WriteLine("Activity resumed - app was already active");
            else
            {
                Debug.WriteLine("Activity resumed - app was not active.  Firing app resume event!");
                IsActive = true;
                StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public void OnTrimMemory([GeneratedEnum] TrimMemory level)
        {
            Debug.WriteLine("Android trimming memory");
            if (level == TrimMemory.UiHidden)
            {
                Debug.WriteLine("Android trimming UI - set app to background and fire event");
                IsActive = false;
                StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        // useless methods
        public void OnActivityCreated(Activity activity, Bundle savedInstanceState) { }
        public void OnActivityDestroyed(Activity activity) { }
        public void OnActivityPaused(Activity activity) { }
        public void OnActivitySaveInstanceState(Activity activity, Bundle outState) { }
        public void OnActivityStarted(Activity activity) { }
        public void OnActivityStopped(Activity activity) { }
        public void OnConfigurationChanged(Configuration newConfig) { }
        public void OnLowMemory() { }
    }
}