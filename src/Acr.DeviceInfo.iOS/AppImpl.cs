using System;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using Foundation;
using UIKit;


namespace Acr.DeviceInfo
{
    public class AppImpl : IApp
    {
        public string Version => NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
        public bool IsBackgrounded => UIApplication.SharedApplication.ApplicationState != UIApplicationState.Active;
        public CultureInfo CurrentCulture => this.GetSystemCultureInfo();


        public IObservable<CultureInfo> WhenCultureChanged()
        {
            return Observable.Create<CultureInfo>(ob =>
                NSLocale
                    .Notifications
                    .ObserveCurrentLocaleDidChange((sender, args) =>
                    {
                        var culture = this.GetSystemCultureInfo();
                        ob.OnNext(culture);
                    })
            );
        }


        public IObservable<object> WhenEnteringForeground()
        {
            return Observable.Create<object>(ob =>
            {
                var token = UIApplication
                    .Notifications
                    .ObserveWillEnterForeground((sender, args) => ob.OnNext(null));

                return () => token.Dispose();
            });
        }


        public IObservable<object> WhenEnteringBackground()
        {
            return Observable.Create<object>(ob =>
            {
                var token = UIApplication
                    .Notifications
                    .ObserveDidEnterBackground((sender, args) => ob.OnNext(null));

                return () => token.Dispose();
            });
        }


        // taken from https://developer.xamarin.com/guides/cross-platform/xamarin-forms/localization/ with modifications
        protected virtual CultureInfo GetSystemCultureInfo()
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
                return value;
            }
            catch
            {
                return CultureInfo.CurrentUICulture;
            }
        }
    }
}