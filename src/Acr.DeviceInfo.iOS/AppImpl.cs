using System;
using System.Globalization;
using System.Linq;
using Foundation;
using UIKit;


namespace Acr.DeviceInfo {

    public class AppImpl : AbstractAppImpl {

        public AppImpl() {
            this.Version = NSBundle.MainBundle.InfoDictionary["CFBundleVersion"].ToString();

            //UIApplication.Notifications.ObserveWillEnterForeground((sender, args) => {});
            UIApplication.Notifications.ObserveDidBecomeActive((sender, args) => this.IsBackgrounded = false);
            UIApplication.Notifications.ObserveDidEnterBackground((sender, args) => this.IsBackgrounded = true);
            this.IsBackgrounded = (UIApplication.SharedApplication.ApplicationState != UIApplicationState.Active);

            NSLocale.Notifications.ObserveCurrentLocaleDidChange((sender, args) => this.SetLocale());
            this.SetLocale(); // initial state
        }


        void SetLocale() {
            var netLocale = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier.Replace("_", "-");
            CultureInfo value;
            try {
                value = new CultureInfo(netLocale);
            }
            catch {
                var pl = NSLocale.PreferredLanguages.FirstOrDefault();
                value = pl == null
                    ? CultureInfo.CurrentUICulture
                    : new CultureInfo(pl);
            }
            this.Locale = value;
        }
    }
}