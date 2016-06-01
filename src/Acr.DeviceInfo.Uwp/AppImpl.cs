using System;
using System.Globalization;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;


namespace Acr.DeviceInfo
{

    public class AppImpl : AbstractAppImpl
    {

        public AppImpl()
        {
            var ver = Package.Current.Id.Version;
            this.Version = $"{ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}";

            this.Locale = CultureInfo.CurrentCulture;
        }


        protected override void StartMonitoringLocaleUpdates()
        {
        }


        protected override void StopMonitoringLocaleUpdates()
        {
        }


        protected override void StartMonitoringAppState()
        {
            Window.Current.VisibilityChanged += this.OnVisibilityChanged;
        }


        protected override void StopMonitoringAppState()
        {
            Window.Current.VisibilityChanged -= this.OnVisibilityChanged;
        }


        void OnVisibilityChanged(object sender, VisibilityChangedEventArgs args)
        {

        }
    }
}
