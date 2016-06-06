using System;
using System.Globalization;
using System.Linq;
using Foundation;
using UIKit;


namespace Acr.DeviceInfo
{

    public class AppImpl : AbstractAppImpl
    {
        NSObject resumeCallback;
        NSObject backgroundCallback;
        NSObject localeCallback;
        CultureInfo cultureInfo;


        public AppImpl()
        {
            this.Version = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
            this.SetLocale(); // initial state
        }


        public override CultureInfo Locale => this.cultureInfo;
        public override bool IsForegrounded => UIApplication.SharedApplication.ApplicationState == UIApplicationState.Active;

        protected override void StartMonitoringAppState()
        {
            this.resumeCallback = UIApplication.Notifications.ObserveDidBecomeActive((sender, args) => this.OnAppStateChanged());
            this.backgroundCallback = UIApplication.Notifications.ObserveDidEnterBackground((sender, args) => this.OnAppStateChanged());
        }


        protected override void StopMonitoringAppState()
        {
            this.resumeCallback?.Dispose();
            this.resumeCallback = null;
            this.backgroundCallback?.Dispose();
            this.backgroundCallback = null;
        }


        protected override void StartMonitoringLocaleUpdates()
        {
            this.localeCallback = NSLocale.Notifications.ObserveCurrentLocaleDidChange((sender, args) =>
            {
                this.SetLocale();
                this.OnLocaleChanged();
            });
        }


        protected override void StopMonitoringLocaleUpdates()
        {
            this.localeCallback?.Dispose();
            this.localeCallback = null;
        }

        // taken from https://developer.xamarin.com/guides/cross-platform/xamarin-forms/localization/ with modifications
        void SetLocale()
        {
            try
            {
                var netLang = "en";
                var prefLang = "en";
                if (NSLocale.PreferredLanguages.Any())
                {
                    var pref = NSLocale.PreferredLanguages
                        .First()
                        .Substring(0, 2)
                        .ToLower();

                    if (prefLang == "pt")
                        pref = pref == "pt" ? "pt-BR" : "pt-PT";

                    netLang = pref.Replace("_", "0");
                    Console.WriteLine($"Preferred Language: {netLang}");
                }
                CultureInfo value;
                try
                {
                    Console.WriteLine($"Setting locale to {netLang}");
                    value = new CultureInfo(netLang);
                }
                catch
                {
                    Console.WriteLine($"Failed setting locale - moving to preferred langugage {prefLang}");
                    value = new CultureInfo(prefLang);
                }
                this.cultureInfo = value;
            }
            catch (Exception ex)
            {
                this.cultureInfo = CultureInfo.CurrentUICulture;
                Console.WriteLine($"Invalid culture code - {ex}");
            }
        }
    }
}