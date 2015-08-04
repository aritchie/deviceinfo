using System;
using System.Diagnostics;
using Acr.DeviceInfo;
using Xamarin.Forms;


namespace Samples {

    public partial class MainPage : ContentPage {
        bool bg;

        public MainPage() {
            InitializeComponent();
            this.AppSection.BindingContext = DeviceInfo.App;
            this.BatterySection.BindingContext = DeviceInfo.Battery;
            this.CameraSection.BindingContext = DeviceInfo.Hardware;
            this.DisplaySection.BindingContext = DeviceInfo.Hardware;
            this.InfoSection.BindingContext = DeviceInfo.Hardware;
            this.ConnectSection.BindingContext = DeviceInfo.Connectivity;

            DeviceInfo.App.PropertyChanged += (sender, args) => {
                // this isn't particularily useful for xamarin forms since they have an implementation of resume/sleep in app.  HOWEVER, it is good for things like MvvmCross and background services of your own
                if (args.PropertyName != "IsBackgrounded")
                    return;

                if (bg) {
                    bg = false;
                    try {
                        this.FromBg.On = true;
                        // below will return before xamarin forms is up on on android!
                        //Device.BeginInvokeOnMainThread(() => {
                        //    this.DisplayAlert("Returning from background", "OK", null);
                        //});
                    }
                    catch (Exception ex) {
                        Debug.WriteLine("ERROR: " + ex);
                    }
                }
                else {
                    bg = DeviceInfo.App.IsBackgrounded;
                }
            };
        }
    }
}
