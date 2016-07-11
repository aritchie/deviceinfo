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
        public EventHandler StatusChanged;
        public bool IsActive { get; private set; } = true;


        public void OnActivityResumed(Activity activity)
        {
            // if an activity is resuming, your app has moved into the foreground
            if (this.IsActive)
                Debug.WriteLine("Activity resumed - app was already active");
            else
            {
                Debug.WriteLine("Activity resumed - app was not active.  Firing app resume event!");
                this.IsActive = true;
                this.StatusChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public void OnTrimMemory([GeneratedEnum] TrimMemory level)
        {
            Debug.WriteLine("Android trimming memory");
            if (level == TrimMemory.UiHidden)
            {
                Debug.WriteLine("Android trimming UI - set app to background and fire event");
                this.IsActive = false;
                this.StatusChanged?.Invoke(this, EventArgs.Empty);
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