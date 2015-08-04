using System;
using Android.App;
using Android.Content.PM;


namespace Acr.DeviceInfo {

    public static class Utils {

        public static bool CheckPermission(string permission) {
            var result = Application.Context.ApplicationContext.CheckCallingOrSelfPermission(permission);
            if (result == Permission.Granted)
                return true;

            Console.WriteLine($"{permission} was not granted in your manifest");
            return false;
        }
    }
}