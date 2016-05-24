using System;
using System.Globalization;
using System.Linq;
using Foundation;
using UIKit;


namespace Acr.DeviceInfo
{

    public class AppImpl : IApp
    {

        public AppImpl()
        {
            this.Version = NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();

            UIApplication.Notifications.ObserveDidBecomeActive((sender, args) =>
            {
                this.IsBackgrounded = false;
                this.Resuming?.Invoke(this, EventArgs.Empty);
            });
            UIApplication.Notifications.ObserveDidEnterBackground((sender, args) =>
            {
                this.IsBackgrounded = true;
                this.EnteringSleep?.Invoke(this, EventArgs.Empty);
            });
            this.IsBackgrounded = (UIApplication.SharedApplication.ApplicationState != UIApplicationState.Active);

            NSLocale.Notifications.ObserveCurrentLocaleDidChange((sender, args) => this.SetLocale());
            this.SetLocale(); // initial state
        }


        public string Version { get; }
        public bool IsBackgrounded { get; private set; }
        public CultureInfo Locale { get; private set; }
        public event EventHandler LocaleChanged;
        public event EventHandler Resuming;
        public event EventHandler EnteringSleep;


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