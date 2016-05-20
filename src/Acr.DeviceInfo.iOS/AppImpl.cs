using System;
using System.Globalization;
using System.Linq;
using Foundation;
using UIKit;


namespace Acr.DeviceInfo
{

    public class AppImpl : AbstractAppImpl
    {

        public AppImpl()
        {
            this.Version = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();

            //UIApplication.Notifications.ObserveWillEnterForeground((sender, args) => {});
            UIApplication.Notifications.ObserveDidBecomeActive((sender, args) => this.IsBackgrounded = false);
            UIApplication.Notifications.ObserveDidEnterBackground((sender, args) => this.IsBackgrounded = true);
            this.IsBackgrounded = (UIApplication.SharedApplication.ApplicationState != UIApplicationState.Active);

            NSLocale.Notifications.ObserveCurrentLocaleDidChange((sender, args) => this.SetLocale());
            this.SetLocale(); // initial state
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
                this.Locale = value;
            }
            catch (Exception ex)
            {
                this.Locale = CultureInfo.CurrentUICulture;
                Console.WriteLine($"Invalid culture code - {ex}");
            }
        }
    }
}