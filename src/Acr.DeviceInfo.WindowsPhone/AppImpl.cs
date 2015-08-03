using System;
using System.Globalization;
using System.Windows;
using Windows.ApplicationModel;


namespace Acr.DeviceInfo {

    public class AppImpl : AbstractAppImpl {

        public AppImpl() {
            // TODO: app state
            //Window.Current.VisibilityChanged += (sender, args) => { }
            this.Locale = CultureInfo.CurrentCulture;
            this.Version = Package.Current.Id.Version.ToString();
        }
    }
}
