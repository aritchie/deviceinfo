using System;
using System.Globalization;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Xaml;


namespace Acr.DeviceInfo
{

    public class AppImpl : IApp
    {

        //public AppImpl()
        //{
        //    var ver = Package.Current.Id.Version;
        //    this.Version = $"{ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}";

        //    this.Locale = CultureInfo.CurrentCulture;
        //}


        //protected override void StartMonitoringLocaleUpdates()
        //{
        //}


        //protected override void StopMonitoringLocaleUpdates()
        //{
        //}


        //protected override void StartMonitoringAppState()
        //{
        //    Window.Current.VisibilityChanged += this.OnVisibilityChanged;
        //}


        //protected override void StopMonitoringAppState()
        //{
        //    Window.Current.VisibilityChanged -= this.OnVisibilityChanged;
        //}


        //void OnVisibilityChanged(object sender, VisibilityChangedEventArgs args)
        //{

        //}
        public string Version { get; }
        public bool IsBackgrounded { get; }
        public CultureInfo CurrentCulture { get; }
        public IObservable<CultureInfo> WhenCultureChanged()
        {
            throw new NotImplementedException();
        }

        public IObservable<object> WhenEnteringForeground()
        {
            throw new NotImplementedException();
        }

        public IObservable<object> WhenEnteringBackground()
        {
            throw new NotImplementedException();
        }
    }
}
