using System;
using System.Globalization;
using Windows.ApplicationModel;
using Windows.UI.Xaml;


namespace Acr.DeviceInfo {

    public class AppImpl : AbstractAppImpl {

        public AppImpl() {
            Window.Current.VisibilityChanged += (sender, args) => this.IsBackgrounded = !args.Visible;

            var ver = Package.Current.Id.Version;
            this.Version = $"{ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}";

            // TODO: changes
            this.Locale = CultureInfo.CurrentCulture;
        }
    }
}
