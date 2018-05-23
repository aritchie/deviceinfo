using System;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using Foundation;


namespace Plugin.DeviceInfo
{
    public partial class AppImpl : IApp
    {
        public string BundleName => NSBundle.MainBundle.BundleIdentifier;
        public string Version => NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();
        public string ShortVersion => NSBundle.MainBundle.InfoDictionary["CFBundleShortVersionString"].ToString();
        public CultureInfo CurrentCulture => this.GetSystemCultureInfo();


        public IObservable<CultureInfo> WhenCultureChanged() => Observable.Create<CultureInfo>(ob =>
            NSLocale
                .Notifications
                .ObserveCurrentLocaleDidChange((sender, args) =>
                {
                    var culture = this.GetSystemCultureInfo();
                    ob.OnNext(culture);
                })
        );


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