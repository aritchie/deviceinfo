using System;
using Acr.DeviceInfo;
using Xamarin.Forms;


namespace Samples {

    public partial class MainPage : ContentPage {

        public MainPage() {
            InitializeComponent();
            this.AppSection.BindingContext = DeviceInfo.App;
            this.BatterySection.BindingContext = DeviceInfo.Battery;
            this.CameraSection.BindingContext = DeviceInfo.Hardware;
            this.DisplaySection.BindingContext = DeviceInfo.Hardware;
            this.InfoSection.BindingContext = DeviceInfo.Hardware;
            this.ConnectSection.BindingContext = DeviceInfo.Connectivity;
        }
    }
}
